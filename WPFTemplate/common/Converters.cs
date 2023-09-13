using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

namespace WPFTemplate
{
    public class SvgParser : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Path path = new Path();

            // 加载SVG文件
            XmlDocument svgDoc = new XmlDocument();
            svgDoc.Load(@"C:\Qh\Learning\MyWPF\WPFTemplate\Icons\搜索.svg");

            // 提取SVG路径数据
            XmlNodeList pathNodes = svgDoc.GetElementsByTagName("path");
            if (pathNodes.Count > 0)
            {
                string pathData = pathNodes[0].Attributes["d"].Value;
                path.Data = Geometry.Parse(pathData);
            }

            return path.Data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ButtonTypeToSvg : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value.ToString();
            switch (str)
            {
                case "Add":
                    return SvgResource.Add;
                case "Update":
                    return SvgResource.Update;
                case "Delete":
                    return SvgResource.Delete;
                case "Save":
                    return SvgResource.Save;
                case "Cancel":
                    return SvgResource.Cancel;
                case "Import":
                    return SvgResource.Import;
                case "Export":
                    return SvgResource.Export;
                case "Print":
                    return SvgResource.Print;
                case "Set":
                    return SvgResource.Set;
                case "Query":
                    return SvgResource.Query;
                case "Copy":
                    return SvgResource.Copy;
                case "Refresh":
                    return SvgResource.Refresh;
                case "Default":
                    return SvgResource.Default;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TextBoxIconToPathData : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value.ToString();
            switch (str)
            {
                case "User":
                    return SvgResource.User;
                case "KeyBoard":
                    return SvgResource.KeyBoard;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TextLengthToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NoBackgroundToBlack : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "#FFFFFFFF")
            {
                return Brushes.Black;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ProgressBarValueToPercentage : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var maximum = System.Convert.ToDouble(values[0]);
            var value = System.Convert.ToDouble(values[1]);

            if (maximum == 0)
            {
                return "0" + "%";
            }

            double progressValue = value / maximum * 100;
            return (Math.Round(progressValue)).ToString() + "%";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NormalTabControlCornerRadiusConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var radius = (CornerRadius)values[0];
            var items = values[1] as ItemCollection;
            var item = values[2] as TabItem;
            var index = items.IndexOf(item);
            if (items.Count == 1)
            {
                return radius;
            }

            if (index == 0)
            {
                return new CornerRadius(radius.TopLeft, 0, 0, radius.BottomLeft);
            }

            if (index == items.Count - 1)
            {
                return new CornerRadius(0, radius.TopRight, radius.BottomRight, 0);
            }

            return new CornerRadius(0, 0, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
