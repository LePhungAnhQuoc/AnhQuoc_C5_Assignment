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
    /// Interaction logic for ucUserRoleManagement.xaml
    /// </summary>
    public partial class ucUserRoleManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<RoleRepository> getRoleRepo { get; set; }
        public Func<UserRepository> getUserRepo { get; set; }
        public Func<UserRoleRepository> getUserRoleRepo { get; set; }
        public Func<UserInfoRepository> getUserInfoRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private UserViewModel userVM;
        private RoleViewModel roleVM;
        private UserRoleViewModel userRoleVM;
        private UserInfoViewModel userInfoVM;
        #endregion

        #region Mapping
        private UserMap userMap;
        private UserRoleMap userRoleMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<UserRole> listFillUserRoles;
        #endregion

        #region Properties
        #endregion

        #region PropDp

        #region IsAllow-Features


        public bool IsAllowSetRole
        {
            get { return (bool)GetValue(IsAllowSetRoleProperty); }
            set { SetValue(IsAllowSetRoleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowSetRole.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowSetRoleProperty =
            DependencyProperty.Register("IsAllowSetRole", typeof(bool), typeof(ucUserRoleManagement), new PropertyMetadata(true));


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

        public ucUserRoleManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillUserRoles = new ObservableCollection<UserRole>();

            userVM = UnitOfViewModel.Instance.UserViewModel;
            roleVM = UnitOfViewModel.Instance.RoleViewModel;
            userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;
            userInfoVM = UnitOfViewModel.Instance.UserInfoViewModel;

            userMap = UnitOfMap.Instance.UserMap;
            userRoleMap = UnitOfMap.Instance.UserRoleMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucUserRolesTable.btnInfoClick += UcUsersTable_btnInfoClick;
            ucUserRolesTable.btnSetRoleClick += UcUsersTable_btnSetRoleClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            
            this.Loaded += ucUserRoleManagement_Loaded;
            this.DataContext = this;
        }

        private void ucUserRoleManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucUserRolesTable.getItem_Status = () => null;
            ucUserRolesTable.getUserRepo = getUserRepo;
            ucUserRolesTable.getUserRoles = () => getUserRoleRepo().Gets();

            AddToListFill(getUserRoleRepo().Gets());
            AddItemsToDataGrid(listFillUserRoles);

            ucUserRolesTable.dtgbtnSetRole.Visibility = (IsAllowSetRole)
                ? Visibility.Visible : Visibility.Collapsed;
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getUserRoleRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(listFillUserRoles);
        }

        private ObservableCollection<UserRole> FillByTextSearch(ObservableCollection<UserRole> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<UserRole> results = new ObservableCollection<UserRole>();
            foreach (UserRole item in source)
            {
                var itemDto = userRoleMap.ConvertToDto(item);
                bool isCheck = itemDto.User.Username.ContainsCorrectly(textSearch, true);
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
            frmAddUserRole frmAddUserRole = MainWindow.UnitOfForm.FrmAddUserRole(true);
            frmAddUserRole.ShowDialog();

            if (frmAddUserRole.Context.Item == null) // Cancel the operation
            {
                return;
            }

            UserRoleDto newUserRoleDto = frmAddUserRole.Context.Item;

            #region AddNewItem
            UserRole newUserRole = userRoleVM.CreateByDto(newUserRoleDto);
            getUserRoleRepo().Add(newUserRole);
            getUserRoleRepo().WriteAdd(newUserRole);
            #endregion

            #region AddTo-listFill
            listFillUserRoles.Add(newUserRole);
            AddItemsToDataGrid(listFillUserRoles);
            #endregion
            
            Utilitys.ShowMessageBox1(Utilitys.NotifyAddSuccessfully("user role"));

        }

        private void UcUsersTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucUserRolesTable.dgDatas.SelectedIndex == -1)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyPleaseSelect("user"));
                return;
            }

            UserRoleDto userRoleDtoSelect = ucUserRolesTable.SelectedUserRoleDto;

            User userSelect = userVM.FindById(userRoleDtoSelect.User.Id);
            UserDto userDtoSelect = userMap.ConvertToDto(userSelect);

            ucUserInformation ucUserInformation = MainWindow.UnitOfForm.UcUserInformation(true);
            ucUserInformation.getItem = () => userDtoSelect;

            Window frmUserInformation = Utilitys.CreateDefaultForm();
            frmUserInformation.Content = ucUserInformation;

            ucUserInformation.btnBack.Click += (_sender, _e) =>
            {
                frmUserInformation.Close();
            };
            frmUserInformation.Show();
        }

        private void UcUsersTable_btnSetRoleClick(object sender, RoutedEventArgs e)
        {
            if (ucUserRolesTable.dgDatas.SelectedIndex == -1)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyPleaseSelect("user"));
                return;
            }

            UserRoleDto userRoleDtoSelect = ucUserRolesTable.SelectedUserRoleDto;

            if (userRoleDtoSelect.Role.Id == Constants.adminRoleId)
            {
                Utilitys.ShowMessageBox1("Can not set role for admin");
                return;
            }

            User userSelect = userVM.FindById(userRoleDtoSelect.User.Id);
            UserDto userDtoSelect = userMap.ConvertToDto(userSelect);

            frmAddUserRole frmAddUserRole = MainWindow.UnitOfForm.FrmAddUserRole(true);
            frmAddUserRole.getItemToUpdate = () => userRoleDtoSelect;
            frmAddUserRole.ShowDialog();

            if (frmAddUserRole.Context.Item == null) // Cancel the operation
            {
                return;
            }

            UserRoleDto newUserRoleDto = frmAddUserRole.Context.Item;
       
            #region UpdateItemInformation
            UserRole updateUserRole = userRoleVM.FindById(newUserRoleDto.Id);
            userRoleVM.Copy(updateUserRole, newUserRoleDto);
            getUserRoleRepo().WriteUpdate(updateUserRole);
            #endregion

            #region Refresh-listFill
            ucUserRolesTable.ModifiedPagination();
            #endregion

            Utilitys.ShowMessageBox1(Utilitys.NotifyUpdateSuccessfully("user role"));

        }

        private void AddToListFill(ObservableCollection<UserRole> items)
        {
            listFillUserRoles.Clear();
            listFillUserRoles.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<UserRole> items)
        {
            ucUserRolesTable.getUserRoles = () => (items);
            ucUserRolesTable.ModifiedPagination();
        }
    }
}
