#pragma checksum "..\..\Modulation.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E64664A4C5974EF8F4BB1B12629FBBDE9443D654226DBD9ADDDCD362888E3B9F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace BestSledWPFG1 {
    
    
    /// <summary>
    /// Modulation
    /// </summary>
    public partial class Modulation : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 64 "..\..\Modulation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Viewbox MainViewbox;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\Modulation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EnableBut;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\Modulation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ModFreqEdit;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\Modulation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeModFreqBut;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\Modulation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DutyCycleEdit;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\Modulation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeDutyCycleBut;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\Modulation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Mod1;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\Modulation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DcOff1;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\Modulation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse SLED1Indicator;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\Modulation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse ModulationModeIndicator;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\Modulation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Close_But;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BestSledWPFG1;component/modulation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Modulation.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainViewbox = ((System.Windows.Controls.Viewbox)(target));
            return;
            case 2:
            this.EnableBut = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\Modulation.xaml"
            this.EnableBut.Click += new System.Windows.RoutedEventHandler(this.Modulation_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ModFreqEdit = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ChangeModFreqBut = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\Modulation.xaml"
            this.ChangeModFreqBut.Click += new System.Windows.RoutedEventHandler(this.modulationFreq_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DutyCycleEdit = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.ChangeDutyCycleBut = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\Modulation.xaml"
            this.ChangeDutyCycleBut.Click += new System.Windows.RoutedEventHandler(this.dutyCycle_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Mod1 = ((System.Windows.Controls.Button)(target));
            
            #line 83 "..\..\Modulation.xaml"
            this.Mod1.Click += new System.Windows.RoutedEventHandler(this.Modulation_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DcOff1 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.SLED1Indicator = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 10:
            this.ModulationModeIndicator = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 11:
            this.Close_But = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\Modulation.xaml"
            this.Close_But.Click += new System.Windows.RoutedEventHandler(this.CloseBut_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

