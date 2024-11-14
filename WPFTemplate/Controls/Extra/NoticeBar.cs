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
    public class NoticeBar : ContentControl
    {
        private ContentPresenter contentPresenter { get; set; }

        private bool IsInDesignMode => System.ComponentModel.DesignerProperties.GetIsInDesignMode(this);

        static NoticeBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NoticeBar), new FrameworkPropertyMetadata(typeof(NoticeBar)));
        }

        public NoticeBar()
        {
            this.Loaded += (s, e) =>
            {
                Initialize();
            };
        }

        private void Initialize()
        {
            if (this.contentPresenter != null && !IsInDesignMode)
            {
                var width = contentPresenter.ActualWidth;
                contentPresenter.Margin = new Thickness
                {
                    Right = 0 - Math.Floor(width) - 5
                };
                CreateAnimation();
            }
        }

        private void CreateAnimation()
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames
            {
                KeyFrames = new DoubleKeyFrameCollection
                {
                    new EasingDoubleKeyFrame{ KeyTime = TimeSpan.Zero, Value = 0 },
                    new EasingDoubleKeyFrame{ KeyTime = TimeSpan.FromSeconds(8),Value = 0 - (this.ActualWidth + contentPresenter.ActualWidth + 5)}
                }
            };
            storyboard.RepeatBehavior = RepeatBehavior.Forever;
            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, contentPresenter);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.X)"));
            storyboard.Begin();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            contentPresenter = GetTemplateChild("contentPresenter") as ContentPresenter;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            Initialize();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            Initialize();
        }
    }
}
