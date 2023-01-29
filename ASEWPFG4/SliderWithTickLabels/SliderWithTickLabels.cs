﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SliderWithTickLabels
{
    /// <summary>
    /// Siga os passos 1a ou 1b e depois 2 para usar esse controle personalizado em um arquivo XAML.
    ///
    /// Passo 1a) Usando o controle personalizado em um arquivo XAML que já existe no projeto atual.
    /// Adicione o atributo XmlNamespace ao elemento raiz do arquivo de marcação onde ele 
    /// deve ser usado:
    ///
    ///     xmlns:MyNamespace="clr-namespace:SliderWithTickLabels"
    ///
    ///
    /// Passo 1b) Usando o controle personalizado em um arquivo XAML que existe em um projeto diferente.
    /// Adicione o atributo XmlNamespace ao elemento raiz do arquivo de marcação onde ele 
    /// deve ser usado:
    ///
    ///     xmlns:MyNamespace="clr-namespace:SliderWithTickLabels;assembly=SliderWithTickLabels"
    ///
    /// Também será necessário adicionar nesse projeto uma referência ao projeto que contém esse arquivo XAML
    /// e Recompilar para evitar erros de compilação:
    ///
    ///     No Gerenciador de Soluções, clique com o botão direito no projeto alvo e
    ///     "Adicionar Referência"->"Projetos"->[Selecione esse projeto]
    ///
    ///
    /// Passo 2)
    /// Vá em frente e use seu controle no arquivo XAML.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class SliderWithTickLabels : Slider
    {
		public static readonly DependencyProperty GeneratedTicksProperty;
		public static readonly DependencyProperty TickLabelTemplateProperty;

		protected override void OnRender(DrawingContext dc)

		{
			Size size = new Size(base.ActualWidth - 2, base.ActualHeight);

			double tickCount = (int)((this.Maximum - this.Minimum) / this.TickFrequency) + 1;

			if ((this.Maximum - this.Minimum) % this.TickFrequency == 0)

				tickCount -= 1;

			Double tickFrequencySize;

			// Calculate tick's setting

			tickFrequencySize = (size.Width * this.TickFrequency / (this.Maximum - this.Minimum));

			string text = "";

			FormattedText formattedText = null;

			double num = this.Maximum - this.Minimum;

			int i = 0;

			// Draw each tick text

			RotateTransform RT = new RotateTransform();
			RT.Angle = 90;
			dc.PushTransform(RT);

			for (i = 0; i <= tickCount; i++)

			{
				text = Convert.ToString(Convert.ToInt32(this.Minimum + this.TickFrequency * i), 10);

				formattedText = new FormattedText(text, System.Globalization.CultureInfo.GetCultureInfo("en-us"), FlowDirection.RightToLeft, new Typeface("Calibri"), 9, Brushes.Black);
				formattedText.TextAlignment = TextAlignment.Center;

				dc.DrawText(formattedText, new Point(20, -(tickFrequencySize * i) - 13));
				//dc.DrawText(formattedText, new Point(0, (tickFrequencySize * i)));
			}

		}
		

		[Bindable(true)]
		public DoubleCollection GeneratedTicks
		{
			get
			{
				return base.GetValue(SliderWithTickLabels.GeneratedTicksProperty) as DoubleCollection;
			}
			set
			{
				base.SetValue(SliderWithTickLabels.GeneratedTicksProperty, value);
			}
		}

		[Bindable(true)]
		public DataTemplate TickLabelTemplate
		{
			get
			{
				return base.GetValue(SliderWithTickLabels.TickLabelTemplateProperty) as DataTemplate;
			}
			set
			{
				base.SetValue(SliderWithTickLabels.TickLabelTemplateProperty, value);
			}
		}

		static SliderWithTickLabels()
        {
			SliderWithTickLabels.GeneratedTicksProperty = DependencyProperty.Register("GeneratedTicks", typeof(DoubleCollection), typeof(SliderWithTickLabels), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
			SliderWithTickLabels.TickLabelTemplateProperty = DependencyProperty.Register("TickLabelTemplate", typeof(DataTemplate), typeof(SliderWithTickLabels), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SliderWithTickLabels), new FrameworkPropertyMetadata(typeof(SliderWithTickLabels)));
        }

		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(e);

			var propertyNames = new string[] { "Minimum", "Maximum", "TickFrequency", "Ticks", "IsDirectionReversed" };

			if (IsInitialized && propertyNames.Contains(e.Property.Name))
			{
				CalculateTicks();
			}
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			CalculateTicks();
		}

		protected override Geometry GetLayoutClip(Size layoutSlotSize)
		{
			return ClipToBounds ? base.GetLayoutClip(layoutSlotSize) : null;
		}

		private void CalculateTicks()
		{
			if (this.Ticks != null && this.Ticks.Any())
			{
				this.GeneratedTicks = new DoubleCollection(this.Ticks.Union(new double[] { this.Minimum, this.Maximum }).Where(t => this.Minimum <= t && t <= this.Maximum));
			}
			else
			{
				if (this.TickFrequency > 0.0)
				{
					this.GeneratedTicks = new DoubleCollection(
						Enumerable.Range(
							0,
							Convert.ToInt32(
								Math.Ceiling((this.Maximum - this.Minimum) / this.TickFrequency)
							) + 1
						)
						.Select(t => Math.Min(t * this.TickFrequency + this.Minimum, this.Maximum))
					);
				}
			}
		}
    }
}
