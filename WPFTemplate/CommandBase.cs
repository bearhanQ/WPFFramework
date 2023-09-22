using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFTemplate
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        private readonly Action<object> executeAction;
        private readonly Func<object, bool> canExecuteFunc;

        public CommandBase(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            this.executeAction = executeAction;
            this.canExecuteFunc = canExecuteFunc;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
