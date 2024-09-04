using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucPagination.xaml
    /// </summary>
    public partial class ucPagination : UserControl
    {
        #region getDatas
        private Func<ObservableCollection<object>> _getItems;
        public Func<ObservableCollection<object>> getItems
        {
            get
            {
                return _getItems;
            }
            set
            {
                _getItems = value;
                Refresh();
            }
        }
        public Func<int> getNumberItems { get; set; }

        #region Forms
        public Func<ucBookStatussTable> getUcBookStatussTable { get; set; }
        public Func<ucReadersTable> getUcReadersTable { get; set; }
        public Func<ucBooksTable> getUcBooksTable { get; set; }
        public Func<ucUsersTable> getUcUsersTable { get; set; }
        public Func<ucUserRolesTable> getUcUserRolesTable { get; set; }
        public Func<ucFunctionsTable> getUcFunctionsTable { get; set; }
        public Func<ucRolesTable> getUcRolesTable { get; set; }
        public Func<ucLoanSlipsTable> getUcLoanSlipsTable { get; set; }
        public Func<ucLoanDetailsTable> getUcLoanDetailsTable { get; set; }
        public Func<ucLoanDetailHistorysTable> getUcLoanDetailHistorysTable { get; set; }
        public Func<ucLoanHistorysTable> getUcLoanHistorysTable { get; set; }
        public Func<ucCategorysTable> getUcCategorysTable { get; set; }
        public Func<ucPublishersTable> getUcPublishersTable { get; set; }
        public Func<ucAuthorsTable> getUcAuthorsTable { get; set; }
        public Func<ucTranslatorsTable> getUcTranslatorsTable { get; set; }
        public Func<ucProvincesTable> getUcProvincesTable { get; set; }
        public Func<ucPenaltyReasonsTable> getUcPenaltyReasonsTable { get; set; }
        public Func<ucParametersTable> getUcParametersTable { get; set; }
        #endregion
        #endregion

        public int[] ItemsPerPageList { get; set; } = new int[] { 5, 10, 15, 25 };
        public int SelectedItemPerPage { get; set; }

        #region Result-Prop
        public ObservableCollection<object> Results { get; set; }
        #endregion

        #region Fields
        private int currentIndexPage;
        private int numberItems;
        private int maxPage;
        private ObservableCollection<object> items;
        #endregion

        public ucPagination()
        {
            InitializeComponent();
            currentIndexPage = 0;

            btnFirst.Click += BtnFirst_Click;
            btnPrev.Click += BtnPrev_Click;
            btnNext.Click += BtnNext_Click;
            btnLast.Click += BtnLast_Click;
            cbItemsPerPage.SelectionChanged += CbItemsPerPage_SelectionChanged;
            Loaded += UcPagination_Loaded;

            this.DataContext = this;
        }

        private void CbItemsPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void UcPagination_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void Refresh()
        {
            if (getNumberItems != null)
            {
                if (SelectedItemPerPage == 0)
                {
                    cbItemsPerPage.SelectedIndex = 0;

                    if (!(cbItemsPerPage.SelectedIndex == 0 && SelectedItemPerPage == 0))
                        return;
                    SelectedItemPerPage = ItemsPerPageList[0];
                }    

                numberItems = SelectedItemPerPage;

                items = getItems();

                maxPage = items.Count / numberItems;

                if (items.Count % numberItems > 0)
                {
                    maxPage++;
                }

                if (maxPage == 0)
                    maxPage = 1;

                stkNumberPages.Children.Clear();
                for (int i = 0; i < maxPage; i++)
                {
                    ucButtonCircle ucButtonCircle = new ucButtonCircle();
                    ucButtonCircle.ButtonContent = (i + 1).ToString();

                    ucButtonCircle.Margin = new Thickness(5, 0, 5, 0);

                    ucButtonCircle.btnCircle.Click += BtnNumberPage_Click;
                    stkNumberPages.Children.Add(ucButtonCircle);
                }
                BtnFirst_Click(null, null);
            }
        }

        private void GetItemsPerPage(int currentPage)
        {
            ObservableCollection<object> result = new ObservableCollection<object>();
            int startIndex = currentPage * numberItems;

            int i = 0;
            for (i = startIndex; i < startIndex + numberItems; i++)
            {
                if (i == items.Count)
                    break;
                var item = items[i];
                result.Add(item);
            }

            lblFirstPageNumber.Content = startIndex + 1;
            lblLastPageNumber.Content = i;
            lblTotalPageNumber.Content = items.Count;

            if (getUcReadersTable != null)
            {
                getUcReadersTable().dgReaders.ItemsSource = result;
            }
            if (getUcBooksTable != null)
            {
                getUcBooksTable().dgBooks.ItemsSource = result;
            }
            if (getUcUsersTable != null)
            {
                getUcUsersTable().dgDatas.ItemsSource = result;
            }
            if (getUcBookStatussTable != null)
            {
                getUcBookStatussTable().dgDatas.ItemsSource = result;
            }
            if (getUcFunctionsTable != null)
            {
                getUcFunctionsTable().dgDatas.ItemsSource = result;
            }
            if (getUcRolesTable != null)
            {
                getUcRolesTable().dgDatas.ItemsSource = result;
            }
            if (getUcUserRolesTable != null)
            {
                getUcUserRolesTable().dgDatas.ItemsSource = result;
            }
            if (getUcLoanSlipsTable != null)
            {
                getUcLoanSlipsTable().dgDatas.ItemsSource = result;
            }
            if (getUcLoanDetailsTable != null)
            {
                getUcLoanDetailsTable().dgDatas.ItemsSource = result;
            }
            if (getUcLoanDetailHistorysTable != null)
            {
                getUcLoanDetailHistorysTable().dgDatas.ItemsSource = result;
            }
            if (getUcLoanHistorysTable != null)
            {
                getUcLoanHistorysTable().dgDatas.ItemsSource = result;
            }
            if (getUcCategorysTable != null)
            {
                getUcCategorysTable().dgDatas.ItemsSource = result;
            }
            if (getUcPublishersTable != null)
            {
                getUcPublishersTable().dgDatas.ItemsSource = result;
            }
            if (getUcAuthorsTable != null)
            {
                getUcAuthorsTable().dgDatas.ItemsSource = result;
            }
            if (getUcTranslatorsTable != null)
            {
                getUcTranslatorsTable().dgDatas.ItemsSource = result;
            }
            if (getUcProvincesTable != null)
            {
                getUcProvincesTable().dgDatas.ItemsSource = result;
            }
            if (getUcPenaltyReasonsTable != null)
            {
                getUcPenaltyReasonsTable().dgDatas.ItemsSource = result;
            }
            if (getUcParametersTable != null)
            {
                getUcParametersTable().dgDatas.ItemsSource = result;
            }
            // Temp
            ucButtonCircle ucBtnCircle;
            for (i = 0; i < maxPage; i++)
            {
                ucBtnCircle = stkNumberPages.Children[i] as ucButtonCircle;
                ucBtnCircle.btnCircle.Background = new SolidColorBrush(Colors.Transparent);
                ucBtnCircle.btnCircle.Foreground = Brushes.Black;
                ucBtnCircle.Visibility = Visibility.Collapsed;
            }
            ucBtnCircle = stkNumberPages.Children[currentPage] as ucButtonCircle;
            ucBtnCircle.btnCircle.Foreground = Utilitys.GetColorFromCode("#0770da");
            ucBtnCircle.Visibility = Visibility.Visible;
        }


        #region Navigate-To-Page
        private void BtnNumberPage_Click(object sender, RoutedEventArgs e)
        {
            string strNumber = (sender as Button).Content.ToString();
            int numberContent = Convert.ToInt32(strNumber);
            currentIndexPage = numberContent - 1;
            GetItemsPerPage(currentIndexPage);
        }

        private void BtnFirst_Click(object sender, RoutedEventArgs e)
        {
            currentIndexPage = 0;
            GetItemsPerPage(currentIndexPage);
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndexPage > 0)
            {
                currentIndexPage--;
                GetItemsPerPage(currentIndexPage);
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndexPage < maxPage - 1)
            {
                currentIndexPage++;
                GetItemsPerPage(currentIndexPage);
            }
        }

        private void BtnLast_Click(object sender, RoutedEventArgs e)
        {
            currentIndexPage = maxPage - 1;
            GetItemsPerPage(currentIndexPage);
        }
        #endregion

    }
}
