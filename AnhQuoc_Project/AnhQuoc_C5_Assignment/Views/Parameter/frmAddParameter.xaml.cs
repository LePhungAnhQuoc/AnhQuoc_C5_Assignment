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
    /// Interaction logic for frmAddParameter.xaml
    /// </summary>
    public partial class frmAddParameter : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<ParameterDto> getItemToUpdate { get; set; }

        public Func<string> getIdParameter { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region ViewModels
        private ParameterViewModel parameterVM;
        #endregion

        #region Mapping
        private ParameterMap parameterMap;
        #endregion

        #region ResultProperty
        private ParameterDto _Item;
        public ParameterDto Item
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

        public frmAddParameter()
        {
            InitializeComponent();

            parameterVM = UnitOfViewModel.Instance.ParameterViewModel;
            parameterMap = UnitOfMap.Instance.ParameterMap;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtName.MaxLength = maxLength;
            txtDescription.MaxLength = Constants.textAreaMaxLength;
            txtValue.MaxLength = maxLength;
            #endregion
            
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;

            txtName.LostFocus += Txt_LostFocus;
            txtDescription.LostFocus += Txt_LostFocus;
            txtValue.LostFocus += Txt_LostFocus;

            this.DataContext = this;
            this.Loaded += frmAddParameter_Loaded;
        }

        private void frmAddParameter_Loaded(object sender, RoutedEventArgs e)
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
            Item = new ParameterDto(getIdParameter());
            Item.Status = true;
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void CopyItem()
        {
            var getItem = getItemToUpdate();
            Item = getItem.Clone() as ParameterDto;

            Item.ModifiedAt = DateTime.Now;
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

            Parameter getParameter = parameterVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(getParameterRepo().Gets(), getParameter, true, Constants.checkPropParameter);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("parameter"));
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

            Parameter normalItem = parameterVM.CreateByDto(Item);
            Parameter normalSourceItem = parameterVM.CreateByDto(getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropParameter);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(getParameterRepo().Gets(), normalItem, true, Constants.checkPropParameter);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("parameter"));
                    return;
                }
            }
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            parameterVM.Copy(Item, getItemToUpdate());
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
            if (Validation.GetHasError(txtDescription))
                return true;
            if (Validation.GetHasError(txtValue))
                return true;
          
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();

            be = txtDescription.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();

            be = txtValue.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
              
        }

        private void FormatValues()
        {
            txtName.Text = txtName.Text.Trim();
            txtDescription.Text = txtDescription.Text.Trim();
            txtValue.Text = txtValue.Text.Trim();
           
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
                    this.Title = "Add New Parameter Form";
                    lblTitle.Content = "Add New Parameter";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    this.Title = "Update Parameter Information Form";
                    lblTitle.Content = "Update Parameter Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
