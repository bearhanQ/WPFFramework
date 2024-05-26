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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class ChartItem : ContentControl
    {
        private static readonly DependencyPropertyKey ValuePropertyKey;

        public static readonly DependencyProperty ValueProperty;

        public bool IsInDesignMode => System.ComponentModel.DesignerProperties.GetIsInDesignMode(this);

        public double Value
        {
            get
            {
                return (double)GetValue(ValueProperty);
            }
        }

        private Chart ParentChart;

        static ChartItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartItem), new FrameworkPropertyMetadata(typeof(ChartItem)));
            ValuePropertyKey = DependencyProperty.RegisterReadOnly("Value", typeof(double), typeof(BarItem), new PropertyMetadata((double)0));
            ValueProperty = ValuePropertyKey.DependencyProperty;
        }

        public ChartItem()
        {
            this.Loaded += ChartItem_Loaded;
        }

        private void ChartItem_Loaded(object sender, RoutedEventArgs e)
        {
            if(ParentChart!=null && ParentChart.OpenAnimation)
            {
                CreateAnimation(0, this.ActualHeight, this);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ParentChart = LocalVisualTreeHelper.GetParent(this, typeof(Chart)) as Chart;
            CalculateValue();
        }

        private void CalculateValue()
        {
            var chart = LocalVisualTreeHelper.GetParent(this, typeof(Chart)) as Chart;
            if (chart != null && chart.ItemsSource != null && !string.IsNullOrWhiteSpace(chart.ValueMemberPath))
            {
                double value = 0;
                if (double.TryParse(Content.GetType().GetProperty(chart.ValueMemberPath).GetValue(Content).ToString(), out value))
                {
                    SetValue(ValuePropertyKey, value);
                }
            }
        }

        private void CreateAnimation(double from, double to, DependencyObject d)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.FillBehavior = FillBehavior.Stop;
            animation.KeyFrames.Add(new EasingDoubleKeyFrame { KeyTime = TimeSpan.Zero, Value = from });
            animation.KeyFrames.Add(new EasingDoubleKeyFrame { KeyTime = TimeSpan.FromMilliseconds(500), Value = to });
            Storyboard.SetTarget(animation, d);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }
}
