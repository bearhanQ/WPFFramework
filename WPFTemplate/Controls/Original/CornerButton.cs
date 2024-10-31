using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class CornerButton : Button
    {
        public static readonly DependencyProperty CornerRadiusProperty;

        public static readonly DependencyProperty ButtonTypeProperty;

        public static readonly DependencyProperty IconProperty;

        public static readonly DependencyProperty MouseOverForegroundProperty;

        public static readonly DependencyProperty LinkShowUnderLineProperty;

        public static readonly DependencyProperty PressShowShadowProperty;

        private Border _shadowBorder;

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public ButtonType ButtonType
        {
            get { return (ButtonType)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }
        public bool LinkShowUnderLine
        {
            get { return (bool)GetValue(LinkShowUnderLineProperty); }
            set { SetValue(LinkShowUnderLineProperty, value); }
        }
        public bool PressShowShadow
        {
            get { return (bool)GetValue(PressShowShadowProperty); }
            set { SetValue(PressShowShadowProperty, value); }
        }

        static CornerButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerButton), new FrameworkPropertyMetadata(typeof(CornerButton)));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerButton));
            ButtonTypeProperty = DependencyProperty.Register("ButtonType", typeof(ButtonType), typeof(CornerButton));
            IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(CornerButton), new PropertyMetadata(string.Empty));
            MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(CornerButton), new PropertyMetadata(Brushes.Black));
            LinkShowUnderLineProperty = DependencyProperty.Register("LinkShowUnderLine", typeof(bool), typeof(CornerButton), new PropertyMetadata(true));
            PressShowShadowProperty = DependencyProperty.Register("PressShowShadow", typeof(bool), typeof(CornerButton), new PropertyMetadata(true));

            //EventManager.RegisterClassHandler(typeof(CornerButton), UIElement.MouseEnterEvent, new RoutedEventHandler(OnMouseEnterEvent));
            //EventManager.RegisterClassHandler(typeof(CornerButton), UIElement.MouseLeaveEvent, new RoutedEventHandler(OnMouseLeaveEvent));
        }

        public static void OnMouseEnterEvent(object sender, RoutedEventArgs e)
        {
            var button = sender as CornerButton;
            if (button != null && button.ButtonType == ButtonType.Normal)
            {
                var shadowBoder = button._shadowBorder;
                if (shadowBoder != null)
                {
                    Storyboard storyboard = new Storyboard();
                    RectAnimationUsingKeyFrames animation = new RectAnimationUsingKeyFrames
                    {
                        KeyFrames = new RectKeyFrameCollection
                        {
                            new EasingRectKeyFrame
                            {
                                KeyTime = TimeSpan.FromSeconds(0),
                                Value = new Rect
                                {
                                    X = button.ActualWidth / 2,
                                    Y = 0,
                                    Width = 0,
                                    Height = button.ActualHeight
                                }
                            },
                            new EasingRectKeyFrame
                            {
                                KeyTime = TimeSpan.FromMilliseconds(300),
                                Value = new Rect
                                {
                                    X = 0,
                                    Y = 0,
                                    Width = button.ActualWidth,
                                    Height = button.ActualHeight
                                }
                            }
                        }
                    };
                    Storyboard.SetTarget(animation, button._shadowBorder);
                    Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.Clip).(RectangleGeometry.Rect)"));
                    storyboard.FillBehavior = FillBehavior.Stop;
                    storyboard.Children.Add(animation);
                    storyboard.Begin();
                }
            }
        }

        public static void OnMouseLeaveEvent(object sender, RoutedEventArgs e)
        {
            var button = sender as CornerButton;
            if (button != null && button.ButtonType == ButtonType.Normal)
            {
                button.CreateClip();
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _shadowBorder = this.GetTemplateChild("PART_ShadowBorder") as Border;
            CreateClip();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            CreateClip();
        }

        private void CreateClip()
        {
            //if (_shadowBorder != null)
            //{
            //    var X = this.ActualWidth;
            //    var Height = this.ActualHeight;
            //    _shadowBorder.Clip = new RectangleGeometry
            //    {
            //        Rect = new Rect
            //        {
            //            X = X / 2,
            //            Y = 0,
            //            Width = 0,
            //            Height = Height
            //        }
            //    };
            //}
        }
    }
}
