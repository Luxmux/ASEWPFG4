﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BestSledWPFG1
{
    /// <summary>
    /// Interaction logic for Defaults.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public MainWindow mainWindow;
        ModBusSlave mySlave;

        public Admin(MainWindow _mainWindow)
        {
            InitializeComponent();
            this.mainWindow = _mainWindow;
            mySlave = MainWindow.modbusSlaveList[mainWindow.selectedSlaveID];
			
			//update UI
			this.Dispatcher.Invoke(() =>
            {
				
				if (mainWindow.loggingToFilesAdmin && mainWindow.foundbestsled)
				{
					this.Dispatcher.Invoke(() =>
					{
						AdminLoggingOnOff.Content = "Stop Logging";

						LoggingButAdminExisting.IsEnabled = false;
						LoggingButAdminNew.IsEnabled = false;
						AdminLoggingOnOff.IsEnabled = true;
						logIntervalAdmin.IsEnabled = false;
					});

					if (mainWindow.LoggingIntervalAdmin == 1) logIntervalAdmin.SelectedIndex = 0;
					if (mainWindow.LoggingIntervalAdmin == 5) logIntervalAdmin.SelectedIndex = 1;
					if (mainWindow.LoggingIntervalAdmin == 10) logIntervalAdmin.SelectedIndex = 2;
					if (mainWindow.LoggingIntervalAdmin == 15) logIntervalAdmin.SelectedIndex = 3;
					if (mainWindow.LoggingIntervalAdmin == 30) logIntervalAdmin.SelectedIndex = 4;
					if (mainWindow.LoggingIntervalAdmin == 60) logIntervalAdmin.SelectedIndex = 5;



					//Assign slave object properties
					mySlave.SlaveAdminLogFilePath = mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()];
					filepath.Text = mySlave.SlaveAdminLogFilePath;

					//Establish output streams
					mySlave.slaveAdminLogFileOutputStream = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Append, FileAccess.Write, FileShare.Read);
					mySlave.slaveAdminLogFileFsWriter = new StreamWriter(mySlave.slaveAdminLogFileOutputStream);

					//Establish input streams
					mySlave.adminIStreamLeft = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					mySlave.adminIStreamRight = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

					mySlave.adminFSReaderLeft = new StreamReader(mySlave.adminIStreamLeft);
					mySlave.adminFSReaderRight = new StreamReader(mySlave.adminIStreamRight);

					//Read past header line for left and right streams for both files
					mySlave.adminFSReaderLeft.ReadLine();
					mySlave.adminFSReaderRight.ReadLine();

					mySlave.CurrentLeftIndex = -1;
					mySlave.CurrentRightIndex = -1;

					mainWindow.loggingToFilesAdmin = true;
					filepath.Background = new SolidColorBrush(Color.FromRgb(34, 139, 34));
					AdminLoggingOnOff.Content = "Stop Admin Logging";

				}
				else
				{
					this.Dispatcher.Invoke(() =>
					{
						LoggingButAdminExisting.IsEnabled = true;
						LoggingButAdminNew.IsEnabled = true;
						AdminLoggingOnOff.IsEnabled = false;
						logIntervalAdmin.IsEnabled = true;

						filepath.Background = new SolidColorBrush(Color.FromRgb(205, 92, 92));
					});
				}
				if (mySlave.FirmwareVersion < 3)
				{
					OSEBodyTECTimeConstantLabel.Content = "OSE TEC Time Constant";
					KpLabel.Content = "OSE TEC Kp";
					KdLabel.Content = "OSE TEC Kd";
					KiLabel.Content = "OSE TEC Ki";


					

					OSECapacityLabel.Content = "OSE TEC Capacity";
					OSECurrentLabel.Content = "OSE TEC Current";
					OSEBodyTECCoolingPIDLabel.Content = "OSE TEC Cooling PID";
					OSEBodyTECHeatingPIDLabel.Content = "OSE TEC Heating PID";
		
				}
				else
				{
					
				}



				if (mySlave.Capabilities[26] == 0)
				{
					PMTECTimeConstantSetButton.IsEnabled = false;
					PMTECTimeConstantSetBox.IsEnabled = false;
					PMKpSetButton.IsEnabled = false;
					PMKpSetBox.IsEnabled = false;
					PMKdSetButton.IsEnabled = false;
					PMKdSetBox.IsEnabled = false;
					PMKiSetButton.IsEnabled = false;
					PMKiSetBox.IsEnabled = false;
				}
							
				
	
				BoardTempBox.Visibility = Visibility.Visible;
				BoardTempRaw.Visibility = Visibility.Visible;
				FunctionCode.Text = "";
			});
        }

        private void ManualPollChanged (object sender, RoutedEventArgs e)
        {

            ComboBoxItem new_function = FunctionCode.SelectedItem as ComboBoxItem;

            if (new_function != null)
            {
                if (new_function.Content.ToString() == "Read Holding Reg")
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Addr.IsEnabled = true;
                        NumReg.IsEnabled = true;
                        Value.IsEnabled = false;
                        Value.Text = "";
                    });
                }
                else if (new_function.Content.ToString() == "Read Input Reg")
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Addr.IsEnabled = true;
                        NumReg.IsEnabled = true;
                        Value.IsEnabled = false;
                        Value.Text = "";
                    });
                }
                else if (new_function.Content.ToString() == "Write Single Reg")
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Addr.IsEnabled = true;
                        NumReg.IsEnabled = false;
                        Value.IsEnabled = true;
                        NumReg.Text = "";
                    });
                }
            }



        }

        private void ManualModbusSend_Click(object sender, RoutedEventArgs e)
        {
            string result = "";

            if (FunctionCode.SelectedIndex==0) //read holding register
            {
				try
				{
					UInt16[] readData = mainWindow.mbClient.ReadHoldingRegisters((byte)mySlave.ModbusID,int.Parse(Addr.Text), int.Parse(NumReg.Text));
					for (int x = 0; x < int.Parse(NumReg.Text); x++)
					{
						result += readData[x].ToString() + "\r\n";
					}
					ListBox1.Text = result;
				}
				catch
				{
					ListBox1.Text = "Error Reading Reg";
				}
			}
            else if (FunctionCode.SelectedIndex == 1) // read input register
            {
				try
				{
					UInt16[] readData = mainWindow.mbClient.ReadInputRegisters((byte)mySlave.ModbusID,int.Parse(Addr.Text), int.Parse(NumReg.Text));
					for (int x = 0; x < int.Parse(NumReg.Text); x++)
					{
						result += readData[x].ToString() + "\r\n";
					}
					ListBox1.Text = result;
				}
				catch
				{
					ListBox1.Text = "Error Reading Reg";
				}
			}
            else if (FunctionCode.SelectedIndex == 2) // rwite single register
            {
				try
				{
					mainWindow.mbClient.WriteSingleRegister((byte)mySlave.ModbusID,int.Parse(Addr.Text), UInt16.Parse(Value.Text));
					ListBox1.Text = "success";
				}
                catch
                {
					ListBox1.Text = "Error Writing Reg";
				}
            }
        }

		private void Show_Raw_Checked(object sender, RoutedEventArgs e) {
			mainWindow.showRawValues = true;
		}
		private void Show_Raw_Unchecked(object sender, RoutedEventArgs e) {
			mainWindow.showRawValues = false;
		}
		
		private void LoggingButAdminNew_Click(object sender, RoutedEventArgs e)
        {
            if (mySlave.AdminLogginBut == false)
            {
				SaveFileDialog fileDialog = new SaveFileDialog();
				fileDialog.DefaultExt = ".csv"; // Required file extension 
				fileDialog.Filter = "csv files (.csv)|*.csv"; // Optional file extensions

				bool? res = fileDialog.ShowDialog();
				if (res.HasValue && res.Value)
				{
					filepath.Text = fileDialog.FileName;
                    mainWindow.adminLogFilePathStrings.Remove(mySlave.SlaveID.ToString());
					mainWindow.adminLogFilePathStrings.Add(mySlave.SlaveID.ToString(), fileDialog.FileName);

					//Create empty files
					File.WriteAllText(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], String.Empty);
					//Assign slave object properties
					mySlave.SlaveAdminLogFilePath = mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()];


					//Initialize line counts
					mySlave.AdminLogfileLineCount = 0;

					

					
					//Establish output streams
					mySlave.slaveAdminLogFileOutputStream = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Append, FileAccess.Write, FileShare.Read);
					mySlave.slaveAdminLogFileFsWriter = new StreamWriter(mySlave.slaveAdminLogFileOutputStream);

					//Establish input streams
					mySlave.adminIStreamLeft = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					mySlave.adminIStreamRight = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

					mySlave.adminFSReaderLeft = new StreamReader(mySlave.adminIStreamLeft);
					mySlave.adminFSReaderRight = new StreamReader(mySlave.adminIStreamRight);

					//Initialize both CSV files
					mainWindow.InitializeAdminCSVFile(mySlave);

					//Read past header line for left and right streams for both files
					mySlave.adminFSReaderLeft.ReadLine();
					mySlave.adminFSReaderRight.ReadLine();

					mySlave.CurrentLeftIndex = -1;
					mySlave.CurrentRightIndex = -1;

					mySlave.AdminLogginBut = true;
					mainWindow.loggingToFilesAdmin = true;
					filepath.Background = new SolidColorBrush(Color.FromRgb(34, 139, 34));
					AdminLoggingOnOff.Content = "Stop Admin Logging";
				}
			}
            else
            {
				MessageBox.Show("Please stop logging before changing path");
            }
        }

		private void LoggingButAdminExisting_Click(object sender, RoutedEventArgs e)
		{
			if (mySlave.AdminLogginBut == false)
			{

				OpenFileDialog fileDialog = new OpenFileDialog();
				fileDialog.DefaultExt = ".csv"; // Required file extension 
				fileDialog.Filter = "csv files (.csv)|*.csv"; // Optional file extensions
				bool? res = fileDialog.ShowDialog();
				if (res.HasValue && res.Value)
				{
					filepath.Text = fileDialog.FileName;
					mainWindow.adminLogFilePathStrings.Remove(mySlave.SlaveID.ToString());

					mainWindow.adminLogFilePathStrings.Add(mySlave.SlaveID.ToString(), fileDialog.FileName);

					//Assign slave object properties
					mySlave.SlaveAdminLogFilePath = mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()];

					//Establish output streams
					mySlave.slaveAdminLogFileOutputStream = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Append, FileAccess.Write, FileShare.Read);
					mySlave.slaveAdminLogFileFsWriter = new StreamWriter(mySlave.slaveAdminLogFileOutputStream);

					//Establish input streams
					mySlave.adminIStreamLeft = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					mySlave.adminIStreamRight = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

					mySlave.adminFSReaderLeft = new StreamReader(mySlave.adminIStreamLeft);
					mySlave.adminFSReaderRight = new StreamReader(mySlave.adminIStreamRight);

					//Read past header line for left and right streams for both files
					mySlave.adminFSReaderLeft.ReadLine();
					mySlave.adminFSReaderRight.ReadLine();

					mySlave.CurrentLeftIndex = -1;
					mySlave.CurrentRightIndex = -1;

					mySlave.AdminLogginBut = true;
					mainWindow.loggingToFilesAdmin = true;
					filepath.Background = new SolidColorBrush(Color.FromRgb(34, 139, 34));
					AdminLoggingOnOff.Content = "Stop Admin Logging";
				}
			}
			else
			{
				MessageBox.Show("Please stop logging before changing path");
			}
		}

		private void AdminLoggingOnOff_Click(object sender, RoutedEventArgs e)
		{
			if (filepath.Text.Trim() != "[Please choose admin log path]")
			{
				if (mySlave.AdminLogginBut == true)
				{
					mySlave.AdminLogginBut = false;
					mySlave.slaveAdminLogFileFsWriter.Close();
					filepath.Background = new SolidColorBrush(Color.FromRgb(205, 92, 92));
					AdminLoggingOnOff.Content = "Start Admin Logging";

				}
				else
				{			
					mainWindow.adminLogFilePathStrings.Remove(mySlave.SlaveID.ToString());

					mainWindow.adminLogFilePathStrings.Add(mySlave.SlaveID.ToString(), filepath.Text);

					//Assign slave object properties
					mySlave.SlaveAdminLogFilePath = mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()];

					//Establish output streams
					mySlave.slaveAdminLogFileOutputStream = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Append, FileAccess.Write, FileShare.Read);
					mySlave.slaveAdminLogFileFsWriter = new StreamWriter(mySlave.slaveAdminLogFileOutputStream);

					//Establish input streams
					mySlave.adminIStreamLeft = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					mySlave.adminIStreamRight = new FileStream(mainWindow.adminLogFilePathStrings[mySlave.SlaveID.ToString()], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

					mySlave.adminFSReaderLeft = new StreamReader(mySlave.adminIStreamLeft);
					mySlave.adminFSReaderRight = new StreamReader(mySlave.adminIStreamRight);

					//Read past header line for left and right streams for both files
					mySlave.adminFSReaderLeft.ReadLine();
					mySlave.adminFSReaderRight.ReadLine();

					mySlave.CurrentLeftIndex = -1;
					mySlave.CurrentRightIndex = -1;

					mySlave.AdminLogginBut = true;
					mainWindow.loggingToFilesAdmin = true;
					filepath.Background = new SolidColorBrush(Color.FromRgb(34, 139, 34));
					AdminLoggingOnOff.Content = "Stop Admin Logging";
				}
			}
			else
			{
				MessageBox.Show("Please create or a choose file first before running logging.");
			}
		}
		


		
		private void OSETECTimeConstantSetButton_Click(object sender, RoutedEventArgs e)
        {
			this.Dispatcher.Invoke(() =>
			{
				if (OSETECTimeConstantSetBox.Text.Length > 0 && IsTextNumeric(OSETECTimeConstantSetBox.Text.Replace("s", "").Replace(" ", "")) == true)
				{

					double Rawvalue;
					UInt16 value1 = 0;
					UInt16 value2 = 0;

					if ((double.Parse(OSETECTimeConstantSetBox.Text) < 0 || double.Parse(OSETECTimeConstantSetBox.Text) > 1900))
					{
					}
					else
					{
						Rawvalue = (double.Parse(OSETECTimeConstantSetBox.Text) * 2250000);
						value1 = (UInt16)(Rawvalue / 65536);
						if (value1 >= 0)
						{
							value2 = (UInt16)(Rawvalue - value1 * 65536);
						}

						mainWindow.mbClient.WriteSingleRegister((byte)mySlave.ModbusID, 57, value1);
						mainWindow.mbClient.WriteSingleRegister((byte)mySlave.ModbusID, 58, value2);
					}
				}
				else
				{
					MessageBox.Show("Error: Invalid OSE Body Time Constant Value");

				}
				OSETECTimeConstantSetBox.Text = "";
			});
        }

        private void KpSetButton_Click(object sender, RoutedEventArgs e)
        {
			this.Dispatcher.Invoke(() =>
			{
				if (KpSetBox.Text.Length > 0 && IsTextNumeric(KpSetBox.Text.Replace(" ", "")) == true)
				{

					if ((double.Parse(KpSetBox.Text) < 0 || double.Parse(KpSetBox.Text) > 65535))
					{
					}
					else
					{
						mainWindow.mbClient.WriteSingleRegister((byte)mySlave.ModbusID, 61, UInt16.Parse(KpSetBox.Text)); ;
					}
				}
				else
				{
					MessageBox.Show("Error: Invalid OSE Body Kp Value");

				}
				KpSetBox.Text = "";
			});
        }

        private void KdSetButton_Click(object sender, RoutedEventArgs e)
        {
			this.Dispatcher.Invoke(() =>
			{
				if (KdSetBox.Text.Length > 0 && IsTextNumeric(KdSetBox.Text.Replace(" ", "")) == true)
				{

					if ((double.Parse(KdSetBox.Text) < 0 || double.Parse(KdSetBox.Text) > 65535))
					{
					}
					else
					{
						mainWindow.mbClient.WriteSingleRegister((byte)mySlave.ModbusID, 63, UInt16.Parse(KdSetBox.Text)); ;
					}
				}
				else
				{
					MessageBox.Show("Error: Invalid OSE Body Kd Value");

				}
				KdSetBox.Text = "";
			});
        }

        private void KiSetButton_Click(object sender, RoutedEventArgs e)
        {
			this.Dispatcher.Invoke(() =>
			{
				if (KiSetBox.Text.Length > 0 && IsTextNumeric(KiSetBox.Text.Replace(" ", "")) == true)
				{
					if ((double.Parse(KiSetBox.Text) < 0 || double.Parse(KiSetBox.Text) > 65535))
					{
					}
					else
					{
						mainWindow.mbClient.WriteSingleRegister((byte)mySlave.ModbusID, 65, UInt16.Parse(KiSetBox.Text)); ;
					}
				}
				else
				{
					MessageBox.Show("Error: Invalid OSE Body Ki Value");

				}
				KiSetBox.Text = "";
			});
        }



		private void PMTECTimeConstantSetButton_Click(object sender, RoutedEventArgs e)
		{
			this.Dispatcher.Invoke(() =>
			{
				if (PMTECTimeConstantSetBox.Text.Length > 0 && IsTextNumeric(PMTECTimeConstantSetBox.Text.Replace("s", "").Replace(" ", "")) == true)
				{
					double Rawvalue;
					UInt16 value1 = 0;
					UInt16 value2 = 0;

					if ((double.Parse(PMTECTimeConstantSetBox.Text) < 0 || double.Parse(PMTECTimeConstantSetBox.Text) > 1900))
					{
					}
					else
					{
						Rawvalue = (double.Parse(PMTECTimeConstantSetBox.Text) * 2250000);
						value1 = (UInt16)(Rawvalue / 65536);
						if (value1 >= 0)
						{
							value2 = (UInt16)(Rawvalue - value1 * 65536);
						}

						mainWindow.mbClient.WriteSingleRegister((byte)mySlave.ModbusID, 72, value1);
						mainWindow.mbClient.WriteSingleRegister((byte)mySlave.ModbusID, 73, value2);
					}
				}
				else
				{
					MessageBox.Show("Error: Invalid PM TEC Time Constant Value");

				}
				PMTECTimeConstantSetBox.Text = "";
			});
		}

		private void PMKpSetButton_Click(object sender, RoutedEventArgs e)
		{
			this.Dispatcher.Invoke(() =>
			{
				if (PMKpSetBox.Text.Length > 0 && IsTextNumeric(PMKpSetBox.Text.Replace(" ", "")) == true)
				{

					if ((double.Parse(PMKpSetBox.Text) < 0 || double.Parse(PMKpSetBox.Text) > 65535))
					{
					}
					else
					{
						mainWindow.mbClient.WriteSingleRegister((byte)mySlave.ModbusID, 75, UInt16.Parse(PMKpSetBox.Text)); ;
					}
				}
				else
				{
					MessageBox.Show("Error: Invalid PM Kp Value");

				}
				PMKpSetBox.Text = "";
			});
		}

		private void PMKdSetButton_Click(object sender, RoutedEventArgs e)
		{
			this.Dispatcher.Invoke(() =>
			{
				if (PMKdSetBox.Text.Length > 0 && IsTextNumeric(PMKdSetBox.Text.Replace(" ", "")) == true)
				{
					if ((double.Parse(PMKdSetBox.Text) < 0 || double.Parse(PMKdSetBox.Text) > 65535))
					{
					}
					else
					{
						mainWindow.mbClient.WriteSingleRegister((byte)mySlave.ModbusID, 77, UInt16.Parse(PMKdSetBox.Text)); ;
					}
				}
				else
				{
					MessageBox.Show("Error: Invalid PM KdValue");

				}
				PMKdSetBox.Text = "";
			});
		}

		private void PMKiSetButton_Click(object sender, RoutedEventArgs e)
		{
			this.Dispatcher.Invoke(() =>
			{
				if (PMKiSetBox.Text.Length > 0 && IsTextNumeric(PMKiSetBox.Text.Replace(" ", "")) == true)
				{

					if ((double.Parse(PMKiSetBox.Text) < 0 || double.Parse(PMKiSetBox.Text) > 65535))
					{
					}
					else
					{
						mainWindow.mbClient.WriteSingleRegister((byte)mySlave.ModbusID, 79, UInt16.Parse(PMKiSetBox.Text)); ;
					}
				}
				else
				{
					MessageBox.Show("Error: Invalid PM Ki Value");

				}
				PMKiSetBox.Text = "";
			});
		}


		private static bool IsTextNumeric(string str)
			{
				//System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
				//  return reg.IsMatch(str);

				//System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("^[0-9]([.][0-9]{1,3})?$");
				//return reg.IsMatch(str);
				Decimal dummy;
				return Decimal.TryParse(str, out dummy);

			}

		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel = true;
			//Do whatever you want here..
			this.Visibility = Visibility.Hidden;
		}

		private void CloseBut_Click(object sender, RoutedEventArgs e)
		{
			this.Visibility = Visibility.Hidden;
		}
	}
}