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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucAddBook.xaml
    /// </summary>
    public partial class ucAddBook : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }

        public Func<ucBookManagement> getUcBookManagement { get; set; }
        #endregion

        #region Properties
        private string _QuantityValue;
        public string QuantityValue
        {
            get { return _QuantityValue; }
            set
            {
                _QuantityValue = value;
                OnPropertyChanged();
            }
        }


        private BookISBN _SelectedBookISBN;
        public BookISBN SelectedBookISBN
        {
            get
            {
                return _SelectedBookISBN;
            }
            set
            {
                _SelectedBookISBN = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BookISBN> _BookISBNs;
        public ObservableCollection<BookISBN> BookISBNs
        {
            get
            {
                return _BookISBNs;
            }
            set
            {
                _BookISBNs = value;
                OnPropertyChanged();
            }
        }
       
        #endregion

        #region ViewModels
        private BookViewModel bookVM;
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


        public ucAddBook()
        {
            InitializeComponent();
            bookVM = UnitOfViewModel.Instance.BookViewModel;

            #region SetTextBoxMaxLength
            #endregion

            btnAdd.Click += BtnAdd_Click;
            btnCancel.Click += BtnCancel_Click;
            txtInputQuantity.LostFocus += TxtFormatValue_LostFocus;

            this.Loaded += UcAddBook_Loaded;
            this.DataContext = this;
        }

        private void UcAddBook_Loaded(object sender, RoutedEventArgs e)
        {
            BookISBNs = getBookISBNRepo().Gets();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAllSelecting())
            {
                return;
            }
            bool isCheckEmptyItem = true;
            bool isCheckQuantity = this.IsCheckEmptyQuantityValue(QuantityValue);
            bool isHasError = this.IsValidationGetHasError();

            if (!isCheckEmptyItem || !isCheckQuantity || isHasError)
            {
                RunAllValidations();
                return;
            }
            
            ObservableCollection<Book> getNewBooks = new ObservableCollection<Book>();
            int quantity = Convert.ToInt32(QuantityValue);

            #region GetIndexId
            Book temp = new Book();
            int indexId = bookVM.getMaxIndexId(nameof(temp.Id));
            #endregion

            for (int i = 0; i < quantity; i++)
            {
                indexId++;
                int bookId = bookVM.GetId(indexId);

                Book newBook = new Book();
                newBook.Id = bookId;
                newBook.ISBN = SelectedBookISBN.ISBN;
                newBook.Status = true;

                newBook.CreatedAt = DateTime.Now;
                newBook.ModifiedAt = DateTime.Now;

                getNewBooks.Add(newBook);
            }
            getUcBookManagement().AddNewBooks(getNewBooks);
            ClearAllValues();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            getUcBookManagement().CancelAddNewBooks();
            ClearAllValues();
        }

        private void PassValueToItem(Book item, BookISBN selectedBookISBN)
        {
            item.ISBN = selectedBookISBN.ISBN;
        }

        private void ClearAllValues()
        {
            cbBookISBNs.SelectedItem = null;
            txtInputQuantity.Text = string.Empty;
        }

        private bool IsAllSelecting()
        {
            if (SelectedBookISBN == null)
            {
                Utilities.ShowMessageBox1("Please select book ISBN");
                return false;
            }
            return true;
        }

        public bool IsCheckEmptyQuantityValue(string quantityValue)
        {
            if (Utilities.IsCheckEmptyString(quantityValue))
            {
                return false;
            }
            return true;
        }

        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(cbBookISBNs))
                return true;
            if (Validation.GetHasError(txtInputQuantity))
                return true;
            return false;
        }
        
        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtInputQuantity.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = cbBookISBNs.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();
        }
        private void TxtFormatValue_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
        }
    }
}
