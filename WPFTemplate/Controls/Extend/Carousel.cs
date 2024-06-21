using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    //[TemplatePart(Name = "PART_NextSelectedContentHost", Type = typeof(ContentPresenter))]
    public class Carousel : ItemsControl
    {
        public static readonly DependencyProperty SelectedIndexProperty;

        public static readonly DependencyProperty IsAutoSwitchProperty;

        public static readonly DependencyProperty IntervalProperty;

        public static readonly DependencyProperty ShowBottomPageProperty;

        public static readonly DependencyProperty PauseOnMouseOverProperty;

        private Grid gridMain;

        internal FrameworkElement ItemsPresenter => GetTemplateChild("itemsPresenter") as FrameworkElement;

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public bool IsAutoSwitch
        {
            get { return (bool)GetValue(IsAutoSwitchProperty); }
            set { SetValue(IsAutoSwitchProperty, value); }
        }

        [TypeConverter(typeof(TimeSpanConverter))]
        public TimeSpan Interval
        {
            get { return (TimeSpan)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        public bool ShowBottomPage
        {
            get { return (bool)GetValue(ShowBottomPageProperty); }
            set { SetValue(ShowBottomPageProperty, value); }
        }

        public bool PauseOnMouseOver
        {
            get { return (bool)GetValue(PauseOnMouseOverProperty); }
            set { SetValue(PauseOnMouseOverProperty, value); }
        }

        static Carousel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Carousel), new FrameworkPropertyMetadata(typeof(Carousel)));
            SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(Carousel),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(OnIndexChanged), null, false, UpdateSourceTrigger.PropertyChanged));
            IsAutoSwitchProperty = DependencyProperty.Register("IsAutoSwitch", typeof(bool), typeof(Carousel),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnIsAutoSwitchChanged), null, false, UpdateSourceTrigger.PropertyChanged));
            IntervalProperty = DependencyProperty.Register("Interval", typeof(TimeSpan), typeof(Carousel), new PropertyMetadata(new TimeSpan(0, 0, 1)));
            ShowBottomPageProperty = DependencyProperty.Register("ShowBottomPage", typeof(bool), typeof(Carousel), new PropertyMetadata(true));
            PauseOnMouseOverProperty = DependencyProperty.Register("PauseOnMouseOver", typeof(bool), typeof(Carousel), new PropertyMetadata(true));
        }

        public Carousel()
        {
            this.Loaded += Carousel_Loaded;
        }

        private void Carousel_Loaded(object sender, RoutedEventArgs e)
        {
            if (gridMain != null)
            {
                gridMain.Clip = new RectangleGeometry
                {
                    Rect = new Rect
                    {
                        X = 0,
                        Y = 0,
                        Width = this.ActualWidth,
                        Height = this.ActualHeight,
                    }
                };
            }
            OffSetChildItemsSize();
        }

        private void OffSetChildItemsSize()
        {
            if (this.HasItems)
            {
                foreach (var item in this.Items)
                {
                    var c = item as FrameworkElement;
                    if (c != null)
                    {
                        c.Width = this.ActualWidth;
                        c.Height = this.ActualHeight;
                    }
                }
            }
        }

        private static void OnIsAutoSwitchChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var carousel = sender as Carousel;
            if (carousel != null)
            {
                carousel.RepeatAnimation();
            }
        }

        private static void OnIndexChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (int)args.OldValue;
            var newValue = (int)args.NewValue;
            var carousel = sender as Carousel;
            if (carousel != null)
            {
                carousel.BeginAnimation(oldValue, newValue);
            }
        }

        private void BeginAnimation(int oldIndex, int newIndex)
        {
            if (ItemsPresenter != null)
            {
                Storyboard storyboard = new Storyboard();
                ThicknessAnimationUsingKeyFrames animation = new ThicknessAnimationUsingKeyFrames();
                animation.KeyFrames = new ThicknessKeyFrameCollection();

                animation.KeyFrames.Add(new EasingThicknessKeyFrame
                {
                    KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0)),
                    Value = new Thickness(0 - (this.ActualWidth * oldIndex), 0, 0, 0)
                });

                animation.KeyFrames.Add(new EasingThicknessKeyFrame
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000)),
                    Value = new Thickness(0 - (this.ActualWidth * newIndex), 0, 0, 0)
                });

                Storyboard.SetTarget(animation, ItemsPresenter);
                Storyboard.SetTargetProperty(animation, new PropertyPath("(FrameworkElement.Margin)"));
                storyboard.Children.Add(animation);
                storyboard.Begin();
            }
        }

        private void RepeatAnimation()
        {
            this.Dispatcher.BeginInvoke(new Action(async () =>
            {
                while (IsAutoSwitch)
                {
                    if (!PauseOnMouseOver)
                    {
                        if (this.SelectedIndex == this.Items.Count - 1)
                        {
                            this.SelectedIndex = 0;
                        }
                        else
                        {
                            if (gridMain != null)
                            {
                                this.SelectedIndex++;
                            }
                        }
                        await Task.Delay(Interval);
                    }
                    else
                    {
                        if (!this.IsMouseOver)
                        {
                            if (this.SelectedIndex == this.Items.Count - 1)
                            {
                                this.SelectedIndex = 0;
                            }
                            else
                            {
                                if (gridMain != null)
                                {
                                    this.SelectedIndex++;
                                }
                            }
                            await Task.Delay(Interval);
                        }
                        else
                        {
                            await Task.Delay(500);
                        }
                    }
                }
            }));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            gridMain = this.Template.FindName("gridMain", this) as Grid;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            if (gridMain != null)
            {
                gridMain.Clip = new RectangleGeometry
                {
                    Rect = new Rect
                    {
                        X = 0,
                        Y = 0,
                        Width = this.ActualWidth,
                        Height = this.ActualHeight,
                    }
                };
            }
            OffSetChildItemsSize();
        }
    }
}
