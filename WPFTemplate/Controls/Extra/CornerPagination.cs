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

        private int maxPageSize
        {
            get
            {
                var count = this.TotalCount;
                var size = this.Size;
                var maxPageSize = Math.Ceiling((double)count / size);
                return (int)maxPageSize;
            }
        }

        static CornerPagination()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerPagination), new FrameworkPropertyMetadata(typeof(CornerPagination)));
            ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CornerPagination));
            PageSizesProperty = DependencyProperty.Register("PageSizes", typeof(List<string>), typeof(CornerPagination), new PropertyMetadata(new List<string> { "10", "20", "50", "100" }));
            CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(int), typeof(CornerPagination), new FrameworkPropertyMetadata(1, new PropertyChangedCallback(CurrentPageChangedCallback)));
            TotalCountProperty = DependencyProperty.Register("TotalCount", typeof(int), typeof(CornerPagination), new FrameworkPropertyMetadata(10, new PropertyChangedCallback(TotalCountChangedCallback)));
            PageButtonsCollectionProperty = DependencyProperty.Register("PageButtonsCollection", typeof(ObservableCollection<string>), typeof(CornerPagination), new PropertyMetadata(new ObservableCollection<string>()));
            SizeProperty = DependencyProperty.Register("Size", typeof(int), typeof(CornerPagination), new FrameworkPropertyMetadata(10, new PropertyChangedCallback(SizeChangedCallback)));
            CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(CornerPagination));
        }

        public CornerPagination()
        {
            LocalPageButtonCommand = new CommandBase(PageButtonAc);
        }

        private static void SizeChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pagination = (CornerPagination)d;
            if (pagination != null)
            {
                // pagination.SetPageButtonsCollection();
                pagination.CurrentPage = 1;
            }
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
                        var sum = CurrentPage + SkipNum;
                        if (sum > maxPageSize)
                        {
                            sum = maxPageSize;
                        }
                        this.CurrentPage = sum;
                        break;
                    case '\ue714':
                        var sub = CurrentPage - SkipNum;
                        if (sub < 1)
                        {
                            sub = 1;
                        }
                        this.CurrentPage = sub;
                        break;
                    case '\ue616':
                        {
                            if (CurrentPage < maxPageSize)
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

        private string doubleRightArrow = '\ue713'.ToString();
        private string doubleLeftArrow = '\ue714'.ToString();
        private string singleRightArrow = '\ue616'.ToString();
        private string singleLeftArrow = '\ue617'.ToString();

        private void SetPageButtonsCollection()
        {
            this.PageButtonsCollection.Clear();

            //1 2 3 4 5
            //1 2 3 4 5...max
            //1 2 < x x > max
            //1 < x x x x max

            if (maxPageSize <= SkipNum)
            {
                PageButtonsCollection.Add(singleLeftArrow);
                for (int i = 1; i <= maxPageSize; i++)
                {
                    PageButtonsCollection.Add(i.ToString());
                }
                PageButtonsCollection.Add(singleRightArrow);
            }
            else if (CurrentPage <= SkipNum)
            {
                PageButtonsCollection.Add(singleLeftArrow);
                for (int i = 1; i <= SkipNum; i++)
                {
                    PageButtonsCollection.Add(i.ToString());
                }
                if (SkipNum < maxPageSize)
                {
                    PageButtonsCollection.Add(doubleRightArrow);
                    PageButtonsCollection.Add(maxPageSize.ToString());
                }
                PageButtonsCollection.Add(singleRightArrow);
            }
            else if (CurrentPage < maxPageSize - SkipNum)
            {
                PageButtonsCollection.Add(singleLeftArrow);
                PageButtonsCollection.Add("1");
                if (2 < CurrentPage - 1)
                {
                    PageButtonsCollection.Add("2");
                    PageButtonsCollection.Add(doubleLeftArrow);
                }
                for (int i = CurrentPage - 1; i <= CurrentPage + 1; i++)
                {
                    PageButtonsCollection.Add(i.ToString());
                }
                if (CurrentPage + 1 < maxPageSize)
                {
                    PageButtonsCollection.Add(doubleRightArrow);
                    PageButtonsCollection.Add(maxPageSize.ToString());
                }
                PageButtonsCollection.Add(singleRightArrow);
            }
            else
            {
                PageButtonsCollection.Add(singleLeftArrow);
                PageButtonsCollection.Add("1");
                if (1 < maxPageSize - SkipNum)
                {
                    PageButtonsCollection.Add(doubleLeftArrow);
                }
                for (int i = maxPageSize - SkipNum; i <= maxPageSize; i++)
                {
                    if (i == 1)
                    {
                        PageButtonsCollection.Add(doubleLeftArrow);
                        continue;
                    }
                    PageButtonsCollection.Add(i.ToString());
                }
                PageButtonsCollection.Add(singleRightArrow);
            }

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SetPageButtonsCollection();
        }
    }
}
