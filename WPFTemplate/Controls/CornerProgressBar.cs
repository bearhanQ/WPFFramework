﻿using Microsoft.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFTemplate.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFTemplate.Controls;assembly=WPFTemplate.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CornerProgressBar/>
    ///
    /// </summary>
    public class CornerProgressBar : RangeBase
    {
        private FrameworkElement _track;

        static CornerProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerProgressBar), new FrameworkPropertyMetadata(typeof(CornerProgressBar)));
        }

        public CornerProgressBar()
        {
            this.Loaded += CornerProgressBar_Loaded;
        }

        private void CornerProgressBar_Loaded(object sender, RoutedEventArgs e)
        {
            OnIsIndeterminateChanged(this, new DependencyPropertyChangedEventArgs());
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerProgressBar));

        public bool ShowPercentage
        {
            get { return (bool)GetValue(ShowPercentageProperty); }
            set { SetValue(ShowPercentageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowPercentage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowPercentageProperty =
            DependencyProperty.Register("ShowPercentage", typeof(bool), typeof(CornerProgressBar), new PropertyMetadata(true));

        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsIndeterminate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(CornerProgressBar),new FrameworkPropertyMetadata(false, OnIsIndeterminateChanged));

        public ProgressBarType ProgressBarType
        {
            get { return (ProgressBarType)GetValue(ProgressBarTypeProperty); }
            set { SetValue(ProgressBarTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressBarType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressBarTypeProperty =
            DependencyProperty.Register("ProgressBarType", typeof(ProgressBarType), typeof(CornerProgressBar), new PropertyMetadata(ProgressBarType.Normal));

        private static void OnIsIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var progressBar = d as CornerProgressBar;
            if (progressBar != null)
            {
                if (progressBar.Template != null)
                {
                    if (progressBar.IsIndeterminate)
                    {
                        if (progressBar.ProgressBarType == ProgressBarType.Normal)
                        {
                            var partTrackBorder = progressBar.Template.FindName("PART_Track", progressBar);
                            if (partTrackBorder != null)
                            {
                                var border = partTrackBorder as Border;
                                border.Clip = new RectangleGeometry
                                {
                                    Rect = new Rect { X = 0, Y = 0, Width = border.Width / 2, Height = border.Height },
                                    Transform = new TranslateTransform { X = 0 }
                                };
                                Storyboard storyboard = new Storyboard();
                                storyboard.RepeatBehavior = RepeatBehavior.Forever;
                                DoubleAnimation animation = new DoubleAnimation
                                {
                                    From = -border.Width,
                                    To = border.Width,
                                    Duration = TimeSpan.FromSeconds(2)
                                };
                                Storyboard.SetTarget(animation, border);
                                Storyboard.SetTargetProperty(animation, new PropertyPath("(Border.Clip).(RectangleGeometry.Transform).(TranslateTransform.X)"));
                                storyboard.Children.Add(animation);
                                storyboard.Begin();
                            }
                        }
                    }
                    else
                    {
                        if (progressBar.ProgressBarType == ProgressBarType.Normal)
                        {
                            var partTrackBorder = progressBar.Template.FindName("PART_Track", progressBar);
                            if (partTrackBorder != null)
                            {
                                var border = partTrackBorder as Border;
                                border.Clip = null;
                            }
                        }
                    }
                }
            }
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            if (!this.IsIndeterminate)
            {
                SetPartTrackValue();
            }
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            if (!this.IsIndeterminate)
            {
                SetPartTrackValue();
            }
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            if (!this.IsIndeterminate)
            {
                SetPartTrackValue();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._track = this.Template.FindName("PART_Track", this) as FrameworkElement;
        }

        private void SetPartTrackValue()
        {
            if (this.ProgressBarType == ProgressBarType.Normal)
            {
                double minimum = this.Minimum;
                double maximum = this.Maximum;
                double value = this.Value;
                double num = (IsIndeterminate || maximum <= minimum) ? 1.0 : ((value - minimum) / (maximum - minimum));

                var indicatorWidth = num * this.ActualWidth;

                if (_track != null)
                {
                    var border = _track as Border;
                    border.Clip = new RectangleGeometry
                    {
                        Rect = new Rect { X = 0, Y = 0, Width = indicatorWidth, Height = border.Height }
                    };
                }
            }

            if (this.ProgressBarType == ProgressBarType.Cycle)
            {
                double minimum = this.Minimum;
                double maximum = this.Maximum;
                double value = this.Value;
                double num = (maximum <= minimum) ? 1.0 : ((value - minimum) / (maximum - minimum));

                var EndAngle = num * 360;

                if (_track != null)
                {
                    var arc = _track as Arc;
                    arc.EndAngle = EndAngle;
                }
            }
        }
    }
}
