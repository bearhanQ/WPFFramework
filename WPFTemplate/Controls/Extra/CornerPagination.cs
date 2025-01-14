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
        public static readonly RoutedEvent ClickEvent;

        public static readonly DependencyProperty PageSizesProperty;

        public static readonly DependencyProperty CurrentPageProperty;

        public static readonly DependencyProperty TotalCountProperty;

        public static readonly DependencyProperty PageButtonsCollectionProperty;

        public static readonly DependencyProperty SizeProperty;

        public static readonly DependencyProperty CommandProperty;

        public List<string> PageSizes
        {
            get { return (List<string>)GetValue(PageSizesProperty); }
            set { SetValue(PageSizesProperty, value); }
        }

        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public int TotalCount
        {
            get { return (int)GetValue(TotalCountProperty); }
            set { SetValue(TotalCountProperty, value); }
        }

        public ObservableCollection<string> PageButtonsCollection
        {
            get { return (ObservableCollection<string>)GetValue(PageButtonsCollectionProperty); }
            set { SetValue(PageButtonsCollectionProperty, value); }
        }

        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        public event RoutedEventHandler Click
        {
            add
            {
                AddHandler(ClickEvent, value);
            }
            remove
            {
                RemoveHandler(ClickEvent, value);
            }
        }

        private ICommand _localCommand { get; set; }

        public ICommand LocalPageButtonCommand
        {
            get
            {
                return _localCommand;
            }
            private set
            {
                _localCommand = value;
            }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private int SkipNum = 5;

        static CornerPagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerPagination), new FrameworkPropertyMetadata(typeof(CornerPagination)));
            ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CornerPagination));
            PageSizesProperty = DependencyProperty.Register("PageSizes", typeof(List<string>), typeof(CornerPagination), new PropertyMetadata(new List<string> { "10", "20", "50", "100" }));
            CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(int), typeof(CornerPagination), new FrameworkPropertyMetadata(1,new PropertyChangedCallback(CurrentPageChangedCallback)));
            TotalCountProperty = DependencyProperty.Register("TotalCount", typeof(int), typeof(CornerPagination), new FrameworkPropertyMetadata(10,new PropertyChangedCallback(TotalCountChangedCallback)));
            PageButtonsCollectionProperty = DependencyProperty.Register("PageButtonsCollection", typeof(ObservableCollection<string>), typeof(CornerPagination), new PropertyMetadata(new ObservableCollection<string>()));
            SizeProperty = DependencyProperty.Register("Size", typeof(int), typeof(CornerPagination), new PropertyMetadata(10));
            CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(CornerPagination));
        }

        public CornerPagination()
        {
            LocalPageButtonCommand = new CommandBase(PageButtonAc);
        }

        private static void TotalCountChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pagination = (CornerPagination)d;
            if (pagination != null)
            {
                pagination.SetPageButtonsCollection();
            }
        }

        private static void CurrentPageChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pagination = (CornerPagination)d;
            if (pagination != null)
            {
                pagination.SetPageButtonsCollection();
                pagination.RaiseEvent(new RoutedEventArgs(CornerPagination.ClickEvent, pagination));
                //there is no need to set a commandparameter,command is just for searching...
                pagination.Command?.Execute(e.NewValue);
            }
        }

        private void PageButtonAc(object parameter)
        {
            if(parameter == null)
            {
                return;
            }

            var content = parameter.ToString();

            if (int.TryParse(content, out int index))
            {
                this.SetValue(CornerPagination.CurrentPageProperty, index);
            }
            else
            {
                var arrow = Char.Parse(content);

                switch (arrow)
                {
                    case '\ue713':
                        this.CurrentPage += SkipNum;
                        break;
                    case '\ue714':
                        this.CurrentPage -= SkipNum;
                        break;
                    case '\ue616':
                        {
                            if (CurrentPage < GetMaxPageSize())
                            {
                                CurrentPage += 1;
                            }
                            break;
                        }
                    case '\ue617':
                        {
                            if (CurrentPage > 1)
                            {
                                CurrentPage -= 1;
                            }
                            break;
                        }
                }
            }
        }

        private static void CurrentPageTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            var textbox = e.OriginalSource as CornerTextBox;
            if (textbox != null && e.Key == Key.Enter)
            {
                if (int.TryParse(textbox.Text, out int page))
                {
                    var pagination = sender as CornerPagination;
                    if (pagination != null)
                    {
                        pagination.SetValue(CurrentPageProperty, page);
                    }
                }
            }
        }

        private int GetMaxPageSize()
        {
            var count = this.TotalCount;
            var size = this.Size;
            var maxindex = Math.Ceiling(((double)count / size));
            return (int)maxindex;
        }

        private void SetPageButtonsCollection()
        {
            this.PageButtonsCollection.Clear();

            var minIndex = 1;
            var maxPageSize = this.GetMaxPageSize();
            var currentIndex = this.CurrentPage;

            //1 2 3 4 5
            if (maxPageSize <= SkipNum)
            {
                for (int i = 1; i <= maxPageSize; i++)
                {
                    this.PageButtonsCollection.Add(i.ToString());
                }
                return;
            }

            var doublerightarrow = '\ue713'.ToString();
            var doubleleftarrow = '\ue714'.ToString();
            var rightarrow = '\ue616'.ToString();
            var lefttarrow = '\ue617'.ToString();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SetPageButtonsCollection();
        }
    }

    //internal class PageNumClickCommand : ICommand
    //{
    //    public event EventHandler CanExecuteChanged;

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public void Execute(object parameter)
    //    {
    //        if (parameter != null)
    //        {
    //            var dic = parameter as Dictionary<string, DependencyObject>;
    //            var pagination = dic.First().Value as CornerPagination;
    //            var content = dic.First().Key;
    //            if (int.TryParse(content, out int index))
    //            {
    //                pagination.SetValue(CornerPagination.CurrentPageIndexProperty, index);
    //            }
    //            else
    //            {
    //                var arrow = Char.Parse(content);

    //                switch (arrow)
    //                {
    //                    case '\ue713':
    //                        pagination.CurrentPageIndex += pagination.SkipNum;
    //                        break;
    //                    case '\ue714':
    //                        pagination.CurrentPageIndex -= pagination.SkipNum;
    //                        break;
    //                    case '\ue616':
    //                        {
    //                            if (pagination.CurrentPageIndex < pagination.GetMaxIndex())
    //                            {
    //                                pagination.CurrentPageIndex += 1;
    //                            }
    //                            break;
    //                        }
    //                    case '\ue617':
    //                        {
    //                            if (pagination.CurrentPageIndex > 1)
    //                            {
    //                                pagination.CurrentPageIndex -= 1;
    //                            }
    //                            break;
    //                        }
    //                }
    //            }
    //        }
    //    }
    //}

}
