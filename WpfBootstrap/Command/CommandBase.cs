using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfBootstrap.Command
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Func<object, bool> FCanExecute;

        public Action<object> AExecute;

        public CommandBase(Func<object, bool> fc, Action<object> ac)
        {
            FCanExecute = fc;
            AExecute = ac;
        }

        public bool CanExecute(object parameter)
        {
            return FCanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            AExecute(parameter);
        }
    }
}
