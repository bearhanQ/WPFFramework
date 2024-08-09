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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class Card : ContentControl
    {
        public static readonly DependencyProperty CornerRadiusProperty;

        public static readonly DependencyProperty CardTypeProperty;

        public static readonly DependencyProperty AngleSizeProperty;

        private Grid GridMain;

        private Ellipse ellipse1;

        private Ellipse ellipse2;

        private Ellipse ellipse3;

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public CardType CardType
        {
            get { return (CardType)GetValue(CardTypeProperty); }
            set { SetValue(CardTypeProperty, value); }
        }

        public double AngleSize
        {
            get { return (double)GetValue(AngleSizeProperty); }
            set { SetValue(AngleSizeProperty, value); }
        }

        static Card()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Card));
            CardTypeProperty = DependencyProperty.Register("CardType", typeof(CardType), typeof(Card));
            AngleSizeProperty = DependencyProperty.Register("AngleSize", typeof(double), typeof(Card), new PropertyMetadata((double)10));
        }

        private void CreateCardElement()
        {
            if (GridMain != null)
            {
                var geometry = new RectangleGeometry();
                geometry.Rect = new Rect
                {
                    X = 0,
                    Y = 0,
                    Width = this.ActualWidth,
                    Height = this.ActualHeight
                };
                geometry.RadiusX = this.CornerRadius.TopLeft;
                geometry.RadiusY = this.CornerRadius.TopLeft;
                GridMain.Clip = geometry;
            }

            var size = this.ActualWidth > this.ActualHeight ? this.ActualHeight : this.ActualWidth;

            if (ellipse1 != null && ellipse2 != null)
            {
                ellipse1.Width = ellipse2.Width = ellipse3.Width = size / 2;
                ellipse1.Height = ellipse2.Height = ellipse3.Height = size / 2;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GridMain = GetTemplateChild("gridMain") as Grid;
            ellipse1 = GetTemplateChild("ellipse1") as Ellipse;
            ellipse2 = GetTemplateChild("ellipse2") as Ellipse;
            ellipse3 = GetTemplateChild("ellipse3") as Ellipse;

            CreateCardElement();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            CreateCardElement();
        }
    }
}
