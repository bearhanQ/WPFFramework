using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;

namespace WPFTemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Human.Childs.Clear();
            Human.Childs.Rows.Add("5");
            Human.Childs.Rows.Add("6");
            Human.Childs.Rows.Add("7");
            Human.Childs.Rows.Add("8");

            Human.Childs.Rows.Add("9");
            Human.Childs.Rows.Add("10");
            Human.Childs.Rows.Add("11");
            Human.Childs.Rows.Add("12");

            Human.Childs.Rows.Add("13");
            Human.Childs.Rows.Add("14");
            Human.Childs.Rows.Add("15");
            Human.Childs.Rows.Add("16");

            Human.Childs.Rows.Add("17");
            Human.Childs.Rows.Add("18");
            Human.Childs.Rows.Add("19");
            Human.Childs.Rows.Add("20");

            Human.Childs.Rows.Add("21");
            Human.Childs.Rows.Add("22");
            Human.Childs.Rows.Add("23");
            Human.Childs.Rows.Add("24");
        }
    }

    public class People
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Pass { get; set; }
    }

    public class Human
    {
        private static DataTable _childs;
        public static DataTable Childs
        {
            get
            {
                if (_childs == null)
                {
                    _childs = new DataTable();
                    _childs.Columns.Add("Name", typeof(string));
                }
                return _childs;
            }
        }
    }
}
