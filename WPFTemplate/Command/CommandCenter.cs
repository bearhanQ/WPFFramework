using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFTemplate
{
    public class CommandCenter
    {
        public static readonly DependencyProperty EventProperty;

        public static readonly DependencyProperty CommandProperty;

        public static readonly DependencyProperty CommandParameterProperty;

        static CommandCenter()
        {
            EventProperty = DependencyProperty.RegisterAttached("Event", typeof(RoutedEvent), typeof(CommandCenter),
                new FrameworkPropertyMetadata(null,FrameworkPropertyMetadataOptions.None,new PropertyChangedCallback(EventPropertyChangedCallBack)));
            CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(CommandCenter));
            CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(CommandCenter));
        }
        private static void EventPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Control;
            if (control != null)
            {
                control.AddHandler(e.NewValue as RoutedEvent, new RoutedEventHandler((o, s) =>
                {
                    var command = d.GetValue(CommandProperty) as ICommand;
                    var commandParameter = d.GetValue(CommandParameterProperty);
                    if (command != null)
                    {
                        command.Execute(commandParameter);
                    }
                }));
            }
        }
        public static RoutedEvent GetEvent(DependencyObject obj)
        {
            return (RoutedEvent)obj.GetValue(EventProperty);
        }
        public static void SetEvent(DependencyObject obj, RoutedEvent value)
        {
            obj.SetValue(EventProperty, value);
        }
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }
        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }
        public static object GetCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(CommandParameterProperty);
        }
        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }
    }
}
