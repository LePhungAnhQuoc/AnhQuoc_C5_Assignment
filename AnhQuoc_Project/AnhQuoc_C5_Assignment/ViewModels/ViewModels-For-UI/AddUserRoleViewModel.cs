using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class AddUserRoleViewModel: BaseViewModel<object>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }
        private frmAddUserRole thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;
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
        private RoleMap roleMap;
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

        #region Commands
        //public RelayCommand LoadedCmd { get; private set; }
        public RelayCommand ClosingCmd { get; private set; }

        public RelayCommand btnConfirmClickCmd { get; private set; }
        public RelayCommand btnCancelClickCmd { get; private set; }
        public RelayCommand btnUpdateClickCmd { get; private set; }
        public RelayCommand btnResetClickCmd { get; private set; }
        public RelayCommand cbUserSelectionChangedCmd { get; private set; }
        #endregion

        public AddUserRoleViewModel()
        {
            userVM = UnitOfViewModel.Instance.UserViewModel;
            userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;
            roleVM = UnitOfViewModel.Instance.RoleViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            userMap = UnitOfMap.Instance.UserMap;
            roleMap = UnitOfMap.Instance.RoleMap;

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => frmAddUserRole_Loaded(para, null));
            ClosingCmd = new RelayCommand((para) => onClosing(para, null));

            btnConfirmClickCmd = new RelayCommand((para) => BtnConfirm_Click(para, null));
            btnCancelClickCmd = new RelayCommand((para) => BtnCancel_Click(para, null));
            btnUpdateClickCmd = new RelayCommand((para) => BtnUpdate_Click(para, null));
            btnResetClickCmd = new RelayCommand((para) => BtnReset_Click(para, null));
            cbUserSelectionChangedCmd = new RelayCommand((para) => CbUser_SelectionChanged(para, null));
            #endregion
        }

        public void onLoaded(object sender, RoutedEventArgs e)
        {
            IsCancel = true;
            thisForm = sender as frmAddUserRole;

            mainContentControls = new List<DependencyObject>();
            foreach (DependencyObject child in thisForm.mainContent.Children)
            {
                mainContentControls.AddRange(Utilities.GetControlHaveValidationRules(child));
            }

            TextBoxes = mainContentControls.Where(obj => obj is TextBox).Select(obj => obj as TextBox).ToList();
            foreach (TextBox textBox in TextBoxes)
            {
                textBox.MaxLength = Constants.textBoxMaxLength;
                textBox.LostFocus += Txt_LostFocus;
            }


            ObservableCollection<User> getFillUserNotHasRole = userVM.FillUserRole(thisForm.getUserRoleRepo().Gets(), false, StatusValue);

            AllUsers = getFillUserNotHasRole;

            AllRoles = new ObservableCollection<Role>();
            AllRoles.AddRange(thisForm.getRoleRepo().Gets());
            AllRoles.RemoveAt(0);

            if (thisForm.getItemToUpdate == null)
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
                var getItem = thisForm.getItemToUpdate();
                Item = getItem.Clone() as UserRoleDto;

                feature = "UPDATE";
                SetFormByAddOrUpdate(feature);
            }
        }

        private void onClosing(object sender, CancelEventArgs e)
        {
            BtnCancel_Click(null, null, true);
        }



        private void CbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (feature == "ADD")
            {
                if (thisForm.cbUser.SelectedIndex == -1)
                {
                    SetTextBoxIsEnable(false);
                }
                else
                {
                    Item.User = thisForm.cbUser.SelectedItem as User;
                    SetTextBoxIsEnable(true);
                }
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            Utilities.RunAllValidations(mainContentControls);
            bool isHasError = Utilities.IsValidationGetHasError(mainContentControls);
            if (isHasError)
            {
                return;
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            Utilities.RunAllValidations(mainContentControls);
            bool isHasError = Utilities.IsValidationGetHasError(mainContentControls);
            if (isHasError)
            {
                return;
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            userRoleVM.Copy(Item, thisForm.getItemToUpdate());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e, bool isClosed = false)
        {
            if (IsCancel)
                Item = null;
            if (!isClosed)
                thisForm.Close();
        }


        private void SetTextBoxIsEnable(bool isEnable)
        {
            thisForm.cbRole.IsEnabled = isEnable;
        }

        private void SetFormByAddOrUpdate(string feature)
        {
            switch (feature)
            {
                case "ADD":
                    thisForm.Title = "Add New User Role Form";
                    thisForm.lblTitle.Content = "Add New User Role";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    thisForm.btnConfirm.Visibility = Visibility.Visible;

                    break;
                case "UPDATE":
                    thisForm.Title = "Update User Role Information Form";
                    thisForm.lblTitle.Content = "Update User Role Information";

                    thisForm.stkUpdateButton.Visibility = Visibility.Visible;
                    thisForm.btnConfirm.Visibility = Visibility.Collapsed;

                    thisForm.gdCbUser.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.Text = txt.Text.Trim();
        }
    }
}
