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
    /// Interaction logic for ucLoanSlipManagement.xaml
    /// </summary>
    public partial class ucLoanSlipManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
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

        private ucAddLoan ucAddLoan;
        #endregion

        #region ViewModels
        private LoanSlipViewModel loanSlipVM;
        #endregion

        #region Mapping
        #endregion

        #region Fillter-Variable
        private ObservableCollection<LoanSlip> listFillLoanSlips;
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
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucLoanSlipManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucLoanSlipManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucLoanSlipManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucLoanSlipManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }
        
        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucLoanSlipManagement), new PropertyMetadata(true));


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

        public ucLoanSlipManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillLoanSlips = new ObservableCollection<LoanSlip>();
            storeContent = new Stack<object>();

            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            #endregion

            #region Register-Event
            txtSearch.TextChanged += TxtSearch_TextChanged;
            btnAdd.Click += BtnAdd_Click;
            ucLoanSlipsTable.btnInfoClick += UcLoanSlipsTable_btnInfoClick;
            #endregion
            this.Loaded += ucLoanSlipManagement_Loaded;
            this.DataContext = this;
        }

        private void ucLoanSlipManagement_Loaded(object sender, RoutedEventArgs e)
        {
            AddToListFill(getLoanSlipRepo().Gets());
            AddItemsToDataGrid(listFillLoanSlips);

            #region IsAllow-Feature
            ucLoanSlipsTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
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
            var source = getLoanSlipRepo().Gets();
            var results = loanSlipVM.FillByReaderFullName(source, txtSearch.Text, true);

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }
        #endregion


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ucAddLoan = MainWindow.UnitOfForm.UcAddLoan(true);
            ucAddLoan.getParentUc = () => this;
            storeContent.Push(this.Content);
            this.Content = ucAddLoan;
        }
        
        private void NewItemFromUcAdd()
        {
            if (ucAddLoan.Context.LoanSlipDto == null) // Cancel the operation
            {
                return;
            }


            LoanSlip newLoanSlip = loanSlipVM.CreateByDto(ucAddLoan.Context.LoanSlipDto);

            #region AddTo-listFill
            listFillLoanSlips.Add(newLoanSlip);
            AddItemsToDataGrid(listFillLoanSlips);
            #endregion
        }


        private void AddToListFill(ObservableCollection<LoanSlip> items)
        {
            listFillLoanSlips.Clear();
            listFillLoanSlips.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<LoanSlip> items)
        {
            ucLoanSlipsTable.getLoanSlips = () => items;
            ucLoanSlipsTable.ModifiedPagination();
        }

        #region Table-Event
        private void UcLoanSlipsTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            ucLoanSlipInformation ucLoanSlipInformation = MainWindow.UnitOfForm.UcLoanSlipInformation(true);
            ucLoanSlipInformation.getItem = () => ucLoanSlipsTable.SelectedDto;

            Window frmLoanSlipInformation = Utilities.CreateFormToAddUserControlInfo();
            frmLoanSlipInformation.Content = ucLoanSlipInformation;

            frmLoanSlipInformation.ShowDialog();
        }
        #endregion
    }
}
