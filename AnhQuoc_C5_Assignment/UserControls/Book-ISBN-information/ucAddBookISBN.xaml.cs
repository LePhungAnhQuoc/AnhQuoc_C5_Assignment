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
        private BookTitleDto _SelectedBookTitleDto;
        public BookTitleDto SelectedBookTitleDto
        {
            get
            {
                return _SelectedBookTitleDto;
            }
            set
            {
                _SelectedBookTitleDto = value;
                OnPropertyChanged();
            }
        }

        private AuthorDto _SelectedAuthorDto;
        public AuthorDto SelectedAuthorDto
        {
            get
            {
                return _SelectedAuthorDto;
            }
            set
            {
                _SelectedAuthorDto = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedLanguage;
        public string SelectedLanguage
        {
            get { return _SelectedLanguage; }
            set
            {
                _SelectedLanguage = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _SelectedPublishDate;
        public DateTime? SelectedPublishDate
        {
            get { return _SelectedPublishDate; }
            set
            {
                _SelectedPublishDate = value;
                OnPropertyChanged();
            }
        }


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
        private BookISBN _Item;
        public BookISBN Item
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
            Item = new BookISBN();
            Item.ISBN = bookISBNVM.GetId();

            var allAuthors = getAuthorRepo().Gets();
            AllAuthorDtos = authorMap.ConvertToDto(allAuthors);

            var allBookTitles = getBookTitleRepo().Gets();
            AllBookTitleDtos = bookTitleMap.ConvertToDto(allBookTitles);

            AllLanguages = Utilities.GetListAllLanguages();

            IsCheckValidForm = false;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAllSelecting())
                return;

            bool isHasError = IsValidationGetHasError();
            if (isHasError)
            {
                RunAllValidations();
                return;
            }

            // Truyền dữ liệu
            Author selectedAuthor = authorVM.FindById(SelectedAuthorDto.Id);
            PassValueToItem(Item, SelectedBookTitleDto, selectedAuthor, SelectedLanguage, SelectedPublishDate);

            // Kiểm tra sự trùng lặp
            bool isExistInformation = Utilities.IsExistInformation(getBookISBNRepo().Gets(), Item, true, Constants.checkPropBookISBN);
            if (isExistInformation)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyBookISBNExistInfo());
                return;
            }

            AddNewBookISBN(Item);
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

        private void PassValueToItem(BookISBN item, BookTitleDto selectedBookTitleDto, Author selectedAuthor, string selectedLanguage, DateTime? selectedPublishDate)
        {
            item.IdBookTitle = selectedBookTitleDto.Id;
            item.IdAuthor = selectedAuthor.Id;
            item.Language = selectedLanguage;
            item.PublishDate = (DateTime)selectedPublishDate;
        }

        private bool IsAllSelecting()
        {
            if (SelectedBookTitleDto == null)
            {
                Utilities.ShowMessageBox1("Please select book title");
                return false;
            }

            if (SelectedAuthorDto == null)
            {
                Utilities.ShowMessageBox1("Please select author");
                return false;
            }

            if (SelectedLanguage == null)
            {
                Utilities.ShowMessageBox1("Please select language");
                return false;
            }

            if (SelectedPublishDate == null)
            {
                Utilities.ShowMessageBox1("Please select publish date");
                return false;
            }
            return true;
        }

        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(cbBookTitle))
                return true;
            if (Validation.GetHasError(cbAuthor))
                return true;
            if (Validation.GetHasError(datePublishDate))
                return true;
            if (Validation.GetHasError(cbLanguage))
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
            be = datePublishDate.GetBindingExpression(DatePicker.SelectedDateProperty);
            be.UpdateSource();
            be = cbLanguage.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();
        }
    }
}
