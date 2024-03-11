using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFTemplate
{
    public class NotifyWindow
    {
        public static readonly DependencyProperty IsNotifyWindowProperty;

        static NotifyWindow()
        {
            IsNotifyWindowProperty = DependencyProperty.RegisterAttached("IsNotifyWindow", typeof(bool), typeof(NotifyWindow), 
                new FrameworkPropertyMetadata(false,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,new PropertyChangedCallback(IsNotifyWindowChangedCallBack)));
        }

        public static void IsNotifyWindowChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                if ((bool)e.NewValue)
                {
                    window.Loaded += (sender, args) =>
                    {
                        var screenWidth = SystemParameters.WorkArea.Width;
                        var screenHeight = SystemParameters.WorkArea.Height;
                        window.Left = screenWidth - window.Width;
                        window.Top = screenHeight - window.Height;
                        window.Topmost = true;
                    };
                }
            }
        }

        public static bool GetIsNotifyWindow(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNotifyWindowProperty);
        }

        public static void SetIsNotifyWindow(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNotifyWindowProperty, value);
        }
    }
}
