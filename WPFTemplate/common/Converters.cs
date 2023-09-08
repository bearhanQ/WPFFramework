using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
}
