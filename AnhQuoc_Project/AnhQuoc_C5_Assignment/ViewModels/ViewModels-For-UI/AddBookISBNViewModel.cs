using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class AddBookISBNViewModel: BaseViewModel<object>, IPageViewModel
    {
        #region Fields
        private ucAddBookISBN thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;
        #endregion

        #region Properties
        private ObservableCollection<BookTitle> _AllBookTitles;
        public ObservableCollection<BookTitle> AllBookTitles
        {
            get
            {
                return _AllBookTitles;
            }
            set
            {
                _AllBookTitles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Author> _AllAuthors;
        public ObservableCollection<Author> AllAuthors
        {
            get
            {
                return _AllAuthors;
            }
            set
            {
                _AllAuthors = value;
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

        #region Commands
        public RelayCommand LoadedCmd { get; private set; }
        public RelayCommand btnConfirmClickCmd { get; private set; }
        public RelayCommand btnCancelClickCmd { get; private set; }
        #endregion

        public AddBookISBNViewModel()
        {
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            authorVM = UnitOfViewModel.Instance.AuthorViewModel;

            authorMap = UnitOfMap.Instance.AuthorMap;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;

            #region Init-Commands
            LoadedCmd = new RelayCommand((para) => frmAddBookISBN_Loaded(para, null));
            btnConfirmClickCmd = new RelayCommand((para) => btnConfirm_Click(para, null));
            btnCancelClickCmd = new RelayCommand((para) => btnCancel_Click(para, null));
            #endregion
        }

        private void frmAddBookISBN_Loaded(object sender, RoutedEventArgs e)
        {
            thisForm = sender as ucAddBookISBN;

            mainContentControls = new List<DependencyObject>();
            foreach (DependencyObject child in thisForm.mainContent.Children)
            {
                mainContentControls.AddRange(Utilities.GetControlHaveValidationRules(child));
            }

            TextBoxes = mainContentControls.Where(obj => obj is TextBox).Select(obj => obj as TextBox).ToList();
            foreach (TextBox textBox in TextBoxes)
            {
                textBox.MaxLength = Constants.textBoxMaxLength;
                textBox.LostFocus += Txt_LostFocus;
            }
            
            NewItem();

            AllAuthors = thisForm.getAuthorRepo().Gets();
            AllBookTitles = thisForm.getBookTitleRepo().Gets();
            AllLanguages = Utilities.GetListAllLanguages();
            IsCheckValidForm = false;
        }

        private void NewItem()
        {
            Item = new BookISBNDto(bookISBNVM.GetId());
            Item.Status = true;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            Utilities.RunAllValidations(mainContentControls);

            bool isHasError = Utilities.IsValidationGetHasError(mainContentControls);
            if (isHasError)
            {
                return;
            }

            // Kiểm tra sự trùng lặp
            var getBookISBN = bookISBNVM.CreateByDto(Item);

            bool isExistInformation = Utilities.IsExistInformation(thisForm.getBookISBNRepo().Gets(), getBookISBN, true, Constants.checkPropBookISBN);

            if (isExistInformation)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyBookISBNExistInfo());
                return;
            }


            // Add new Item
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
            thisForm.getBookISBNRepo().Add(newItem);
            thisForm.getBookISBNRepo().WriteAdd(newItem);
        }



        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.Text = txt.Text.Trim();
        }
    }
}
