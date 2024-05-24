using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFTemplate
{
    public class Chart : ItemsControl
    {
        public static readonly DependencyProperty HorizontalLineCountProperty;

        public static readonly DependencyProperty ValueMemberPathProperty;

        public static readonly DependencyProperty OpenAnimationProperty;

        internal static readonly DependencyProperty VerticalNumbersProperty;

        internal static readonly DependencyProperty RatioProperty;

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
        internal double Ratio
        {
            get { return (double)GetValue(RatioProperty); }
            set { SetValue(RatioProperty, value); }
        }
        public bool OpenAnimation
        {
            get { return (bool)GetValue(OpenAnimationProperty); }
            set { SetValue(OpenAnimationProperty, value); }
        }

        static Chart()
        {
            VerticalNumbersProperty = DependencyProperty.Register("VerticalNumbers", typeof(ObservableCollection<double>), typeof(Bar),
                new PropertyMetadata(new ObservableCollection<double>(new List<double> { 400, 300, 200, 100, 0 })));
            HorizontalLineCountProperty = DependencyProperty.Register("HorizontalLineCount", typeof(int), typeof(Bar),
                new FrameworkPropertyMetadata(5,FrameworkPropertyMetadataOptions.None,new PropertyChangedCallback(HorizontalLineCountPropertyChangedCallback)));
            ValueMemberPathProperty = DependencyProperty.Register("ValueMemberPath", typeof(string), typeof(Bar));
            RatioProperty = DependencyProperty.Register("Ratio", typeof(double), typeof(Bar), new PropertyMetadata((double)1));
            OpenAnimationProperty = DependencyProperty.Register("OpenAnimation", typeof(bool), typeof(Bar), new PropertyMetadata(true));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            GenerateVerticalNumbers();
        }
        private void GenerateVerticalNumbers()
        {
            if (this.ItemsSource != null && !string.IsNullOrWhiteSpace(ValueMemberPath))
            {
                double max = 0;
                foreach (var item in this.ItemsSource)
                {
                    double value = 0;
                    if (double.TryParse(item.GetType().GetProperty(ValueMemberPath).GetValue(item).ToString(), out value))
                    {
                        if (value > max)
                        {
                            max = value;
                        }
                    }
                }
                if (HorizontalLineCount > 0)
                {
                    var num = this.HorizontalLineCount - 1;
                    this.SetValue(RatioProperty, Math.Ceiling(max) / num);
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
            var bar = (Bar)d;
            if (bar != null)
            {
                bar.GenerateVerticalNumbers();
            }
        }
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            GenerateVerticalNumbers();
        }
    }
}
