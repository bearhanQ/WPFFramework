using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFTemplate
{
    public class ComboBoxViewFilterHelper : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public object ItemSource;

        public ComboBoxViewFilterHelper(object itemSource)
        {
            this.ItemSource = itemSource;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(parameter.ToString()))
            {
                var keyvalues = parameter.ToString().Split('&');
                var key = keyvalues[0];
                var value = keyvalues[1];

                if (ItemSource is DataView)
                {
                    var filtercollection = $"{key} like '%{value}%'";
                    string filter = string.Join(" and ", filtercollection);
                    var MainView = ItemSource as DataView;
                    MainView.RowFilter = filter;
                }

                if(ItemSource is ICollectionView)
                {
                    var MainView = ItemSource as ICollectionView;

                    Predicate<object> filter = item =>
                    {
                        bool bo = true;
                        if(item is string)
                        {
                            bo = item.ToString().Contains(value);
                        }
                        else
                        {
                            var dataItem = item.GetType().GetProperty(key).GetValue(item).ToString();
                            bo = dataItem.Contains(value);
                        }
                        return bo;
                    };
                    MainView.Filter = filter;
                }
            }
        }
    }
}
