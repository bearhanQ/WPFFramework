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
    public class BarItem : ContentControl
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

        private Bar ParentBar;

        static BarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BarItem), new FrameworkPropertyMetadata(typeof(BarItem)));
            ValuePropertyKey = DependencyProperty.RegisterReadOnly("Value", typeof(double), typeof(BarItem), new PropertyMetadata((double)0));
            ValueProperty = ValuePropertyKey.DependencyProperty;
        }

        public BarItem()
        {
            
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ParentBar = LocalVisualTreeHelper.GetParent(this, typeof(Bar)) as Bar;
            GenerateBarItem();
        }

        private void GenerateBarItem()
        {
            var bar = LocalVisualTreeHelper.GetParent(this, typeof(Bar)) as Bar;
            if (bar != null && bar.ItemsSource != null && !string.IsNullOrWhiteSpace(bar.ValueMemberPath))
            {
                double value = 0;
                if (double.TryParse(Content.GetType().GetProperty(bar.ValueMemberPath).GetValue(Content).ToString(), out value))
                {
                    var barItem = bar.ItemContainerGenerator.ContainerFromItem(Content) as BarItem;
                    var rectangle = barItem.Template.FindName("PART_Rectangle", barItem) as Rectangle;
                    var textBlock = barItem.Template.FindName("TbContent", barItem) as TextBlock;
                    if (rectangle != null)
                    {
                        rectangle.Height = value;
                        if(textBlock != null)
                        {
                            textBlock.Text = value.ToString();
                        }

                        if (ParentBar != null && ParentBar.OpenAnimation && !IsInDesignMode)
                        {
                            CreateAnimation(0, rectangle.Height, rectangle);
                        }
                    }
                    SetValue(ValuePropertyKey, value);
                }
            }
        }

        private void CreateAnimation(double from, double to, DependencyObject d)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            animation.KeyFrames.Add(new EasingDoubleKeyFrame { KeyTime = TimeSpan.Zero, Value = from });
            animation.KeyFrames.Add(new EasingDoubleKeyFrame { KeyTime = TimeSpan.FromMilliseconds(500), Value = to });
            Storyboard.SetTarget(animation, d);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Height"));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }
}
