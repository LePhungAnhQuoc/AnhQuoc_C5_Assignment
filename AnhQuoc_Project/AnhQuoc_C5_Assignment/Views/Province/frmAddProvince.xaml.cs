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
    /// Interaction logic for frmAddProvince.xaml
    /// </summary>
    public partial class frmAddProvince : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<ProvinceDto> getItemToUpdate { get; set; }

        public Func<int> getIdProvince { get; set; }
        public Func<ProvinceRepository> getProvinceRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region ViewModels
        private ProvinceViewModel provinceVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private ProvinceMap provinceMap;
        #endregion

        #region ResultProperty
        private ProvinceDto _Item;
        public ProvinceDto Item
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

        public frmAddProvince()
        {
            InitializeComponent();

            provinceVM = UnitOfViewModel.Instance.ProvinceViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            provinceMap = UnitOfMap.Instance.ProvinceMap;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtName.MaxLength = maxLength;
            #endregion
            
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;

            txtName.LostFocus += Txt_LostFocus;

            this.DataContext = this;
            this.Loaded += frmAddProvince_Loaded;
        }

        private void frmAddProvince_Loaded(object sender, RoutedEventArgs e)
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
            Item = new ProvinceDto(getIdProvince());
            Item.Status = true;
        }

        private void CopyItem()
        {
            var getItem = getItemToUpdate();
            Item = getItem.Clone() as ProvinceDto;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // FormatValues
            FormatValues();

            // Validation
            RunAllValidations();
            bool isHasError = this.IsValidationGetHasError();
            if (isHasError)
            {
                return;
            }

            Province getProvince = provinceVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(getProvinceRepo().Gets(), getProvince, true, Constants.checkPropProvince);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("province"));
                return;
            }
            this.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // FormatValues
            FormatValues();

            // Validation
            RunAllValidations();
            bool isHasError = this.IsValidationGetHasError();
            if (isHasError)
            {
                return;
            }

            Province normalItem = provinceVM.CreateByDto(Item);
            Province normalSourceItem = provinceVM.CreateByDto(getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropProvince);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(getProvinceRepo().Gets(), normalItem, true, Constants.checkPropProvince);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("province"));
                    return;
                }
            }
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            provinceVM.Copy(Item, getItemToUpdate());
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
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        private void FormatValues()
        {
            txtName.Text = txtName.Text.Trim();
           
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
                    this.Title = "Add New Province Form";
                    lblTitle.Content = "Add New Province";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    this.Title = "Update Province Information Form";
                    lblTitle.Content = "Update Province Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
