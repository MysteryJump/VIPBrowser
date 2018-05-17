using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YoutubePlayer
{
	/// <summary>
	/// Paint.xaml の相互作用ロジック
	/// </summary>
	public partial class Paint : UserControl
	{
		public static readonly DependencyProperty DraggedProperty =
	DependencyProperty.RegisterAttached("Dragged", typeof(bool), typeof(MainWindow),
	new PropertyMetadata(false));

		public static void SetDragged(DependencyObject target, bool value)
		{
			target.SetValue(DraggedProperty, value);
		}
		public static bool GetDragged(DependencyObject target)
		{
			return (bool)target.GetValue(DraggedProperty);
		}


		public static readonly DependencyProperty StartPointProperty =
			DependencyProperty.RegisterAttached("StartPoint",
			typeof(Point),
			typeof(MainWindow),
			new UIPropertyMetadata(new Point()));



		public Brush SelectColor
		{
			get { return (Brush)GetValue(SelectColorProperty); }
			set { SetValue(SelectColorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SelectColorProperty =
			DependencyProperty.Register("Background", typeof(Brush), typeof(Button), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

		

		public static Point GetStartPoint(DependencyObject obj)
		{
			return (Point)obj.GetValue(StartPointProperty);
		}

		public static void SetStartPoint(DependencyObject obj, Point value)
		{
			obj.SetValue(StartPointProperty, value);
		}

		public Paint()
		{
			InitializeComponent();
			this.DataContext = this;
			this.SelectColor = new SolidColorBrush(Colors.Black);
			this.FontFamily = new FontFamily("Meiryo UI");
		}

		private void One_MouseDown(object sender, MouseButtonEventArgs e)
		{
			SetDragged(this.One, true);
			SetStartPoint(this.One, e.GetPosition(this.One));
		}

		private void One_MouseMove(object sender, MouseEventArgs e)
		{
			if (!GetDragged(this.One))
			{
				return;
			}

			Point prev = GetStartPoint(this.One);
			Point current = e.GetPosition(this.One);
			
			Line line = new Line();
		
			line.StrokeThickness = brushSize;
			line.Stroke = SelectColor;
			line.X1 = prev.X;
			line.Y1 = prev.Y;
			line.X2 = current.X;
			line.Y2 = current.Y;
			this.One.Children.Add(line);

			SetStartPoint(this.One, current);
		}

		private void One_MouseUp(object sender, MouseButtonEventArgs e)
		{
			SetDragged(this.One, false);
		}

		private void SelectColorButton_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
			if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Color c = Color.FromArgb(r: cd.Color.R, g: cd.Color.G, b: cd.Color.B, a: cd.Color.A);
				this.SelectColor = this.SelectColorButton.Background = new SolidColorBrush(c);
			}
		}
		private double brushSize;
		private void BrushSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			this.brushSize = e.NewValue;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{


			Application a = Application.Current;
			a.Shutdown(0);
		}

		public Canvas CanvasData { get { return this.One; } }

	}
}
