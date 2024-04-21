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
    public class Drawer : ItemsControl
    {
        public static readonly DependencyProperty OrientationProperty;

        public static readonly DependencyProperty PresentIndexProperty;

        private const double constWidth = 10 + 2 + 2 + 10;

        private Thumb thumb;

        private FrameworkElement itemsPresenter;

        private CornerButton buttonExpand;

        private double itemsPresenterActualWidth;

        private double itemsPresenterActualHeight;

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public double PresentIndex
        {
            get { return (double)GetValue(PresentIndexProperty); }
            set { SetValue(PresentIndexProperty, value); }
        }

        static Drawer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Drawer), new FrameworkPropertyMetadata(typeof(Drawer)));
            OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Drawer));
            PresentIndexProperty = DependencyProperty.Register("PresentIndex", typeof(double), typeof(Drawer),
                new FrameworkPropertyMetadata((double)1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(PresentIndexChangedCallBack)));
        }

        public Drawer()
        {
            this.Loaded += Drawer_Loaded;
        }

        private void Drawer_Loaded(object sender, RoutedEventArgs e)
        {
            CalculateItemPresenterSize();
            if (Orientation == Orientation.Horizontal)
            {
                itemsPresenterActualWidth = itemsPresenter.ActualWidth;
            }
            else
            {
                itemsPresenterActualHeight = itemsPresenter.ActualHeight;
            }
        }
        private static void PresentIndexChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var drawer = d as Drawer;
            if (drawer != null)
            {
                drawer.CalculateItemPresenterSize();
            }
        }
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DrawerItem();
        }
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DrawerItem;
        }
        public override void OnApplyTemplate()
        {
            thumb = GetTemplateChild("thumb1") as Thumb;
            itemsPresenter = GetTemplateChild("itemsPresenter1") as FrameworkElement;
            buttonExpand = GetTemplateChild("btnExpand") as CornerButton;

            if (thumb != null)
            {
                thumb.DragDelta += Thumb_DragDelta;
            }

            if (buttonExpand != null)
            {
                buttonExpand.Click += ButtonExpand_Click;
            }

            base.OnApplyTemplate();
        }
        private void ButtonExpand_Click(object sender, RoutedEventArgs e)
        {
            if(Orientation == Orientation.Horizontal)
            {
                Storyboard storyboard = new Storyboard();
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = itemsPresenter.Width,
                    To = itemsPresenterActualWidth,
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                    Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                    FillBehavior = FillBehavior.Stop
                };
                Storyboard.SetTarget(animation, itemsPresenter);
                Storyboard.SetTargetProperty(animation, new PropertyPath("(FrameworkElement.Width)"));
                storyboard.Children.Add(animation);
                storyboard.Completed += (s, d) =>
                {
                    itemsPresenter.Width = itemsPresenterActualWidth;
                };
                storyboard.Begin();
            }
            else
            {
                Storyboard storyboard = new Storyboard();
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = itemsPresenter.Height,
                    To = itemsPresenterActualHeight,
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                    Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                    FillBehavior = FillBehavior.Stop
                };
                Storyboard.SetTarget(animation, itemsPresenter);
                Storyboard.SetTargetProperty(animation, new PropertyPath("(FrameworkElement.Height)"));
                storyboard.Children.Add(animation);
                storyboard.Completed += (s, d) =>
                {
                    itemsPresenter.Height = itemsPresenterActualHeight;
                };
                storyboard.Begin();
            }
        }
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.Orientation == Orientation.Horizontal)
            {
                double horizontalChange = e.HorizontalChange;
                if (this.HorizontalAlignment == HorizontalAlignment.Left)
                {
                    horizontalChange = 0 - horizontalChange;
                }
                if (itemsPresenter.Width - horizontalChange < 0)
                {
                    itemsPresenter.Width = 0;
                    return;
                }
                if (itemsPresenter.Width - horizontalChange > itemsPresenterActualWidth + constWidth)
                {
                    itemsPresenter.Width = itemsPresenterActualWidth;
                    return;
                }
                itemsPresenter.Width -= horizontalChange;
            }
            else
            {
                double verticalChange = e.VerticalChange;
                if (this.VerticalAlignment == VerticalAlignment.Top)
                {
                    verticalChange = 0 - verticalChange;
                }
                if (itemsPresenter.Height - verticalChange < 0)
                {
                    itemsPresenter.Height = 0;
                    return;
                }
                if (itemsPresenter.Height - verticalChange > itemsPresenterActualHeight + constWidth)
                {
                    itemsPresenter.Height = itemsPresenterActualHeight;
                    return;
                }
                itemsPresenter.Height -= verticalChange;
            }
        }
        private void CalculateItemPresenterSize()
        {
            if (Orientation == Orientation.Horizontal)
            {
                double width = 0;
                if (this.Items.Count >= 1)
                {
                    for (int i = 0; i < PresentIndex; i++)
                    {
                        var item = this.Items[i];
                        var presentItem = item as FrameworkElement;
                        if (presentItem != null)
                        {
                            width += presentItem.ActualWidth + presentItem.Margin.Left + presentItem.Margin.Right;
                        }
                    }
                    itemsPresenter.Width = width;
                }
            }
            else
            {
                double height = 0;
                if (this.Items.Count >= 1)
                {
                    for (int i = 0; i < PresentIndex; i++)
                    {
                        var item = this.Items[i];
                        var presentItem = item as FrameworkElement;
                        if (presentItem != null)
                        {
                            height += presentItem.ActualHeight + presentItem.Margin.Top + presentItem.Margin.Bottom;
                        }
                    }
                    itemsPresenter.Height = height;
                }
            }
        }
    }
}
