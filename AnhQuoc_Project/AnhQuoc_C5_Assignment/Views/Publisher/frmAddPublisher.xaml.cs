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
    /// Interaction logic for frmAddPublisher.xaml
    /// </summary>
    public partial class frmAddPublisher : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<PublisherDto> getItemToUpdate { get; set; }

        public Func<string> getIdPublisher { get; set; }
        public Func<PublisherRepository> getPublisherRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region ViewModels
        private PublisherViewModel publisherVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private PublisherMap publisherMap;
        #endregion

        #region ResultProperty
        private PublisherDto _Item;
        public PublisherDto Item
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

        public frmAddPublisher()
        {
            InitializeComponent();

            publisherVM = UnitOfViewModel.Instance.PublisherViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            publisherMap = UnitOfMap.Instance.PublisherMap;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtName.MaxLength = maxLength;
            txtPhone.MaxLength = maxLength;
            txtAddress.MaxLength = Constants.textBoxMaxLength;
            #endregion
            
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;

            txtName.LostFocus += Txt_LostFocus;
            txtPhone.LostFocus += Txt_LostFocus;
            txtAddress.LostFocus += Txt_LostFocus;

            this.DataContext = this;
            this.Loaded += frmAddPublisher_Loaded;
        }

        private void frmAddPublisher_Loaded(object sender, RoutedEventArgs e)
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
            Item = new PublisherDto(getIdPublisher());
        }

        private void CopyItem()
        {
            var getItem = getItemToUpdate();
            Item = getItem.Clone() as PublisherDto;
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

            Publisher getPublisher = publisherVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(getPublisherRepo().Gets(), getPublisher, true, Constants.checkPropPublisher);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("publisher"));
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

            Publisher normalItem = publisherVM.CreateByDto(Item);
            Publisher normalSourceItem = publisherVM.CreateByDto(getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropPublisher);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(getPublisherRepo().Gets(), normalItem, true, Constants.checkPropPublisher);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("publisher"));
                    return;
                }
            }

           
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            publisherVM.Copy(Item, getItemToUpdate());
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
            if (Validation.GetHasError(txtPhone))
                return true;
            if (Validation.GetHasError(txtAddress))
                return true;
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtPhone.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtAddress.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();         
        }

        private void FormatValues()
        {
            txtName.Text = txtName.Text.Trim();
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
                    this.Title = "Add New Publisher Form";
                    lblTitle.Content = "Add New Publisher";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    this.Title = "Update Publisher Information Form";
                    lblTitle.Content = "Update Publisher Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
