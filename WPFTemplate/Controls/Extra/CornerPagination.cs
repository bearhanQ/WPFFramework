using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class CornerPagination : UserControl
    {
        public static readonly DependencyProperty ItemSourceProperty;

        internal static readonly DependencyProperty PageNumClickCommandProperty;

        public static readonly DependencyProperty TargetDataGridProperty;

        public static readonly DependencyProperty MaxCountProperty;

        public static readonly DependencyProperty PageCountProperty;

        public static readonly DependencyProperty PageNumsCollectionProperty;

        public static readonly DependencyProperty CurrentPageIndexProperty;

        internal readonly int SkipNum = 5;

        public DataTable ItemSource
        {
            get { return (DataTable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        internal PageNumClickCommand PageNumClickCommand
        {
            get { return (PageNumClickCommand)GetValue(PageNumClickCommandProperty); }
            set { SetValue(PageNumClickCommandProperty, value); }
        }

        public DataGrid TargetDataGrid
        {
            get { return (DataGrid)GetValue(TargetDataGridProperty); }
            set { SetValue(TargetDataGridProperty, value); }
        }

        public int MaxCount
        {
            get { return (int)GetValue(MaxCountProperty); }
            set { SetValue(MaxCountProperty, value); }
        }

        public int PageSize
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        public ObservableCollection<string> PageNumsCollection
        {
            get { return (ObservableCollection<string>)GetValue(PageNumsCollectionProperty); }
            set { SetValue(PageNumsCollectionProperty, value); }
        }

        public int CurrentPageIndex
        {
            get { return (int)GetValue(CurrentPageIndexProperty); }
            internal set { SetValue(CurrentPageIndexProperty, value); }
        }

        static CornerPagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerPagination), new FrameworkPropertyMetadata(typeof(CornerPagination)));

            ItemSourceProperty =DependencyProperty.Register("ItemSource", typeof(DataTable), typeof(CornerPagination),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ItemSourceChangedCallBack), new CoerceValueCallback(ItemSourceCoerceValueCallBack)));
            PageNumClickCommandProperty = DependencyProperty.Register("PageNumClickCommand", typeof(PageNumClickCommand), typeof(CornerPagination));
            TargetDataGridProperty = DependencyProperty.Register("TargetDataGrid", typeof(DataGrid), typeof(CornerPagination),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(TargetDataGridChangedCallBack)));
            MaxCountProperty = DependencyProperty.Register("MaxCount", typeof(int), typeof(CornerPagination), new PropertyMetadata(0));
            PageCountProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(CornerPagination),
                new FrameworkPropertyMetadata(20, new PropertyChangedCallback(PageCountChangedCallBack)));
            PageNumsCollectionProperty = DependencyProperty.Register("PageNumsCollection", typeof(ObservableCollection<string>), typeof(CornerPagination));
            CurrentPageIndexProperty = DependencyProperty.Register("CurrentPageIndex", typeof(int), typeof(CornerPagination),
                new FrameworkPropertyMetadata(1, new PropertyChangedCallback(CurrentPageIndexChangedCallBack), new CoerceValueCallback(CurrentPageIndexCoerceValueCallBack)));

            EventManager.RegisterClassHandler(typeof(CornerPagination), KeyDownEvent, new KeyEventHandler(CurrentPageTextBoxKeyDown));
        }

        public CornerPagination()
        {
            PageNumsCollection = new ObservableCollection<string>();
            PageNumClickCommand = new PageNumClickCommand();
        }

        private static void CurrentPageTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            var textbox = e.OriginalSource as CornerTextBox;
            if (textbox != null && e.Key == Key.Enter)
            {
                if (int.TryParse(textbox.Text, out int page))
                {
                    var pagination=sender as CornerPagination;
                    if (pagination != null)
                    {
                        pagination.SetValue(CurrentPageIndexProperty, page);
                    }
                }
            }
        }
        private static object ItemSourceCoerceValueCallBack(DependencyObject d, object basevalue)
        {
            var datatable = basevalue as DataTable;
            if (datatable != null)
            {
                if (!datatable.Columns.Contains("ExtraRow_"))
                {
                    datatable.Columns.Add("ExtraRow_", typeof(int));
                    int rownum = 1;
                    foreach (DataRow row in datatable.Rows)
                    {
                        row["ExtraRow_"] = rownum;
                        rownum++;
                    }
                }
            }
            return datatable;
        }
        private static void ItemSourceChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs arg)
        {
            var pagination = d as CornerPagination;
            pagination.Refresh();
        }
        private static void TargetDataGridChangedCallBack(DependencyObject d,DependencyPropertyChangedEventArgs arg)
        {
            var pagination = d as CornerPagination;
            pagination.Refresh();
        }
        private static void PageCountChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs arg)
        {
            var pagination = d as CornerPagination;
            pagination.Refresh();
        }
        private static void CurrentPageIndexChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs arg)
        {
            var pagination = d as CornerPagination;
            pagination.Refresh();
        }
        private static object CurrentPageIndexCoerceValueCallBack(DependencyObject d, object basevalue)
        {
            var pagination = d as CornerPagination;
            var minindex = 1;
            var maxindex = pagination.GetMaxIndex();
            var currentpageindex = (int)basevalue;
            if (currentpageindex < 1)
            {
                return minindex;
            }
            if (currentpageindex > maxindex)
            {
                return (int)maxindex;
            }
            return basevalue;
        }
        private void SetMaxCount()
        {
            if (ItemSource != null)
            {
                this.SetValue(MaxCountProperty, ItemSource.Rows.Count);
            }
        }
        private void CreateDataGridView()
        {
            if (ItemSource != null && TargetDataGrid != null)
            {
                DataView dataView = new DataView(ItemSource);
                dataView.RowFilter = "ExtraRow_ > " + ((CurrentPageIndex - 1) * PageSize) + " AND ExtraRow_ <= " + CurrentPageIndex * PageSize;
                TargetDataGrid.ItemsSource = dataView;
            }
        }
        private void ItemSource_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            Refresh();
        }
        private void ItemSource_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Delete)
            {
                var delindex = (int)e.Row["ExtraRow_"];
                foreach(DataRow row in ItemSource.Rows)
                {
                    var rowIndex = (int)row["ExtraRow_"];
                    if (rowIndex > delindex)
                    {
                        row["ExtraRow_"] = rowIndex -= 1;
                    }
                }
            }
        }
        private void ItemSource_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                var MaxIndex = ItemSource.Rows.Count;
                e.Row["ExtraRow_"] = MaxIndex;
                Refresh();
            }
        }
        private void SetPageNumsCollection()
        {
            this.PageNumsCollection.Clear();

            var minIndex = 1;
            var maxIndex = this.GetMaxIndex();
            var currentIndex = this.CurrentPageIndex;

            //1 ... max
            if (maxIndex <= SkipNum)
            {
                for (int i = 1; i <= maxIndex; i++)
                {
                    this.PageNumsCollection.Add(i.ToString());
                }
                return;
            }

            var doublerightarrow = '\ue713'.ToString();
            var doubleleftarrow = '\ue714'.ToString();
            var rightarrow = '\ue616'.ToString();
            var lefttarrow = '\ue617'.ToString();
            if (maxIndex > SkipNum)
            {
                // "1 2 3 4 5 ... max"
                if (currentIndex - minIndex < SkipNum)
                {
                    this.PageNumsCollection.Add(lefttarrow);
                    for (int i = 1; i <= SkipNum + 1; i++)
                    {
                        this.PageNumsCollection.Add(i.ToString());
                    }
                    this.PageNumsCollection.Add(doublerightarrow);
                    this.PageNumsCollection.Add(maxIndex.ToString());
                    this.PageNumsCollection.Add(rightarrow);
                    return;
                }

                // "1 ... x y z max"
                if (maxIndex - currentIndex < SkipNum)
                {
                    this.PageNumsCollection.Add(lefttarrow);
                    this.PageNumsCollection.Add(minIndex.ToString());
                    this.PageNumsCollection.Add(doubleleftarrow);
                    for (int i = maxIndex - SkipNum; i <= maxIndex; i++)
                    {
                        this.PageNumsCollection.Add(i.ToString());
                    }
                    this.PageNumsCollection.Add(rightarrow);
                    return;
                }

                //1...x y z .. max
                if (currentIndex - minIndex >= SkipNum && maxIndex - currentIndex >= SkipNum)
                {
                    this.PageNumsCollection.Add(lefttarrow);
                    this.PageNumsCollection.Add(minIndex.ToString());
                    this.PageNumsCollection.Add(doubleleftarrow);
                    for (int i = currentIndex - 2; i <= currentIndex + 2; i++)
                    {
                        this.PageNumsCollection.Add(i.ToString());
                    }
                    this.PageNumsCollection.Add(doublerightarrow);
                    this.PageNumsCollection.Add(maxIndex.ToString());
                    this.PageNumsCollection.Add(rightarrow);
                }
            }
        }
        internal int GetMaxIndex()
        {
            var count = this.MaxCount;
            var size = this.PageSize;
            var maxindex = Math.Ceiling(((double)count / size));
            return (int)maxindex;
        }
        public void Refresh()
        {
            SetMaxCount();
            CreateDataGridView();
            SetPageNumsCollection();
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (ItemSource != null)
            {
                ItemSource.RowChanged += ItemSource_RowChanged;
                ItemSource.RowDeleting += ItemSource_RowDeleting;
                ItemSource.RowDeleted += ItemSource_RowDeleted;
            }
        }
    }

    internal class PageNumClickCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                var dic = parameter as Dictionary<string, DependencyObject>;
                var pagination = dic.First().Value as CornerPagination;
                var content = dic.First().Key;
                if (int.TryParse(content, out int index))
                {
                    pagination.SetValue(CornerPagination.CurrentPageIndexProperty, index);
                }
                else
                {
                    var arrow = Char.Parse(content);

                    switch (arrow)
                    {
                        case '\ue713':
                            pagination.CurrentPageIndex += pagination.SkipNum;
                            break;
                        case '\ue714':
                            pagination.CurrentPageIndex -= pagination.SkipNum;
                            break;
                        case '\ue616':
                            {
                                if (pagination.CurrentPageIndex < pagination.GetMaxIndex())
                                {
                                    pagination.CurrentPageIndex += 1;
                                }
                                break;
                            }
                        case '\ue617':
                            {
                                if (pagination.CurrentPageIndex > 1)
                                {
                                    pagination.CurrentPageIndex -= 1;
                                }
                                break;
                            }
                    }
                }
            }
        }
    }

}
