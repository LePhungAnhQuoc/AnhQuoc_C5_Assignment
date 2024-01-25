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
    /// Interaction logic for frmAddTranslator.xaml
    /// </summary>
    public partial class frmAddTranslator : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<TranslatorDto> getItemToUpdate { get; set; }

        public Func<string> getIdTranslator { get; set; }
        public Func<TranslatorRepository> getTranslatorRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region ViewModels
        private TranslatorViewModel translatorVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private TranslatorMap translatorMap;
        #endregion

        #region ResultProperty
        private TranslatorDto _Item;
        public TranslatorDto Item
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

        public frmAddTranslator()
        {
            InitializeComponent();

            translatorVM = UnitOfViewModel.Instance.TranslatorViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            translatorMap = UnitOfMap.Instance.TranslatorMap;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtName.MaxLength = maxLength;
            txtDescription.MaxLength = Constants.txtDescriptionMaxLength;
            txtSummary.MaxLength = Constants.txtSummaryMaxLength;
            #endregion
            
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;

            txtName.LostFocus += Txt_LostFocus;
            txtDescription.LostFocus += Txt_LostFocus;
            txtSummary.LostFocus += Txt_LostFocus;
            
            this.DataContext = this;
            this.Loaded += frmAddTranslator_Loaded;
        }

        private void frmAddTranslator_Loaded(object sender, RoutedEventArgs e)
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
            Item = new TranslatorDto(getIdTranslator());
            Item.Status = true;
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void CopyItem()
        {
            var getItem = getItemToUpdate();
            Item = getItem.Clone() as TranslatorDto;

            Item.ModifiedAt = DateTime.Now;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {            
            // IsCheckEmptyItem
            bool isCheckEmptyItem = translatorVM.IsCheckEmptyItem(Item);

            // FormatValues
            FormatValues();

            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }

            Translator getTranslator = translatorVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(getTranslatorRepo().Gets(), getTranslator, true, Constants.checkPropTranslator);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("translator"));
                return;
            }
            this.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // IsCheckEmptyItem
            bool isCheckEmptyItem = translatorVM.IsCheckEmptyItem(Item);
            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }
            
            Translator normalItem = translatorVM.CreateByDto(Item);
            Translator normalSourceItem = translatorVM.CreateByDto(getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropTranslator);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(getTranslatorRepo().Gets(), normalItem, true, Constants.checkPropTranslator);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("translator"));
                    return;
                }
            }

           
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            translatorVM.Copy(Item, getItemToUpdate());
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
            if (Validation.GetHasError(txtSummary))
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
            be = txtSummary.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
              
        }

        private void FormatValues()
        {
            txtName.Text = txtName.Text.Trim();
            txtDescription.Text = txtDescription.Text.Trim();
            txtSummary.Text = txtSummary.Text.Trim();
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
                    this.Title = "Add New Translator Form";
                    lblTitle.Content = "Add New Translator";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    this.Title = "Update Translator Information Form";
                    lblTitle.Content = "Update Translator Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
