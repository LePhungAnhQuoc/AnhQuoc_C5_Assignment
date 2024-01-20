using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ucLoanHistoryManagement.xaml
    /// </summary>
    public partial class ucLoanHistoryManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<LoanHistoryRepository> getLoanHistoryRepo { get; set; }
        public Func<LoanSlipRepository> getLoanSlipRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields

        private Stack<object> _storeContent;
        public Stack<object> storeContent
        {
            get
            {
                if (_storeContent == null)
                    _storeContent = new Stack<object>();
                return _storeContent;
            }
            set { _storeContent = value; }
        }

        #endregion

        #region Forms
        private ucAddLoanHistory ucAddLoanHistory;
        #endregion

        #region ViewModels
        private LoanHistoryViewModel loanHistoryVM;
        #endregion

        #region Mapping
        private LoanHistoryMap loanHistoryMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<LoanHistory> listFills;
        #endregion

        #region Properties

        #endregion

        #region propDp

        #region IsAllow-Features
        public bool IsAllowViewInformation
        {
            get { return (bool)GetValue(IsAllowViewInformationProperty); }
            set { SetValue(IsAllowViewInformationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowViewInformation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowViewInformationProperty =
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucLoanHistoryManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucLoanHistoryManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucLoanHistoryManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucLoanHistoryManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucLoanHistoryManagement), new PropertyMetadata(true));


        #endregion

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucLoanHistoryManagement()
        {
            InitializeComponent();

            #region Allocations
            listFills = new ObservableCollection<LoanHistory>();

            loanHistoryVM = UnitOfViewModel.Instance.LoanHistoryViewModel;

            loanHistoryMap = UnitOfMap.Instance.LoanHistoryMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucLoanHistoryManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucLoanHistoryManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucLoanHistorysTable.getItem_Status = () => null;

            AddToListFill(getLoanHistoryRepo().Gets());
            AddItemsToDataGrid(listFills);

            #region IsAllow-Feature
            ucLoanHistorysTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            #endregion
        }


        public void BackToMainPage()
        {
            this.Content = storeContent.Pop();

            NewItemFromUcAdd();
        }


        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getLoanHistoryRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<LoanHistory> FillByTextSearch(ObservableCollection<LoanHistory> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<LoanHistory> results = new ObservableCollection<LoanHistory>();
            //foreach (LoanHistory item in source)
            //{
            //    var itemDto = loanHistoryMap.ConvertToDto(item);
            //    bool isCheck = itemDto.Username.ContainsCorrectly(textSearch, true);
            //    if (isCheck)
            //    {
            //        results.Add(item);
            //    }
            //}
            return results;
        }
        #endregion


        private void NewItemFromUcAdd()
        {
            if (ucAddLoanHistory.Context.Item == null) // Cancel the operation
            {
                return;
            }

            LoanHistoryDto newLoanHistoryDto = ucAddLoanHistory.Context.Item;
            LoanHistory newLoanHistory = loanHistoryVM.CreateByDto(newLoanHistoryDto);

            #region AddTo-listFill
            listFills.Add(newLoanHistory);
            AddItemsToDataGrid(listFills);
            #endregion
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            ucAddLoanHistory = MainWindow.UnitOfForm.UcAddLoanHistory(true);
            ucAddLoanHistory.getParentUc = () => this;
            storeContent.Push(this.Content);
            this.Content = ucAddLoanHistory;

        }

        private void AddToListFill(ObservableCollection<LoanHistory> items)
        {
            listFills.Clear();
            listFills.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<LoanHistory> items)
        {
            ucLoanHistorysTable.getLoanHistorys = () => items;
            ucLoanHistorysTable.ModifiedPagination();
        }
    }
}
