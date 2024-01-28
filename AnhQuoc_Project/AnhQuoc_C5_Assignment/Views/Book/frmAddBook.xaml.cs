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
    /// Interaction logic for frmAddBook.xaml
    /// </summary>
    public partial class frmAddBook : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<BookDto> getItemToUpdate { get; set; }

        public Func<int> getIdBook { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region ViewModels
        private BookViewModel bookVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private BookMap bookMap;
        #endregion

        #region ResultProperty
        private BookDto _Item;
        public BookDto Item
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

        public frmAddBook()
        {
            InitializeComponent();

            bookVM = UnitOfViewModel.Instance.BookViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            bookMap = UnitOfMap.Instance.BookMap;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtPrice.MaxLength = maxLength;
            txtNote.MaxLength = Constants.textBoxMaxLength;
            #endregion
            
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;

            txtPrice.LostFocus += Txt_LostFocus;
            txtNote.LostFocus += Txt_LostFocus;
        
            this.DataContext = this;
            this.Loaded += frmAddBook_Loaded;
        }

        private void frmAddBook_Loaded(object sender, RoutedEventArgs e)
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
            Item = new BookDto(getIdBook());
            Item.Status = true;
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void CopyItem()
        {
            var getItem = getItemToUpdate();
            Item = getItem.Clone() as BookDto;

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

            Book getBook = bookVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(getBookRepo().Gets(), getBook, true, Constants.checkPropBook);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("book"));
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

            Book normalItem = bookVM.CreateByDto(Item);
            Book normalSourceItem = bookVM.CreateByDto(getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropBook);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(getBookRepo().Gets(), normalItem, true, Constants.checkPropBook);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("book"));
                    return;
                }
            }
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            bookVM.Copy(Item, getItemToUpdate());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;
            this.Close();
        }
        
        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(txtPrice))
                return true;
            if (Validation.GetHasError(txtNote))
                return true;
            if (Validation.GetHasError(cbISBN))
                return true;

            if (Validation.GetHasError(cbPublisher))
                return true;
            if (Validation.GetHasError(cbTranslator))
                return true;
            if (Validation.GetHasError(cbLanguage))
                return true;

            if (Validation.GetHasError(datePublishDate))
                return true;
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtPrice.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();

            be = txtNote.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();

            be = cbISBN.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();

            be = cbPublisher.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();

            be = cbTranslator.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();

            be = cbLanguage.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();

            be = datePublishDate.GetBindingExpression(DatePicker.SelectedDateProperty);
            be.UpdateSource();
        }

        private void FormatValues()
        {
            txtNote.Text = txtNote.Text.Trim();
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
                    this.Title = "Add New Book Form";
                    lblTitle.Content = "Add New Book";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    this.Title = "Update Book Information Form";
                    lblTitle.Content = "Update Book Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
