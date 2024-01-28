using Microsoft.Win32;
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
    /// Interaction logic for frmAddRole.xaml
    /// </summary>
    public partial class frmAddRole : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<RoleDto> getItemToUpdate { get; set; }

        public Func<string> getIdRole { get; set; }
        public Func<RoleRepository> getRoleRepo { get; set; }
        public Func<ObservableCollection<string>> getRoleGroups { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        private ObservableCollection<string> _AllGroups;
        public ObservableCollection<string> AllGroups
        {
            get { return _AllGroups; }
            set
            {
                _AllGroups = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ViewModels
        private RoleViewModel roleVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private RoleMap roleMap;
        #endregion

        #region ResultProperty
        private RoleDto _Item;
        public RoleDto Item
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

        public frmAddRole()
        {
            InitializeComponent();

            roleVM = UnitOfViewModel.Instance.RoleViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            roleMap = UnitOfMap.Instance.RoleMap;
            
            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtName.MaxLength = maxLength;
            #endregion
            
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;

            txtName.LostFocus += TxtFormatValue_LostFocus;

            this.DataContext = this;
            this.Loaded += frmAddRole_Loaded;
        }

        private void frmAddRole_Loaded(object sender, RoutedEventArgs e)
        {
            AllGroups = new ObservableCollection<string>(getRoleGroups());
            AllGroups.RemoveAt(0);

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
            Item = new RoleDto(getIdRole());
            Item.Status = true;
        }

        private void CopyItem()
        {
            var getItem = getItemToUpdate();
            Item = getItem.Clone() as RoleDto;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            RunAllValidations();
            bool isHasError = this.IsValidationGetHasError();
            if (isHasError)
            {
                return;
            }

            // Kiểm tra thông tin Name của item có tồn tại trong danh sách
            var normalItem = roleVM.CreateByDto(Item);
            bool isExistInformation = Utilities.IsExistInformation(getRoleRepo().Gets(), normalItem, true, Constants.checkPropRole);

            if (isExistInformation)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInfo("Role"));
                return;
            }
            this.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            RunAllValidations();
            bool isHasError = this.IsValidationGetHasError();
            if (isHasError)
            {
                return;
            }

            var normalItem = roleVM.CreateByDto(Item);
            var normalSourceItem = roleVM.CreateByDto(getItemToUpdate());
            
            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropRole);

            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(getRoleRepo().Gets(), normalItem, true, Constants.checkPropRole);

                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInfo("Role"));
                    return;
                }
            }
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            roleVM.Copy(Item, getItemToUpdate());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;
            this.Close();
        }

        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(txtName))
                return true;
            if (Validation.GetHasError(cbGroup))
                return true;
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = cbGroup.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();
        }

        // Others methods
        private void SetFormByAddOrUpdate(string feature)
        {
            switch (feature)
            {
                case "ADD":
                    this.Title = "Add New Role Form";
                    lblTitle.Content = "Add New Role";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    this.Title = "Update Role Information Form";
                    lblTitle.Content = "Update Role Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        private void TxtFormatValue_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
        }
    }
}
