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

        public int UcLoanSlipsTable_btnInfoClick { get; private set; }
        public int UcLoanSlipsTable_btnUpdateClick { get; private set; }
        public int UcLoanSlipsTable_btnDeleteClick { get; private set; }
        public int UcLoanSlipsTable_btnRestoreClick { get; private set; }

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

            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            #endregion

            btnAdd.Click += BtnAdd_Click;

            this.Loaded += ucLoanSlipManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucLoanSlipManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucLoanSlipsTable.getItem_Status = () => null;

            AddToListFill(getLoanSlipRepo().Gets());
            AddItemsToDataGrid(listFillLoanSlips);

            #region IsAllow-Feature
            ucLoanSlipsTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            #endregion
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmBorrowBook frmBorrowBook = MainWindow.UnitOfForm.FrmBorrowBook(true);
            frmBorrowBook.ShowDialog();
         
            if (frmBorrowBook.LoanSlip == null) // Cancel the operation
            {
                return;
            }

            LoanSlip newLoanSlip = frmBorrowBook.LoanSlip;
            
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
    }
}
