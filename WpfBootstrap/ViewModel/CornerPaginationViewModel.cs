using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTemplate;

namespace WpfBootstrap.ViewModel
{
    public class CornerPaginationViewModel : ViewModelBase
    {
        private DataTable _human;

        public DataTable Human
        {
            get
            {
                if (_human == null)
                {
                    _human = new DataTable();
                    _human.Columns.Add("Name", typeof(string));
                    _human.Columns.Add("Age", typeof(int));
                }
                return _human;
            }
        }
        public CornerPaginationViewModel()
        {
            for (int i = 0; i < 200; i++)
            {
                Human.Rows.Add($"张三{i}", i);
            }
        }
    }
}
