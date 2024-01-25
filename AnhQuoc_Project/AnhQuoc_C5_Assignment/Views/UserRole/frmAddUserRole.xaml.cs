using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for frmAddUserRole.xaml
    /// </summary>
    public partial class frmAddUserRole : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<UserRoleDto> getItemToUpdate { get; set; }
        public Func<UserRepository> getUserRepo { get; set; }
        public Func<UserRoleRepository> getUserRoleRepo { get; set; }
        public Func<RoleRepository> getRoleRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        private ObservableCollection<UserRole> allAdminUser;
        private string feature = string.Empty;
        #endregion

        #region Properties
        private ObservableCollection<User> _AllUsers;
        public ObservableCollection<User> AllUsers
        {
            get { return _AllUsers; }
            set
            {
                _AllUsers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Role> _AllRoles;
        public ObservableCollection<Role> AllRoles
        {
            get { return _AllRoles; }
            set
            {
                _AllRoles = value;
                OnPropertyChanged();
            }
        }

        public bool? StatusValue { get; set; } = true;
        #endregion

        #region ViewModels
        private UserViewModel userVM;
        private UserRoleViewModel userRoleVM;
        private RoleViewModel roleVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private UserMap userMap;
        #endregion

        #region ResultProperty
        private UserRoleDto _Item;
        public UserRoleDto Item
        {
            get
            {
                return _Item;
            }
            set
            {
                _Item = value;
                OnPropertyChanged();
            }
        }
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

        public frmAddUserRole()
        {
            InitializeComponent();

            userVM = UnitOfViewModel.Instance.UserViewModel;
            userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;
            roleVM = UnitOfViewModel.Instance.RoleViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            userMap = UnitOfMap.Instance.UserMap;

            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;
            cbUser.SelectionChanged += CbUser_SelectionChanged;

            this.DataContext = this;
            this.Loaded += frmAddUserRole_Loaded;
        }

        private void frmAddUserRole_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<User> getFillUserNotHasRole = userVM.FillUserRole(getUserRoleRepo().Gets(), false, StatusValue);

            AllUsers = getFillUserNotHasRole;

            AllRoles = new ObservableCollection<Role>();
            //AllRoles = new ObservableCollection<Role>(getRoleRepo().Gets());
            int count = getRoleRepo().Count;
            foreach (Role item in getRoleRepo())
            {
                AllRoles.Add(item);
            }
            AllRoles.RemoveAt(0);
            count = getRoleRepo().Count;

            if (getItemToUpdate == null)
            {
                string id = userRoleVM.GetId();
                Item = new UserRoleDto(id);

                feature = "ADD";
                SetFormByAddOrUpdate(feature);

                SetTextBoxIsEnable(false);

                if (getFillUserNotHasRole.Count == 0)
                {
                    Utilities.ShowMessageBox1("There are no users to add roles", string.Empty);
                    BtnCancel_Click(null, null);
                    return;
                }
            }
            else
            {
                var getItem = getItemToUpdate();
                Item = getItem.Clone() as UserRoleDto;

                feature = "UPDATE";
                SetFormByAddOrUpdate(feature);
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // IsCheckEmptyItem
            bool isCheckEmptyItem = userRoleVM.IsCheckEmptyItem(Item);
            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }
            this.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // IsCheckEmptyItem
            bool isCheckEmptyItem = userRoleVM.IsCheckEmptyItem(Item);
            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            userRoleVM.Copy(Item, getItemToUpdate());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;
            this.Close();
        }
        
        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(cbUser))
                return true;
            if (Validation.GetHasError(cbRole))
                return true;
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;
            be = cbUser.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();
            be = cbRole.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();
        }
        
        private void CbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (feature == "ADD")
            {
                if (cbUser.SelectedIndex == -1)
                {
                    SetTextBoxIsEnable(false);
                }
                else
                {
                    SetTextBoxIsEnable(true);
                }
            }
        }

        private void SetTextBoxIsEnable(bool isEnable)
        {
            txtUserName.IsEnabled = isEnable;
            txtPassword.IsEnabled = isEnable;
            cbRole.IsEnabled = isEnable;
        }

        private void SetFormByAddOrUpdate(string feature)
        {
            switch (feature)
            {
                case "ADD":
                    this.Title = "Add New User Role Form";
                    lblTitle.Content = "Add New User Role";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    btnConfirm.Visibility = Visibility.Visible;

                    break;
                case "UPDATE":
                    this.Title = "Update User Role Information Form";
                    lblTitle.Content = "Update User Role Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Collapsed;

                    gdCbUser.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
