using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTemplate.Controls.Extra;

namespace WPFTemplate
{
    public enum ToastType
    {
        Info,
        Error,
        Warning,
        Success
    }

    public class Toast
    {
        private Toast()
        {

        }

        public static void Show(string msg)
        {
            ToastWindow toastWindow = new ToastWindow();
            toastWindow.Text = msg;
            toastWindow.Show();
        }

        public static void Show(string msg, ToastType toastType)
        {
            ToastWindow toastWindow = new ToastWindow();
            toastWindow.Text = msg;
            toastWindow.ToastType = toastType;
            toastWindow.Show();
        }
    }
}
