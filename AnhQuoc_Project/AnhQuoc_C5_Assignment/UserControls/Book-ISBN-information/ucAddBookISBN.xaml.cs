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
    /// Interaction logic for ucAddBookISBN.xaml
    /// </summary>
    public partial class ucAddBookISBN : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<BookTitleRepository> getBookTitleRepo { get; set; }
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<AuthorRepository> getAuthorRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        private ObservableCollection<BookTitleDto> _AllBookTitleDtos;
        public ObservableCollection<BookTitleDto> AllBookTitleDtos
        {
            get
            {
                return _AllBookTitleDtos;
            }
            set
            {
                _AllBookTitleDtos = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<AuthorDto> _AllAuthorDtos;
        public ObservableCollection<AuthorDto> AllAuthorDtos
        {
            get
            {
                return _AllAuthorDtos;
            }
            set
            {
                _AllAuthorDtos = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _AllLanguages;
        public ObservableCollection<string> AllLanguages
        {
            get { return _AllLanguages; }
            set
            {
                _AllLanguages = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ResultProperties
        private BookISBNDto _Item;
        public BookISBNDto Item
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
         
        public bool IsCheckValidForm { get; set; }
        #endregion

        #region ViewModels
        private BookISBNViewModel bookISBNVM;
        private AuthorViewModel authorVM;
        #endregion

        #region Mapping
        private AuthorMap authorMap;
        private BookTitleMap bookTitleMap;
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

        public ucAddBookISBN()
        {
            InitializeComponent();

            #region SetTextBoxMaxLength
            #endregion

            Loaded += UcAddBookISBN_Loaded;
            btnConfirm.Click += btnConfirm_Click;
            btnCancel.Click += btnCancel_Click;

            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            authorVM = UnitOfViewModel.Instance.AuthorViewModel;

            authorMap = UnitOfMap.Instance.AuthorMap;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;

            this.DataContext = this;
        }

        private void UcAddBookISBN_Loaded(object sender, RoutedEventArgs e)
        {
            Item = new BookISBNDto(bookISBNVM.GetId());
            Item.Status = true;

            var allAuthors = getAuthorRepo().Gets();
            AllAuthorDtos = authorMap.ConvertToDto(allAuthors);

            var allBookTitles = getBookTitleRepo().Gets();
            AllBookTitleDtos = bookTitleMap.ConvertToDto(allBookTitles);

            AllLanguages = Utilities.GetListAllLanguages();
            IsCheckValidForm = false;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            RunAllValidations();
            bool isHasError = this.IsValidationGetHasError();
            if (isHasError)
            {
                return;
            }

            // Kiểm tra sự trùng lặp
            var getBookISBN = bookISBNVM.CreateByDto(Item);
            bool isExistInformation = Utilities.IsExistInformation(getBookISBNRepo().Gets(), getBookISBN, true, Constants.checkPropBookISBN);
            if (isExistInformation)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyBookISBNExistInfo());
                return;
            }

            AddNewBookISBN(getBookISBN);
            IsCheckValidForm = true;
            var message = Utilities.NotifyAddSuccessfully("Book ISBN");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;
        }

        public void AddNewBookISBN(BookISBN newItem)
        {
            getBookISBNRepo().Add(newItem);
            getBookISBNRepo().WriteAdd(newItem);
        }
     
        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(cbBookTitle))
                return true;
            if (Validation.GetHasError(cbAuthor))
                return true;
            if (Validation.GetHasError(cbLanguage))
                return true;
            if (Validation.GetHasError(txtDescription))
                return true;
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = cbBookTitle.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();
            be = cbAuthor.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();
            be = cbLanguage.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();
            be = txtDescription.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }
    }
}
