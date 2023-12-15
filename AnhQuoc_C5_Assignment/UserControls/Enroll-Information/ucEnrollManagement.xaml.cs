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
    /// Interaction logic for ucEnrollManagement.xaml
    /// </summary>
    public partial class ucEnrollManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<EnrollRepository> getEnrollRepo { get; set; }
        public Func<LoanSlipRepository> getLoanSlipRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private EnrollViewModel enrollVM;
        #endregion

        #region Mapping
        private EnrollMap enrollMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<Enroll> listFills;
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
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucEnrollManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucEnrollManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucEnrollManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucEnrollManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucEnrollManagement), new PropertyMetadata(true));


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

        public ucEnrollManagement()
        {
            InitializeComponent();

            #region Allocations
            listFills = new ObservableCollection<Enroll>();

            enrollVM = UnitOfViewModel.Instance.EnrollViewModel;

            enrollMap = UnitOfMap.Instance.EnrollMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucEnrollManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucEnrollManagement_Loaded(object sender, RoutedEventArgs e)
        {

            AddToListFill(getEnrollRepo().Gets());
            AddItemsToDataGrid(listFills);

            #region IsAllow-Feature
            ucEnrollsTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getEnrollRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<Enroll> FillByTextSearch(ObservableCollection<Enroll> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<Enroll> results = source;
            //foreach (Enroll item in source)
            //{
            //    var itemDto = EnrollMap.ConvertToDto(item);
            //    bool isCheck = itemDto.Username.ContainsCorrectly(textSearch, true);
            //    if (isCheck)
            //    {
            //        results.Add(item);
            //    }
            //}
            return results;
        }
        #endregion

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //frmAddEnroll frmAddEnroll = MainWindow.UnitOfForm.FrmAddEnroll(true);
            //frmAddEnroll.ShowDialog();

            //if (frmAddEnroll.Item == null) // Cancel the operation
            //{
            //    return;
            //}

            //EnrollDto newEnrollDto = frmAddEnroll.Item;
            //Enroll newEnroll = enrollVM.CreateByDto(newEnrollDto);

            //#region AddTo-listFill
            //listFills.Add(newEnroll);
            //AddItemsToDataGrid(listFills);
            //#endregion
        }

        private void AddToListFill(ObservableCollection<Enroll> items)
        {
            listFills.Clear();
            listFills.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<Enroll> items)
        {
            ucEnrollsTable.getEnrolls = () => items;
            ucEnrollsTable.ModifiedPagination();
        }
    }
}
