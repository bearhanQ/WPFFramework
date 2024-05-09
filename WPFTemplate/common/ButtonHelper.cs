using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFTemplate
{
    public class ButtonHelper
    {

        public static readonly DependencyProperty IsCloseButtonProperty;

        public static readonly DependencyProperty IsMinimizeButtonProperty;

        public static readonly DependencyProperty IsMaxmizeButtonProperty;

        static ButtonHelper()
        {
            IsCloseButtonProperty = DependencyProperty.RegisterAttached("IsCloseButton", typeof(bool), typeof(ButtonHelper),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsCloseButtonChangedCallback)));
            IsMinimizeButtonProperty = DependencyProperty.RegisterAttached("IsMinimizeButton", typeof(bool), typeof(ButtonHelper),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsMinimizeButtonChangedCallback)));
            IsMaxmizeButtonProperty = DependencyProperty.RegisterAttached("IsMaxmizeButton", typeof(bool), typeof(ButtonHelper),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(IsMaxmizeButtonChangedCallback)));
        }

        public static bool GetIsCloseButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCloseButtonProperty);
        }

        public static void SetIsCloseButton(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCloseButtonProperty, value);
        }

        public static void IsCloseButtonChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (button != null)
            {
                var ownerWindow = LocalLogicalTreeHelper.GetParent(button, typeof(Window));
                if (ownerWindow != null && (bool)e.NewValue)
                {
                    var window = ownerWindow as Window;
                    button.Click += (o, j) =>
                    {
                        window.Close();
                    };
                }
            }
        }

        public static bool GetIsMinimizeButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMinimizeButtonProperty);
        }

        public static void SetIsMinimizeButton(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMinimizeButtonProperty, value);
        }

        public static void IsMinimizeButtonChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (button != null)
            {
                var ownerWindow = LocalLogicalTreeHelper.GetParent(button, typeof(Window));
                if (ownerWindow != null && (bool)e.NewValue)
                {
                    var window = ownerWindow as Window;
                    button.Click += (o, j) =>
                    {
                        window.WindowState = WindowState.Minimized;
                    };
                }
            }
        }

        public static bool GetIsMaxmizeButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMaxmizeButtonProperty);
        }

        public static void SetIsMaxmizeButton(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMaxmizeButtonProperty, value);
        }

        public static void IsMaxmizeButtonChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = d as Button;
            if (button != null)
            {
                var ownerWindow = LocalLogicalTreeHelper.GetParent(button, typeof(Window));
                if (ownerWindow != null && (bool)e.NewValue)
                {
                    var window = ownerWindow as Window;
                    button.Click += (o, j) =>
                    {
                        window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                    };
                }
            }
        }
    }
}
