using Microsoft.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class Donut : ItemsControl
    {
        public static readonly DependencyProperty ValueMemberPathProperty;

        public static readonly DependencyProperty ArcThicknessProperty;

        public static readonly DependencyProperty DonutColorsProperty;

        public static readonly DependencyProperty OpenAnimationProperty;

        private ListBox listBoxMain;

        private Grid gridMain;

        private static ObservableCollection<Brush> DonutDefalutColors
        {
            get
            {
                return new ObservableCollection<Brush>()
                {
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFB1C1")),
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF9BE51")),
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFDFD6B")),
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF22EE22")),
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4AF7F7")),
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5151F4")),
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEE56EE"))
                };
            }
        }

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

        public bool OpenAnimation
        {
            get { return (bool)GetValue(OpenAnimationProperty); }
            set { SetValue(OpenAnimationProperty, value); }
        }

        [TypeConverter(typeof(StringToBrushCollectionTypeConverter))]
        public ObservableCollection<Brush> DonutColors
        {
            get { return (ObservableCollection<Brush>)GetValue(DonutColorsProperty); }
            set { SetValue(DonutColorsProperty, value); }
        }

        static Donut()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Donut), new FrameworkPropertyMetadata(typeof(Donut)));
            ValueMemberPathProperty = DependencyProperty.Register("ValueMemberPath", typeof(string), typeof(Donut));
            ArcThicknessProperty = DependencyProperty.Register("ArcThickness", typeof(double), typeof(Donut), 
                new FrameworkPropertyMetadata((double)20, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,new PropertyChangedCallback(ArcThicknessChangedCallBack)));
            DonutColorsProperty = DependencyProperty.Register("DonutColors", typeof(ObservableCollection<Brush>), typeof(Donut),new FrameworkPropertyMetadata(DonutDefalutColors));
            OpenAnimationProperty = DependencyProperty.Register("OpenAnimation", typeof(bool), typeof(Donut), new PropertyMetadata(true));
        }

        public Donut()
        {
            this.Loaded += Donut_Loaded;
        }

        private static void ArcThicknessChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs arg)
        {
            var donut = d as Donut;
            donut.LoadArcs();
        }
        private void Donut_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDisplayItem();
            LoadArcs();
        }
        private void LoadDisplayItem()
        {
            if (listBoxMain != null)
            {
                for (int i = 0; i < listBoxMain.Items.Count; i++)
                {
                    var item = listBoxMain.Items[i];
                    ListBoxItem listboxitem = listBoxMain.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                    if (listboxitem != null)
                    {
                        listboxitem.Background = this.DonutColors[i];
                    }
                }
            }
        }
        private void LoadArcs()
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
                    gridMain.Children.Clear();
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
                            Arc arc = GenerateArc(startAngle, endAngle, i, value);
                            Arc tag = GenerateTagArc(startAngle, endAngle);
                            arc.Tag = tag;

                            gridMain.Children.Add(arc);
                            gridMain.Children.Add(tag);

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
        private Arc GenerateArc(double StartAngle, double EndAngle, int index, double value)
        {
            var Fill = this.DonutColors[index];

            Arc arc = new Arc();
            arc.Width = gridMain.ActualHeight;
            arc.Height = gridMain.ActualHeight;
            arc.Fill = Fill;
            arc.StartAngle = StartAngle;
            arc.EndAngle = EndAngle;
            arc.ArcThickness = this.ArcThickness;
            arc.Stretch = Stretch.None;

            arc.MouseEnter += (sender, e) =>
            {
                var tagArc = ((Arc)sender).Tag as Arc;
                tagArc.Visibility = Visibility.Visible;
            };
            arc.MouseLeave += (sender, e) =>
            {
                var tagArc = ((Arc)sender).Tag as Arc;
                tagArc.Visibility = Visibility.Hidden;
            };

            var item = listBoxMain.Items[index];
            var content = item.GetType().GetProperty(this.DisplayMemberPath).GetValue(item).ToString();
            content = content + ": " + value;
            CornerToolTip toolTip = new CornerToolTip();
            toolTip.Background = Fill;
            toolTip.Foreground = Brushes.White;
            toolTip.Content = content;
            arc.ToolTip = toolTip;

            if (OpenAnimation)
            {
                CreateArcAnimation(StartAngle, EndAngle, arc);
            }

            return arc;
        }
        private Arc GenerateTagArc(double StartAngle, double EndAngle)
        {
            Arc arc = new Arc();
            arc.Width = gridMain.ActualHeight;
            arc.Height = gridMain.ActualHeight;
            arc.Fill = Brushes.Gray;
            arc.StartAngle = StartAngle;
            arc.EndAngle = EndAngle;
            arc.ArcThickness = this.ArcThickness;
            arc.Stretch = Stretch.None;
            arc.Visibility = Visibility.Hidden;
            arc.Opacity = 0.3;
            arc.IsHitTestVisible = false;
            return arc;
        }
        private void CreateArcAnimation(double from, double to,DependencyObject d)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.KeyFrames.Add(new EasingDoubleKeyFrame { KeyTime = TimeSpan.Zero, Value = from });
            animation.KeyFrames.Add(new EasingDoubleKeyFrame { KeyTime = TimeSpan.FromSeconds(1), Value = to });
            Storyboard.SetTarget(animation, d);
            Storyboard.SetTargetProperty(animation, new PropertyPath("EndAngle"));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            listBoxMain = GetTemplateChild("listBoxMain") as ListBox;
            gridMain = GetTemplateChild("gridMain") as Grid;
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            LoadArcs();
        }
    }
}
