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
    /// Interaction logic for ucRoleManagement.xaml
    /// </summary>
    public partial class ucRoleManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<RoleRepository> getRoleRepo { get; set; }
        public Func<RoleFunctionRepository> getRoleFunctionRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        public Func<ObservableCollection<string>> getRoleGroups { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private RoleViewModel roleVM;
        private RoleFunctionViewModel roleFunctionVM;
        #endregion

        #region Mapping
        private RoleMap roleMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<Role> listFills;
        #endregion

        #region Properties

        #endregion

        #region Prop-dp
        #region IsAllow-Features
        public bool IsAllowViewInformation
        {
            get { return (bool)GetValue(IsAllowViewInformationProperty); }
            set { SetValue(IsAllowViewInformationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowViewInformation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowViewInformationProperty =
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucRoleManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucRoleManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucRoleManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucRoleManagement), new PropertyMetadata(true));


        public bool IsAllowSetRole
        {
            get { return (bool)GetValue(IsAllowSetRoleProperty); }
            set { SetValue(IsAllowSetRoleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowSetRole.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowSetRoleProperty =
            DependencyProperty.Register("IsAllowSetRole", typeof(bool), typeof(ucRoleManagement), new PropertyMetadata(true));


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

        public ucRoleManagement()
        {
            InitializeComponent();

            #region Allocations
            listFills = new ObservableCollection<Role>();

            roleVM = UnitOfViewModel.Instance.RoleViewModel;
            roleFunctionVM = UnitOfViewModel.Instance.RoleFunctionViewModel;

            roleMap = UnitOfMap.Instance.RoleMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucRolesTable.btnDeleteClick += UcRolesTable_btnDeleteClick;
            ucRolesTable.btnUpdateClick += UcRolesTable_btnUpdateClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucRoleManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucRoleManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucRolesTable.getItem_Status = () => null;
            ucRolesTable.getRoleFunctionRepo = getRoleFunctionRepo;
            ucRolesTable.getRoleGroups = getRoleGroups;

            AddToListFill(getRoleRepo().Gets());
            AddItemsToDataGrid(listFills);

            var ucUserRoleManagement = MainWindow.UnitOfForm.UcUserRoleManagement(true);
            var ucRoleFunctionManagement = MainWindow.UnitOfForm.UcRoleFunctionManagement(true);

            #region IsAllow-Feature
            ucRolesTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            ucRolesTable.dtgbtnUpdate.Visibility = (IsAllowUpdate) ? Visibility.Visible : Visibility.Collapsed;
            ucRolesTable.dtgbtnDelete.Visibility = (IsAllowDelete) ? Visibility.Visible : Visibility.Collapsed;

            ucUserRoleManagement.IsAllowSetRole = IsAllowSetRole;
            #endregion

            #region Add-TabItem-Content
            tabUserRole.Content = ucUserRoleManagement;
            tabRoleFunction.Content = ucRoleFunctionManagement;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getRoleRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(listFills);
        }

        private ObservableCollection<Role> FillByTextSearch(ObservableCollection<Role> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<Role> results = new ObservableCollection<Role>();
            foreach (Role item in source)
            {
                var itemDto = roleMap.ConvertToDto(item);
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
            frmAddRole frmAddRole = MainWindow.UnitOfForm.FrmAddRole(true);
            frmAddRole.getIdRole = () => roleVM.GetId();
            frmAddRole.ShowDialog();
         
            if (frmAddRole.Context.Item == null) // Cancel the operation
                return;

            RoleDto newRoleDto = frmAddRole.Context.Item;

            #region AddNewItem
            Role newRole = roleVM.CreateByDto(newRoleDto);
            getRoleRepo().Add(newRole);
            getRoleRepo().WriteAdd(newRole);
            #endregion

            #region AddTo-listFill
            listFills.Add(newRole);
            AddItemsToDataGrid(listFills);
            #endregion
        }

        private void UcRolesTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            bool updateStatus = false;
            if (ucRolesTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("role"));
                return;
            }

            RoleDto roleDtoSelect = ucRolesTable.SelectedRoleDto;
            Role roleSelect = roleVM.FindById(roleDtoSelect.Id);

            string message = Utilities.NotifySureToDelete();
            if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                return;
        
            roleSelect.Status = updateStatus;
            getRoleRepo().WriteUpdateStatus(roleSelect, updateStatus);

            ucRolesTable.ModifiedPagination();

            message = Utilities.NotifyDeleteSuccessfully("role");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void UcRolesTable_btnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (ucRolesTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("role"));
                return;
            }

            RoleDto roleDtoSelect = ucRolesTable.SelectedRoleDto;

            frmAddRole frmAddRole = MainWindow.UnitOfForm.FrmAddRole(true);
            frmAddRole.getItemToUpdate = () => roleDtoSelect;
            frmAddRole.ShowDialog();

            if (frmAddRole.Context.Item == null) // Cancel the operation
            {
                return;
            }

            RoleDto updateRoleDto = frmAddRole.Context.Item;

            #region UpdateItemInformation
            Role getRole = roleVM.FindById(updateRoleDto.Id);

            roleVM.Copy(getRole, updateRoleDto);
            getRoleRepo().WriteUpdate(getRole);
            #endregion

            #region Refresh-listFill
            ucRolesTable.ModifiedPagination();
            #endregion
        }

        private void AddToListFill(ObservableCollection<Role> items)
        {
            listFills.Clear();
            listFills.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<Role> items)
        {
            ucRolesTable.getRoles = () => items;
            ucRolesTable.ModifiedPagination();
        }
    }
}
