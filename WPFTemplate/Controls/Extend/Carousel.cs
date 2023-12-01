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
    //[TemplatePart(Name = "PART_NextSelectedContentHost", Type = typeof(ContentPresenter))]
    public class Carousel : ItemsControl
    {
        static Carousel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Carousel), new FrameworkPropertyMetadata(typeof(Carousel)));
        }

        internal FrameworkElement ItemsPresenter => GetTemplateChild("itemsPresenter") as FrameworkElement;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var spmain = this.Template.FindName("SpMain", this) as StackPanel;
            spmain.Clip = new RectangleGeometry
            {
                Rect = new Rect
                {
                    X = 0,
                    Y = 0,
                    Width = this.Width,
                    Height = this.Height,
                }
            };
            Animation();
        }

        private void Animation()
        {
            Storyboard storyboard = new Storyboard();
            ThicknessAnimationUsingKeyFrames animation = new ThicknessAnimationUsingKeyFrames();
            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.KeyFrames = new ThicknessKeyFrameCollection();
            for (int i = 0; i < this.Items.Count; i++)
            {
                animation.KeyFrames.Add(new EasingThicknessKeyFrame
                {
                    KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, i)),
                    Value = new Thickness(0 - (this.Width * i), 0, 0, 0)
                });
            }
            Storyboard.SetTarget(animation, ItemsPresenter);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(FrameworkElement.Margin)"));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }
}
