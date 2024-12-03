using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class Chart : ItemsControl
    {
        public static readonly DependencyProperty HorizontalLineCountProperty;

        public static readonly DependencyProperty ValueMemberPathProperty;

        public static readonly DependencyProperty IsBottomContentVisibleProperty;

        public static readonly DependencyProperty StrokeProperty;

        public static readonly DependencyProperty OpenShadowProperty;

        internal static readonly DependencyProperty VerticalNumbersProperty;

        internal static readonly DependencyProperty RatioProperty;

        internal static readonly DependencyProperty SegmentsProperty;

        protected FrameworkElement itemPresenter;

        protected Path pathMain;

        public bool IsInDesignMode => System.ComponentModel.DesignerProperties.GetIsInDesignMode(this);

        internal ObservableCollection<double> VerticalNumbers
        {
            get { return (ObservableCollection<double>)GetValue(VerticalNumbersProperty); }
            set { SetValue(VerticalNumbersProperty, value); }
        }
        public int HorizontalLineCount
        {
            get { return (int)GetValue(HorizontalLineCountProperty); }
            set { SetValue(HorizontalLineCountProperty, value); }
        }
        public string ValueMemberPath
        {
            get { return (string)GetValue(ValueMemberPathProperty); }
            set { SetValue(ValueMemberPathProperty, value); }
        }
        internal int Ratio
        {
            get { return (int)GetValue(RatioProperty); }
            set { SetValue(RatioProperty, value); }
        }
        internal PathSegmentCollection Segments
        {
            get { return (PathSegmentCollection)GetValue(SegmentsProperty); }
            set { SetValue(SegmentsProperty, value); }
        }
        public bool IsBottomContentVisible
        {
            get { return (bool)GetValue(IsBottomContentVisibleProperty); }
            set { SetValue(IsBottomContentVisibleProperty, value); }
        }
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public bool OpenShadow
        {
            get { return (bool)GetValue(OpenShadowProperty); }
            set { SetValue(OpenShadowProperty, value); }
        }

        static Chart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Chart), new FrameworkPropertyMetadata(typeof(Chart)));
            VerticalNumbersProperty = DependencyProperty.Register("VerticalNumbers", typeof(ObservableCollection<double>), typeof(Chart),
                new PropertyMetadata(new ObservableCollection<double>(new List<double> { 400, 300, 200, 100, 0 })));
            HorizontalLineCountProperty = DependencyProperty.Register("HorizontalLineCount", typeof(int), typeof(Chart),
                new FrameworkPropertyMetadata(6,FrameworkPropertyMetadataOptions.None,new PropertyChangedCallback(HorizontalLineCountPropertyChangedCallback)));
            ValueMemberPathProperty = DependencyProperty.Register("ValueMemberPath", typeof(string), typeof(Chart));
            RatioProperty = DependencyProperty.Register("Ratio", typeof(int), typeof(Chart), new PropertyMetadata(1));
            PathSegmentCollection pathSegments = new PathSegmentCollection();
            SegmentsProperty = DependencyProperty.Register("Segments", typeof(PathSegmentCollection), typeof(Chart));
            IsBottomContentVisibleProperty = DependencyProperty.Register("IsBottomContentVisible", typeof(bool), typeof(Chart), new PropertyMetadata(true));
            StrokeProperty = DependencyProperty.Register("Stroke", typeof(Brush), typeof(Chart), 
                new FrameworkPropertyMetadata((new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF206BC4")))));
            OpenShadowProperty = DependencyProperty.Register("OpenShadow", typeof(bool), typeof(Chart), new PropertyMetadata(true));
        }

        public Chart()
        {
            this.Loaded += Chart_Loaded;
        }
        private void Chart_Loaded(object sender, RoutedEventArgs e)
        {
            GeneratePath();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            itemPresenter = GetTemplateChild("itemPresenter") as FrameworkElement;
            pathMain = GetTemplateChild("PathMain") as Path;
            GenerateVerticalNumbers();
        }
        private void GenerateVerticalNumbers()
        {
            if (this.ItemsSource != null && !string.IsNullOrWhiteSpace(ValueMemberPath))
            {
                int max = 0;
                foreach (var item in this.ItemsSource)
                {
                    int value = 0;
                    if (int.TryParse(item.GetType().GetProperty(ValueMemberPath).GetValue(item).ToString(), out value))
                    {
                        if (value > max)
                        {
                            max = value;
                        }
                    }
                }

                // 24 -> 25  25->25   27->30
                if (max % 5 != 0)
                {
                    max = ((max / 5) + 1) * 5;
                }

                if (HorizontalLineCount > 0)
                {
                    var num = this.HorizontalLineCount - 1;
                    var ratio = max / num;
                    this.SetValue(RatioProperty, ratio);
                    VerticalNumbers.Clear();
                    var collection = new ObservableCollection<double>();
                    for (int i = num; i >= 0; i--)
                    {
                        collection.Add(i * Ratio);
                    }
                    this.SetValue(VerticalNumbersProperty, collection);
                }
            }
        }
        private static void HorizontalLineCountPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (Chart)d;
            if (chart != null)
            {
                chart.GenerateVerticalNumbers();
            }
        }
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            GenerateVerticalNumbers();
            GeneratePath();
        }
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ChartItem();
        }
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ChartItem;
        }
        protected virtual void GeneratePath()
        {

        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            GeneratePath();
        }
    }
}
