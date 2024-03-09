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
    /// Interaction logic for ucProvinceManagement.xaml
    /// </summary>
    public partial class ucProvinceManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ProvinceRepository> getProvinceRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private ProvinceViewModel provinceVM;
        private AdultViewModel adultVM;
        #endregion

        #region Mapping
        private ProvinceMap provinceMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<Province> listFillProvinces;
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
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucProvinceManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucProvinceManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucProvinceManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucProvinceManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucProvinceManagement), new PropertyMetadata(true));


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

        public ucProvinceManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillProvinces = new ObservableCollection<Province>();

            provinceVM = UnitOfViewModel.Instance.ProvinceViewModel;
            adultVM = UnitOfViewModel.Instance.AdultViewModel;

            provinceMap = UnitOfMap.Instance.ProvinceMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucProvincesTable.btnInfoClick += UcProvincesTable_btnInfoClick;
            ucProvincesTable.btnUpdateClick += UcProvincesTable_btnUpdateClick;
            ucProvincesTable.btnDeleteClick += UcProvincesTable_btnDeleteClick;
            ucProvincesTable.btnRestoreClick += UcProvincesTable_btnRestoreClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucProvinceManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucProvinceManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucProvincesTable.getItem_Status = () => null;

            AddToListFill(getProvinceRepo().Gets());
            AddItemsToDataGrid(listFillProvinces);

            #region IsAllow-Feature
            ucProvincesTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            ucProvincesTable.IsAllowUpdate = IsAllowUpdate;
            ucProvincesTable.IsAllowDelete = IsAllowDelete;
            ucProvincesTable.IsAllowRestore = IsAllowRestore;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getProvinceRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<Province> FillByTextSearch(ObservableCollection<Province> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<Province> results = new ObservableCollection<Province>();
            foreach (Province item in source)
            {
                var itemDto = provinceMap.ConvertToDto(item);
                bool isCheck = itemDto.Name.ContainsCorrectly(textSearch, true);
                if (isCheck)
                {
                    results.Add(item);
                }
            }
            return results;
        }
        #endregion

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddProvince frmAddProvince = MainWindow.UnitOfForm.FrmAddProvince(true);
            frmAddProvince.getIdProvince = () => provinceVM.GetId();
            frmAddProvince.ShowDialog();
         
            if (frmAddProvince.Context.Item == null) // Cancel the operation
            {
                return;
            }

            ProvinceDto newProvinceDto = frmAddProvince.Context.Item;

            #region AddNewItem
            Province newProvince = provinceVM.CreateByDto(newProvinceDto);
            getProvinceRepo().Add(newProvince);
            getProvinceRepo().WriteAdd(newProvince);
            #endregion

            #region AddTo-listFill
            listFillProvinces.Add(newProvince);
            AddItemsToDataGrid(listFillProvinces);
            #endregion

            Utilities.ShowMessageBox1(Utilities.NotifyAddSuccessfully("province"));
        }

        private void UcProvincesTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucProvincesTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("province"));
                return;
            }

            ProvinceDto provinceDtoSelect = ucProvincesTable.SelectedProvinceDto;
            Province provinceSelect = provinceVM.FindById(provinceDtoSelect.Id);
            
            ucProvinceInformation ucProvinceInformation = MainWindow.UnitOfForm.UcProvinceInformation(true);
            ucProvinceInformation.getItem = () => provinceDtoSelect;

            Window frmProvinceInformation = Utilities.CreateDefaultForm();
            frmProvinceInformation.Content = ucProvinceInformation;
            frmProvinceInformation.Show();

            ucProvinceInformation.btnBack.Click += (_sender, _e) =>
            {
                frmProvinceInformation.Close();
            };
        }

        private void UcProvincesTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(false);
        }

        private void UcProvincesTable_btnRestoreClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(true);
        }

        private void ChangeStatus(bool updateStatus)
        {
            string message = string.Empty;
            if (ucProvincesTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("province"));
                return;
            }

            ProvinceDto provinceDtoSelect = ucProvincesTable.SelectedProvinceDto;
            Province provinceSelect = provinceVM.FindById(provinceDtoSelect.Id);

            if (updateStatus == false)
            {
                var listTemp = adultVM.FillByCity(adultVM.Repo.Gets(), provinceSelect.Name, null);
                if (listTemp.Count > 0)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyNotValidToDelete("province"));
                    return;
                }


                message = Utilities.NotifySureToDelete();
                if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                    return;
            }

            provinceSelect.Status = updateStatus;
            getProvinceRepo().WriteUpdateStatus(provinceSelect, updateStatus);

            ucProvincesTable.ModifiedPagination();

            if (updateStatus == false)
                message = Utilities.NotifyDeleteSuccessfully("province");
            else
                message = Utilities.NotifyRestoreSuccessfully("province");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void UcProvincesTable_btnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (ucProvincesTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("province"));
                return;
            }

            ProvinceDto provinceDtoSelect = ucProvincesTable.SelectedProvinceDto;
            Province provinceSelect = provinceVM.FindById(provinceDtoSelect.Id);

            frmAddProvince frmAddProvince = MainWindow.UnitOfForm.FrmAddProvince(true);
            frmAddProvince.getItemToUpdate = () => provinceDtoSelect;
            frmAddProvince.ShowDialog();

            if (frmAddProvince.Context.Item == null) // Cancel the operation
            {
                return;
            }

            ProvinceDto newProvinceDto = frmAddProvince.Context.Item;

            #region UpdateItemInformation
            Province getProvince = provinceVM.FindById(newProvinceDto.Id);
            provinceVM.Copy(getProvince, newProvinceDto);
            getProvinceRepo().WriteUpdate(getProvince);

            #endregion

            ucProvincesTable.ModifiedPagination();
            Utilities.ShowMessageBox1(Utilities.NotifyUpdateSuccessfully("province"));
        }

        private void AddToListFill(ObservableCollection<Province> items)
        {
            listFillProvinces.Clear();
            listFillProvinces.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<Province> items)
        {
            ucProvincesTable.getProvinces = () => items;
            ucProvincesTable.ModifiedPagination();
        }
    }
}
