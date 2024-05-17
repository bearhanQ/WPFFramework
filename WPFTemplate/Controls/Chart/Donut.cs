using Microsoft.Expression.Shapes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public partial class Donut : ItemsControl
    {
        public static readonly DependencyProperty ValueMemberPathProperty;

        public static readonly DependencyProperty ArcThicknessProperty;

        private ListBox listBoxMain;

        private Grid gridMain;

        public string ValueMemberPath
        {
            get { return (string)GetValue(ValueMemberPathProperty); }
            set { SetValue(ValueMemberPathProperty, value); }
        }

        public double ArcThickness
        {
            get { return (double)GetValue(ArcThicknessProperty); }
            set { SetValue(ArcThicknessProperty, value); }
        }

        static Donut()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Donut), new FrameworkPropertyMetadata(typeof(Donut)));
            ValueMemberPathProperty = DependencyProperty.Register("ValueMemberPath", typeof(string), typeof(Donut));
            ArcThicknessProperty = DependencyProperty.Register("ArcThickness", typeof(double), typeof(Donut), new PropertyMetadata((double)20));
        }

        public Donut()
        {
            this.Loaded += Donut_Loaded;
        }

        private void Donut_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateItem();
            GenerateArcs();
        }
        private void GenerateItem()
        {
            if (listBoxMain != null)
            {
                for (int i = 0; i < listBoxMain.Items.Count; i++)
                {
                    var item = listBoxMain.Items[i];
                    ListBoxItem listboxitem = listBoxMain.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                    if (listboxitem != null)
                    {
                        listboxitem.Background = this.DonutColors[i].Fill;
                        listboxitem.BorderBrush = this.DonutColors[i].Stroke;
                    }
                }
            }
        }
        private void GenerateArcs()
        {
            if (this.ItemsSource != null && this.ValueMemberPath != null)
            {
                double sum = 0;
                foreach (var item in this.ItemsSource)
                {
                    double value = 0;
                    if (double.TryParse(item.GetType().GetProperty(this.ValueMemberPath).GetValue(item).ToString(), out value))
                    {
                        sum += value;
                    }
                }

                if (gridMain != null)
                {
                    double startAngle = 0;
                    double value = 0;
                    double nexValue = 0;
                    for (int i = 0; i < listBoxMain.Items.Count; i++)
                    {
                        var item = listBoxMain.Items[i];
                        if (double.TryParse(item.GetType().GetProperty(this.ValueMemberPath).GetValue(item).ToString(), out value))
                        {
                            nexValue += value;
                            double endAngle = CalculateAngle(sum, nexValue);
                            Arc arc = GenerateArc(startAngle, endAngle, i);
                            gridMain.Children.Add(arc);
                            startAngle = endAngle;
                        }
                    }
                }
            }
        }
        private double CalculateAngle(double sum, double value)
        {
            double minimum = 0;
            double maximum = sum;
            double num = (maximum <= minimum) ? 1.0 : ((value - minimum) / (maximum - minimum));
            var EndAngle = num * 360;
            return EndAngle;
        }
        private Arc GenerateArc(double StartAngle, double EndAngle, int index)
        {
            Arc arc = new Arc();
            arc.Width = gridMain.ActualHeight;
            arc.Height = gridMain.ActualHeight;
            arc.Stroke = this.DonutColors[index].Stroke;
            arc.Fill = this.DonutColors[index].Fill;
            arc.StartAngle = StartAngle;
            arc.EndAngle = EndAngle;
            arc.ArcThickness = this.ArcThickness;
            arc.Stretch = Stretch.None;
            return arc;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            listBoxMain = GetTemplateChild("listBoxMain") as ListBox;
            gridMain = GetTemplateChild("gridMain") as Grid;
        }
    }

    public partial class Donut
    {
        private List<DonutColor> _DonutColors;
        private List<DonutColor> DonutColors
        {
            get
            {
                if (_DonutColors == null)
                {
                    _DonutColors = new List<DonutColor>()
                    {
                        new DonutColor
                        {
                            Stroke = new SolidColorBrush(Colors.Red),
                            Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFB1C1"))
                        },
                        new DonutColor
                        {
                            Stroke = new SolidColorBrush(Colors.Blue),
                            Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9AD0F5"))
                        },
                        new DonutColor
                        {
                            Stroke = new SolidColorBrush(Colors.Green),
                            Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF58E658"))
                        },
                        new DonutColor
                        {
                            Stroke = new SolidColorBrush(Colors.Yellow),
                            Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF1F14F"))
                        }
                    };
                }
                return _DonutColors;
            }
        }
    }

    internal class DonutColor
    {
        public SolidColorBrush Stroke { get; set; }
        public SolidColorBrush Fill { get; set; }
    }
}
