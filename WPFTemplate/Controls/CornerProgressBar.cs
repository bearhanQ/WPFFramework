using Microsoft.Expression.Shapes;
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
    public class CornerProgressBar : RangeBase
    {
        public static readonly DependencyProperty CornerRadiusProperty;

        public static readonly DependencyProperty ShowPercentageProperty;

        public static readonly DependencyProperty IsIndeterminateProperty;

        public static readonly DependencyProperty ProgressBarTypeProperty;

        private FrameworkElement _track;

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public bool ShowPercentage
        {
            get { return (bool)GetValue(ShowPercentageProperty); }
            set { SetValue(ShowPercentageProperty, value); }
        }
        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }
        public ProgressBarType ProgressBarType
        {
            get { return (ProgressBarType)GetValue(ProgressBarTypeProperty); }
            set { SetValue(ProgressBarTypeProperty, value); }
        }

        static CornerProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerProgressBar), new FrameworkPropertyMetadata(typeof(CornerProgressBar)));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerProgressBar));
            ShowPercentageProperty = DependencyProperty.Register("ShowPercentage", typeof(bool), typeof(CornerProgressBar), new PropertyMetadata(true));
            IsIndeterminateProperty = DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(CornerProgressBar), new FrameworkPropertyMetadata(false, OnIsIndeterminateChanged));
            ProgressBarTypeProperty = DependencyProperty.Register("ProgressBarType", typeof(ProgressBarType), typeof(CornerProgressBar), new PropertyMetadata(ProgressBarType.Normal));
        }
        public CornerProgressBar()
        {
            this.Loaded += CornerProgressBar_Loaded;
        }

        private void CornerProgressBar_Loaded(object sender, RoutedEventArgs e)
        {
            OnIsIndeterminateChanged(this, new DependencyPropertyChangedEventArgs());
        }
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
                }
            }
        }
        private void SetPartTrackValue()
        {
            if (!this.IsIndeterminate)
            {
                if (this.ProgressBarType == ProgressBarType.Normal)
                {
                    double minimum = this.Minimum;
                    double maximum = this.Maximum;
                    double value = this.Value;
                    double num = (IsIndeterminate || maximum <= minimum) ? 1.0 : ((value - minimum) / (maximum - minimum));

                    var indicatorWidth = num * this.Width;
                    var indicatorHegith = this.Height;

                    if (_track != null)
                    {
                        var border = _track as Border;

                        border.Clip = new RectangleGeometry
                        {
                            Rect = new Rect { X = 0, Y = 0, Width = indicatorWidth, Height = indicatorHegith }
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
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            SetPartTrackValue();
        }
        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            SetPartTrackValue();
        }
        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            SetPartTrackValue();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._track = this.Template.FindName("PART_Track", this) as FrameworkElement;
            SetPartTrackValue();
        }
    }
}
