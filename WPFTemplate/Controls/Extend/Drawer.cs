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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class Drawer : ItemsControl
    {
        public static readonly DependencyProperty OrientationProperty;

        public static readonly DependencyProperty PresentIndexProperty;

        private const double constSize = 10 + 2 + 2 + 10;

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
            if (Orientation == Orientation.Horizontal)
            {
                double to = itemsPresenter.Width < itemsPresenterActualWidth ? itemsPresenterActualWidth : 0;
                CreateStoryboard(itemsPresenter.Width, to, FrameworkElement.WidthProperty);
            }
            else
            {
                double to = itemsPresenter.Height < itemsPresenterActualHeight ? itemsPresenterActualHeight : 0;
                CreateStoryboard(itemsPresenter.Height, to, FrameworkElement.HeightProperty);
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
                DragDelta(horizontalChange, FrameworkElement.WidthProperty, itemsPresenterActualWidth);
            }
            else
            {
                double verticalChange = e.VerticalChange;
                if (this.VerticalAlignment == VerticalAlignment.Top)
                {
                    verticalChange = 0 - verticalChange;
                }
                DragDelta(verticalChange, FrameworkElement.HeightProperty, itemsPresenterActualHeight);
            }
        }
        private void CalculateItemPresenterSize()
        {
            double value = 0;
            if (this.Items.Count >= 1)
            {
                for (int i = 0; i < PresentIndex; i++)
                {
                    var item = this.Items[i];
                    var presentItem = item as FrameworkElement;
                    if (presentItem != null)
                    {
                        if(Orientation== Orientation.Horizontal)
                        {
                            value += presentItem.ActualWidth + presentItem.Margin.Left + presentItem.Margin.Right;
                        }
                        else
                        {
                            value += presentItem.ActualHeight + presentItem.Margin.Top + presentItem.Margin.Bottom;
                        }
                    }
                }
                DependencyProperty d = Orientation == Orientation.Horizontal ? FrameworkElement.WidthProperty : FrameworkElement.HeightProperty;
                itemsPresenter.SetValue(d, value);
            }
        }
        private void CreateStoryboard(double from, double to, DependencyProperty property)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
                Duration = new Duration(TimeSpan.FromMilliseconds(250)),
                FillBehavior = FillBehavior.Stop
            };
            Storyboard.SetTarget(animation, itemsPresenter);
            Storyboard.SetTargetProperty(animation, new PropertyPath(string.Format("({0})", property.ToString())));
            storyboard.Children.Add(animation);
            storyboard.Completed += (s, d) =>
            {
                itemsPresenter.SetValue(property, to);
            };
            storyboard.Begin();
        }
        private void DragDelta(double Change, DependencyProperty property, double maxValue)
        {
            var propertyValue = (double)itemsPresenter.GetValue(property);
            if (propertyValue - Change < 0)
            {
                itemsPresenter.SetValue(property, (double)0);
                return;
            }
            if (propertyValue - Change > maxValue + constSize)
            {
                itemsPresenter.SetValue(property, maxValue);
                return;
            }
            itemsPresenter.SetValue(property, propertyValue - Change);
        }
    }
}
