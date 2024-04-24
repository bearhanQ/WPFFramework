using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFTemplate
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Action<object> AcExecute { get; set; }

        public Func<object, bool> FcCanExecute { get; set; }

        public CommandBase(Action<object> ac, Func<object, bool> fc)
        {
            this.AcExecute = ac;
            this.FcCanExecute = fc;
        }

        public CommandBase(Action<object> ac)
        {
            this.AcExecute = ac;
            this.FcCanExecute = (o) =>
            {
                return true;
            };
        }
        public bool CanExecute(object parameter)
        {
            if (FcCanExecute != null)
            {
                return FcCanExecute(parameter);
            }
            return false;
        }
        public void Execute(object parameter)
        {
            AcExecute(parameter);
        }
    }
}
