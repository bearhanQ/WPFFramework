using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    public class CornerExpander : Expander
    {
        public static readonly DependencyProperty CornerRadiusProperty;

        private ToggleButton HeaderSite;

        private FrameworkElement HeaderPresenter;

        private FrameworkElement ExpandSite;

        private double HeaderPresenterActualHeight;

        private double HeaderPresenterActualWidth;

        private double ExpandSiteActualHeight;

        private double ExpandSiteActualWidth;

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        static CornerExpander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerExpander), new FrameworkPropertyMetadata(typeof(CornerExpander)));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerExpander));
        }

        public CornerExpander()
        {
            this.Loaded += CornerExpander_Loaded;
        }

        private void ClearControls()
        {
            HeaderSite = null;
            HeaderPresenter = null;
            ExpandSite = null;
        }
        private void CornerExpander_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControls();

            HeaderSite = GetTemplateChild("HeaderSite") as ToggleButton;
            ExpandSite = GetTemplateChild("ExpandSite") as FrameworkElement;

            if (HeaderSite != null)
            {
                HeaderPresenter = HeaderSite.Template.FindName("headerPresenter", HeaderSite) as FrameworkElement;

                HeaderSite.Checked += HeaderSite_Checked;
                HeaderSite.Unchecked += HeaderSite_Unchecked;
            }

            LoadHeaderPresenter();
            LoadExpandSite();

            this.MouseEnter += CornerExpander_MouseEnter;
            this.MouseLeave += CornerExpander_MouseLeave;
        }

        private void LoadHeaderPresenter()
        {
            if (HeaderPresenter != null)
            {
                HeaderPresenterActualHeight = HeaderPresenter.ActualHeight;
                HeaderPresenterActualWidth = HeaderPresenter.ActualWidth;
                if (this.ExpandDirection == ExpandDirection.Left || this.ExpandDirection == ExpandDirection.Right)
                {
                    HeaderPresenter.Width = 0;
                }
                else
                {
                    HeaderPresenter.Height = 0;
                }
            }
        }
        private void LoadExpandSite()
        {
            if (ExpandSite != null)
            {
                ExpandSiteActualHeight = ExpandSite.ActualHeight;
                ExpandSiteActualWidth = ExpandSite.ActualWidth;
                if (!IsExpanded)
                {
                    if (this.ExpandDirection == ExpandDirection.Left || this.ExpandDirection == ExpandDirection.Right)
                    {
                        ExpandSite.Width = 0;
                    }
                    else
                    {
                        ExpandSite.Height = 0;
                    }
                }
            }
        }
        private void HeaderSite_Unchecked(object sender, RoutedEventArgs e)
        {
            HeaderSiteUncheckedAnimation();
        }
        private void HeaderSite_Checked(object sender, RoutedEventArgs e)
        {
            HeaderSiteCheckedAnimation();
        }
        private void HeaderSiteUncheckedAnimation()
        {
            if (ExpandSite != null)
            {
                if (this.ExpandDirection == ExpandDirection.Up || this.ExpandDirection == ExpandDirection.Down)
                {
                    CreateStoryboard(ExpandSiteActualHeight, 0, ExpandSite, FrameworkElement.HeightProperty);
                }
                if (this.ExpandDirection == ExpandDirection.Left || this.ExpandDirection == ExpandDirection.Right)
                {
                    CreateStoryboard(ExpandSiteActualWidth, 0, ExpandSite, FrameworkElement.WidthProperty);
                }
            }
        }
        private void HeaderSiteCheckedAnimation()
        {
            if (ExpandSite != null)
            {
                if (this.ExpandDirection == ExpandDirection.Up || this.ExpandDirection == ExpandDirection.Down)
                {
                    Storyboard storyboard = new Storyboard();
                    DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
                    EasingDoubleKeyFrame key1 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.Zero));
                    EasingDoubleKeyFrame key2 = new EasingDoubleKeyFrame(ExpandSiteActualHeight, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200)));
                    animation.KeyFrames.Add(key1);
                    animation.KeyFrames.Add(key2);
                    Storyboard.SetTarget(animation, ExpandSite);
                    Storyboard.SetTargetProperty(animation, new PropertyPath("(FrameworkElement.Height)"));
                    storyboard.Children.Add(animation);
                    storyboard.Begin();
                }
                if (this.ExpandDirection == ExpandDirection.Left || this.ExpandDirection == ExpandDirection.Right)
                {
                    Storyboard storyboard = new Storyboard();
                    DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
                    EasingDoubleKeyFrame key1 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.Zero));
                    EasingDoubleKeyFrame key2 = new EasingDoubleKeyFrame(ExpandSiteActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200)));
                    animation.KeyFrames.Add(key1);
                    animation.KeyFrames.Add(key2);
                    Storyboard.SetTarget(animation, ExpandSite);
                    Storyboard.SetTargetProperty(animation, new PropertyPath("(FrameworkElement.Width)"));
                    storyboard.Children.Add(animation);
                    storyboard.Begin();
                }
            }
        }
        private void CornerExpander_MouseLeave(object sender, MouseEventArgs e)
        {
            CornerExpanderMouseLeaveAnimation();

        }
        private void CornerExpander_MouseEnter(object sender, MouseEventArgs e)
        {
            CornerExpanderMouseEnterAnimation();
        }
        private void CornerExpanderMouseEnterAnimation()
        {
            if (HeaderPresenter != null)
            {
                if (this.ExpandDirection == ExpandDirection.Up || this.ExpandDirection == ExpandDirection.Down)
                {
                    CreateStoryboard(0, HeaderPresenterActualHeight, HeaderPresenter, FrameworkElement.HeightProperty);
                }
                if (this.ExpandDirection == ExpandDirection.Left || this.ExpandDirection == ExpandDirection.Right)
                {
                    CreateStoryboard(0, HeaderPresenterActualWidth, HeaderPresenter, FrameworkElement.WidthProperty);
                }
            }
        }
        private void CornerExpanderMouseLeaveAnimation()
        {
            if (HeaderPresenter != null)
            {
                if (this.ExpandDirection == ExpandDirection.Up || this.ExpandDirection == ExpandDirection.Down)
                {

                    CreateStoryboard(HeaderPresenterActualHeight, 0, HeaderPresenter, FrameworkElement.HeightProperty);
                }
                if (this.ExpandDirection == ExpandDirection.Left || this.ExpandDirection == ExpandDirection.Right)
                {
                    CreateStoryboard(HeaderPresenterActualWidth, 0, HeaderPresenter, FrameworkElement.WidthProperty);
                }
            }
        }
        private void CreateStoryboard(double from, double to, FrameworkElement target, DependencyProperty property)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();
            EasingDoubleKeyFrame key1 = new EasingDoubleKeyFrame(from, KeyTime.FromTimeSpan(TimeSpan.Zero));
            EasingDoubleKeyFrame key2 = new EasingDoubleKeyFrame(to, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200)));
            animation.KeyFrames.Add(key1);
            animation.KeyFrames.Add(key2);
            Storyboard.SetTarget(animation, target);
            Storyboard.SetTargetProperty(animation, new PropertyPath(string.Format("({0})", property.ToString())));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }
}
