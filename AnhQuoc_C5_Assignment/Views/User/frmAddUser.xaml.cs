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
    /// Interaction logic for frmAddUser.xaml
    /// </summary>
    public partial class frmAddUser : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<UserDto> getItemToUpdate { get; set; }

        public Func<string> getIdUser { get; set; }
        public Func<UserRepository> getUserRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region ViewModels
        private UserViewModel userVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private UserMap userMap;
        #endregion

        #region ResultProperty
        private UserDto _Item;
        public UserDto Item
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

        public frmAddUser()
        {
            InitializeComponent();

            userVM = UnitOfViewModel.Instance.UserViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            userMap = UnitOfMap.Instance.UserMap;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtUserName.MaxLength = maxLength;
            txtPassword.MaxLength = maxLength;

            txtLName.MaxLength = maxLength;
            txtFName.MaxLength = maxLength;

            txtPhone.MaxLength = maxLength;
            txtAddress.MaxLength = Constants.txtAddressMaxLength;
            #endregion
            
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;

            txtFName.LostFocus += Txt_LostFocus;
            txtLName.LostFocus += Txt_LostFocus;

            txtUserName.LostFocus += Txt_LostFocus;
            txtPassword.LostFocus += Txt_LostFocus;

            txtPhone.LostFocus += Txt_LostFocus;
            txtAddress.LostFocus += Txt_LostFocus;

            this.DataContext = this;
            this.Loaded += frmAddUser_Loaded;
        }

        private void frmAddUser_Loaded(object sender, RoutedEventArgs e)
        {            
            if (getItemToUpdate == null)
            {
                NewItem();
                SetFormByAddOrUpdate("ADD");
            }
            else
            {
                CopyItem();
                SetFormByAddOrUpdate("UPDATE");
            }
        }

        private void NewItem()
        {
            Item = new UserDto(getIdUser());
            Item.Status = true;
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void CopyItem()
        {
            var getItem = getItemToUpdate();
            Item = getItem.Clone() as UserDto;

            Item.ModifiedAt = DateTime.Now;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {            
            // IsCheckEmptyItem
            bool isCheckEmptyItem = userVM.IsCheckEmptyItem(Item);

            // FormatValues
            FormatValues();

            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }

            // Kiểm tra thông tin Username của item có tồn tại trong danh sách
            User getUser = userVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(getUserRepo().Gets(), getUser, true, Constants.checkPropUser);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("username"));
                return;
            }
            this.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // IsCheckEmptyItem
            bool isCheckEmptyItem = userVM.IsCheckEmptyItem(Item);
            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }
            
            // Kiểm tra thông tin Username của item có tồn tại trong danh sách
            User normalItem = userVM.CreateByDto(Item);
            User normalSourceItem = userVM.CreateByDto(getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropUser);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(getUserRepo().Gets(), normalItem, true, Constants.checkPropUser);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("username"));
                    return;
                }
            }

           
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            userVM.Copy(Item, getItemToUpdate());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;
            this.Close();
        }
        
        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(txtUserName))
                return true;
            if (Validation.GetHasError(txtPassword))
                return true;
            if (Validation.GetHasError(txtLName))
                return true;
            if (Validation.GetHasError(txtFName))
                return true;
            if (Validation.GetHasError(txtPhone))
                return true;
            if (Validation.GetHasError(txtAddress))
                return true;
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtUserName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtPassword.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();

            be = txtLName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtFName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();

            be = txtPhone.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtAddress.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();         
        }

        private void FormatValues()
        {
            txtLName.Text = txtLName.Text.Trim();
            txtFName.Text = txtFName.Text.Trim();

            txtUserName.Text = txtUserName.Text.Trim();
            txtPassword.Text = txtPassword.Text.Trim();

            txtPhone.Text = txtPhone.Text.Trim();
            txtAddress.Text = txtAddress.Text.Trim();
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.Text = txt.Text.Trim();
        }

        // Others methods
        private void SetFormByAddOrUpdate(string feature)
        {
            switch (feature)
            {
                case "ADD":
                    this.Title = "Add New User Form";
                    lblTitle.Content = "Add New User";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    this.Title = "Update User Information Form";
                    lblTitle.Content = "Update User Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
