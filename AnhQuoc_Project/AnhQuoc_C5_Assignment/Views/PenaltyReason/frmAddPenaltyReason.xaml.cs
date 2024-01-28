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
    /// Interaction logic for frmAddPenaltyReason.xaml
    /// </summary>
    public partial class frmAddPenaltyReason : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<PenaltyReasonDto> getItemToUpdate { get; set; }

        public Func<string> getIdPenaltyReason { get; set; }
        public Func<PenaltyReasonRepository> getPenaltyReasonRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region ViewModels
        private PenaltyReasonViewModel penaltyReasonVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private PenaltyReasonMap penaltyReasonMap;
        #endregion

        #region ResultProperty
        private PenaltyReasonDto _Item;
        public PenaltyReasonDto Item
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

        public frmAddPenaltyReason()
        {
            InitializeComponent();

            penaltyReasonVM = UnitOfViewModel.Instance.PenaltyReasonViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            penaltyReasonMap = UnitOfMap.Instance.PenaltyReasonMap;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtName.MaxLength = maxLength;
            txtDescription.MaxLength = Constants.textAreaMaxLength;
            txtPrice.MaxLength = maxLength;
            #endregion
            
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;

          
            txtName.LostFocus += Txt_LostFocus;
            txtDescription.LostFocus += Txt_LostFocus;
            txtPrice.LostFocus += Txt_LostFocus;

            this.DataContext = this;
            this.Loaded += frmAddPenaltyReason_Loaded;
        }

        private void frmAddPenaltyReason_Loaded(object sender, RoutedEventArgs e)
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
            Item = new PenaltyReasonDto(getIdPenaltyReason());
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void CopyItem()
        {
            var getItem = getItemToUpdate();
            Item = getItem.Clone() as PenaltyReasonDto;

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

            PenaltyReason getPenaltyReason = penaltyReasonVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(getPenaltyReasonRepo().Gets(), getPenaltyReason, true, Constants.checkPropPenaltyReason);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("penaltyReason"));
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

            PenaltyReason normalItem = penaltyReasonVM.CreateByDto(Item);
            PenaltyReason normalSourceItem = penaltyReasonVM.CreateByDto(getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropPenaltyReason);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(getPenaltyReasonRepo().Gets(), normalItem, true, Constants.checkPropPenaltyReason);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("penaltyReason"));
                    return;
                }
            }
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            penaltyReasonVM.Copy(Item, getItemToUpdate());
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
            if (Validation.GetHasError(txtPrice))
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

            be = txtPrice.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        private void FormatValues()
        {
            txtName.Text = txtName.Text.Trim();
            txtDescription.Text = txtDescription.Text.Trim();
            txtPrice.Text = txtPrice.Text.Trim();
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
                    this.Title = "Add New Penalty Reason Form";
                    lblTitle.Content = "Add New Penalty Reason";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    this.Title = "Update Penalty Reason Information Form";
                    lblTitle.Content = "Update Penalty Reason Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
