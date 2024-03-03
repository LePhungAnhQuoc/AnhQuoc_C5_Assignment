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
    /// Interaction logic for ucUserManagement.xaml
    /// </summary>
    public partial class ucUserManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<UserRepository> getUserRepo { get; set; }
        public Func<UserRoleRepository> getUserRoleRepo { get; set; }
        public Func<UserInfoRepository> getUserInfoRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private UserViewModel userVM;
        private UserRoleViewModel userRoleVM;
        private UserInfoViewModel userInfoVM;

        private LoanSlipViewModel loanSlipVM;
        private LoanHistoryViewModel loanHistoryVM;
        #endregion

        #region Mapping
        private UserMap userMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<User> listFillUsers;
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
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucUserManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucUserManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucUserManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucUserManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucUserManagement), new PropertyMetadata(true));


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

        public ucUserManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillUsers = new ObservableCollection<User>();

            userVM = UnitOfViewModel.Instance.UserViewModel;
            userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;
            userInfoVM = UnitOfViewModel.Instance.UserInfoViewModel;
            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanHistoryVM = UnitOfViewModel.Instance.LoanHistoryViewModel;


            userMap = UnitOfMap.Instance.UserMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucUsersTable.btnInfoClick += UcUsersTable_btnInfoClick;
            ucUsersTable.btnUpdateClick += UcUsersTable_btnUpdateClick;
            ucUsersTable.btnDeleteClick += UcUsersTable_btnDeleteClick;
            ucUsersTable.btnRestoreClick += UcUsersTable_btnRestoreClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucUserManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucUserManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucUsersTable.getItem_Status = () => null;
            ucUsersTable.getUserRoleRepo = getUserRoleRepo;

            AddToListFill(getUserRepo().Gets());
            AddItemsToDataGrid(listFillUsers);

            #region IsAllow-Feature
            ucUsersTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            ucUsersTable.IsAllowUpdate = IsAllowUpdate;
            ucUsersTable.IsAllowDelete = IsAllowDelete;
            ucUsersTable.IsAllowRestore = IsAllowRestore;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getUserRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<User> FillByTextSearch(ObservableCollection<User> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<User> results = new ObservableCollection<User>();
            foreach (User item in source)
            {
                var itemDto = userMap.ConvertToDto(item);
                bool isCheck = itemDto.Username.ContainsCorrectly(textSearch, true);
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
            frmAddUser frmAddUser = MainWindow.UnitOfForm.FrmAddUser(true);
            frmAddUser.getIdUser = () => userVM.GetId();
            frmAddUser.ShowDialog();
         
            if (frmAddUser.Context.Item == null) // Cancel the operation
            {
                return;
            }

            UserDto newUserDto = frmAddUser.Context.Item;

            #region AddNewItem
            User newUser = userVM.CreateByDto(newUserDto);
            getUserRepo().Add(newUser);
            getUserRepo().WriteAdd(newUser);

            UserInfo newUserInfo = userInfoVM.CreateByDto(newUserDto);
            getUserInfoRepo().Add(newUserInfo);
            getUserInfoRepo().WriteAdd(newUserInfo);

            #endregion

            #region AddTo-listFill
            listFillUsers.Add(newUser);
            AddItemsToDataGrid(listFillUsers);
            #endregion

            Utilities.ShowMessageBox1(Utilities.NotifyAddSuccessfully("user"));
        }

        private void UcUsersTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucUsersTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("user"));
                return;
            }

            UserDto userDtoSelect = ucUsersTable.SelectedUserDto;
            User userSelect = userVM.FindById(userDtoSelect.Id);
            
            ucUserInformation ucUserInformation = MainWindow.UnitOfForm.UcUserInformation(true);
            ucUserInformation.getItem = () => userDtoSelect;

            Window frmUserInformation = Utilities.CreateDefaultForm();
            frmUserInformation.Content = ucUserInformation;
            frmUserInformation.Show();

            ucUserInformation.btnBack.Click += (_sender, _e) =>
            {
                frmUserInformation.Close();
            };
        }

        private void UcUsersTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(false);
        }

        private void UcUsersTable_btnRestoreClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(true);
        }

        private void ChangeStatus(bool updateStatus)
        {
            string message = string.Empty;
            if (ucUsersTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("user"));
                return;
            }

            UserDto userDtoSelect = ucUsersTable.SelectedUserDto;
            User userSelect = userVM.FindById(userDtoSelect.Id);

            if (updateStatus == false)
            {
                #region Check-admin-user
                var adminRole = userRoleVM.FindByIdRole(Constants.adminRoleId);
                var adminUser = userVM.FindById(adminRole.IdUser);

                if (userSelect.Id == adminUser.Id)
                {
                    MessageBox.Show("Can not delete admin user!", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                #endregion


                var tempList = loanSlipVM.FillByIdUser(loanSlipVM.Repo.Gets(), userSelect.Id);
                if (tempList.Count == 0)
                {
                    var listTemp = loanHistoryVM.FillByIdUser(loanHistoryVM.Repo.Gets(), userSelect.Id);


                    if (listTemp.Count == 0)
                    {
                        var tempL = userRoleVM.FillByIdUser(userSelect.Id);
                        if (tempL.Count > 0)
                        {
                            Utilities.ShowMessageBox1(Utilities.NotifyNotValidToDelete("user"));
                            return;
                        }
                    }
                    else
                    {
                        Utilities.ShowMessageBox1(Utilities.NotifyNotValidToDelete("user"));
                        return;
                    }
                }
                else
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyNotValidToDelete("user"));
                    return;
                }

                message = Utilities.NotifySureToDelete();
                if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                    return;
            }

            userSelect.Status = updateStatus;
            getUserRepo().WriteUpdateStatus(userSelect, updateStatus);

            ucUsersTable.ModifiedPagination();

            if (updateStatus == false)
                message = Utilities.NotifyDeleteSuccessfully("user");
            else
                message = Utilities.NotifyRestoreSuccessfully("user");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void UcUsersTable_btnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (ucUsersTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("user"));
                return;
            }

            UserDto userDtoSelect = ucUsersTable.SelectedUserDto;
            User userSelect = userVM.FindById(userDtoSelect.Id);

            frmAddUser frmAddUser = MainWindow.UnitOfForm.FrmAddUser(true);
            frmAddUser.getItemToUpdate = () => userDtoSelect;
            frmAddUser.ShowDialog();

            if (frmAddUser.Context.Item == null) // Cancel the operation
            {
                return;
            }

            UserDto newUserDto = frmAddUser.Context.Item;

            #region UpdateItemInformation

            UserInfo getUserInfo = userInfoVM.FindByIdUser(newUserDto.Id);
            userInfoVM.Copy(getUserInfo, newUserDto);
            getUserInfoRepo().WriteUpdate(getUserInfo);

            User getUser = userVM.FindById(newUserDto.Id);
            userVM.Copy(getUser, newUserDto);
            getUserRepo().WriteUpdate(getUser);

            #endregion

            ucUsersTable.ModifiedPagination();
            Utilities.ShowMessageBox1(Utilities.NotifyUpdateSuccessfully("user"));

        }

        private void AddToListFill(ObservableCollection<User> items)
        {
            listFillUsers.Clear();
            listFillUsers.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<User> items)
        {
            ucUsersTable.getUsers = () => items;
            ucUsersTable.ModifiedPagination();
        }
    }
}
