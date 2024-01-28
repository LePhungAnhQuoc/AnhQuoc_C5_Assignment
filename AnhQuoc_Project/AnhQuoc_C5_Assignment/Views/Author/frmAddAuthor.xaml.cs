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
    /// Interaction logic for frmAddAuthor.xaml
    /// </summary>
    public partial class frmAddAuthor : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<AuthorDto> getItemToUpdate { get; set; }

        public Func<string> getIdAuthor { get; set; }
        public Func<AuthorRepository> getAuthorRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region ViewModels
        private AuthorViewModel authorVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private AuthorMap authorMap;
        #endregion

        #region ResultProperty
        private AuthorDto _Item;
        public AuthorDto Item
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

        public frmAddAuthor()
        {
            InitializeComponent();

            authorVM = UnitOfViewModel.Instance.AuthorViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            authorMap = UnitOfMap.Instance.AuthorMap;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtName.MaxLength = maxLength;
            txtDescription.MaxLength = Constants.textAreaMaxLength;
            txtSummary.MaxLength = Constants.textAreaMaxLength;
            #endregion
            
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;

            txtName.LostFocus += Txt_LostFocus;
            txtSummary.LostFocus += Txt_LostFocus;
            txtDescription.LostFocus += Txt_LostFocus;

            this.DataContext = this;
            this.Loaded += frmAddAuthor_Loaded;
        }

        private void frmAddAuthor_Loaded(object sender, RoutedEventArgs e)
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
            Item = new AuthorDto(getIdAuthor());
            Item.Status = true;
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void CopyItem()
        {
            var getItem = getItemToUpdate();
            Item = getItem.Clone() as AuthorDto;

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

            Author getAuthor = authorVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(getAuthorRepo().Gets(), getAuthor, true, Constants.checkPropAuthor);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("author"));
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

            Author normalItem = authorVM.CreateByDto(Item);
            Author normalSourceItem = authorVM.CreateByDto(getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropAuthor);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(getAuthorRepo().Gets(), normalItem, true, Constants.checkPropAuthor);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("author"));
                    return;
                }
            }

           
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            authorVM.Copy(Item, getItemToUpdate());
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
            if (Validation.GetHasError(dateboF))
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
            be = dateboF.GetBindingExpression(TextBox.TextProperty);
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
                    this.Title = "Add New Author Form";
                    lblTitle.Content = "Add New Author";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    this.Title = "Update Author Information Form";
                    lblTitle.Content = "Update Author Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
