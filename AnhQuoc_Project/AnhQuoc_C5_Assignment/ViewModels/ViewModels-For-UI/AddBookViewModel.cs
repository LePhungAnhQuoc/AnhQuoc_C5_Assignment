using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class AddBookViewModel: BaseViewModel<object>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }
        private frmAddBook thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;
        #endregion

        #region Properties

        private int _InputQuantity;
        public int InputQuantity
        {
            get { return _InputQuantity; }
            set
            {
                _InputQuantity = value;
                OnPropertyChanged();
            }
        }

        private List<int> _ListId;
        public List<int> ListId
        {
            get { return _ListId; }
            set
            {
                _ListId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BookISBN> _AllBookISBNs;

        public ObservableCollection<BookISBN> AllBookISBNs
        {
            get { return _AllBookISBNs; }
            set 
            {
                _AllBookISBNs = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Publisher> _AllPublishers;

        public ObservableCollection<Publisher> AllPublishers
        {
            get { return _AllPublishers; }
            set
            {
                _AllPublishers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Translator> _AllTranslators;
        public ObservableCollection<Translator> AllTranslators
        {
            get { return _AllTranslators; }
            set
            {
                _AllTranslators = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BookStatu> _AllBookStatus;
        public ObservableCollection<BookStatu> AllBookStatus
        {
            get { return _AllBookStatus; }
            set
            {
                _AllBookStatus = value;
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

        private ObservableCollection<Book> _BooksResult;

        public ObservableCollection<Book> BooksResult
        {
            get { return _BooksResult; }
            set
            { 
                _BooksResult = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region ViewModels
        private BookISBNViewModel bookISBNVM;
        private PublisherViewModel publisherVM;
        private TranslatorViewModel translatorVM;
        private BookStatusViewModel bookStatusVM;
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

        #region Commands
        public RelayCommand LoadedCmd { get; private set; }
        public RelayCommand ClosingCmd { get; private set; }
        public RelayCommand btnConfirmClickCmd { get; private set; }
        public RelayCommand btnCancelClickCmd { get; private set; }
        public RelayCommand btnUpdateClickCmd { get; private set; }
        public RelayCommand btnResetClickCmd { get; private set; }
        #endregion

        public AddBookViewModel()
        {
            bookVM = UnitOfViewModel.Instance.BookViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            publisherVM = UnitOfViewModel.Instance.PublisherViewModel;
            translatorVM = UnitOfViewModel.Instance.TranslatorViewModel;
            bookStatusVM = UnitOfViewModel.Instance.BookStatusViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            bookMap = UnitOfMap.Instance.BookMap;

            AllBookISBNs = bookISBNVM.Repo.Gets();
            AllPublishers = publisherVM.Repo.Gets();
            AllTranslators = translatorVM.Repo.Gets();
            AllBookStatus = bookStatusVM.Repo.Gets();
            AllLanguages = Utilities.GetListAllLanguages();

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => frmAddBook_Loaded(para, null));
            ClosingCmd = new RelayCommand((para) => onClosing(para, null));

            btnConfirmClickCmd = new RelayCommand((para) => BtnConfirm_Click(para, null));
            btnCancelClickCmd = new RelayCommand((para) => BtnCancel_Click(para, null));
            btnUpdateClickCmd = new RelayCommand((para) => BtnUpdate_Click(para, null));
            btnResetClickCmd = new RelayCommand((para) => BtnReset_Click(para, null));
            #endregion

        }


        public void onLoaded(object sender, RoutedEventArgs e)
        {
            InputQuantity = 1;

            IsCancel = true;

            thisForm = sender as frmAddBook;

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



            if (thisForm.getItemToUpdate == null)
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


        private void onClosing(object sender, CancelEventArgs e)
        {
            BtnCancel_Click(null, null, true);
        }


        private void NewItem()
        {
            Item = new BookDto(thisForm.getIdBook());
            Item.Status = true;
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void CopyItem()
        {
            var getItem = thisForm.getItemToUpdate();
            Item = getItem.Clone() as BookDto;

            Item.ModifiedAt = DateTime.Now;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            Utilities.RunAllValidations(mainContentControls);

            bool isHasError = Utilities.IsValidationGetHasError(mainContentControls);
            if (isHasError)
            {
                return;
            }

            //bool isCheck = Utilities.IsExistInformation(thisForm.getBookRepo().Gets(), getBook, true, Constants.checkPropBook);
            //if (isCheck)
            //{
            //    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("book"));
            //    return;
            //}

            BooksResult = new ObservableCollection<Book>();
            for (int i = 0; i < ListId.Count; i++)
            {
                Book getBook = bookVM.CreateByDto(Item);
                getBook.Id = ListId[i];

                BooksResult.Add(getBook);
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            Utilities.RunAllValidations(mainContentControls);

            bool isHasError = Utilities.IsValidationGetHasError(mainContentControls);
            if (isHasError)
            {
                return;
            }

            Book normalItem = bookVM.CreateByDto(Item);
            Book normalSourceItem = bookVM.CreateByDto(thisForm.getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropBook);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(thisForm.getBookRepo().Gets(), normalItem, true, Constants.checkPropBook);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("book"));
                    return;
                }
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            bookVM.Copy(Item, thisForm.getItemToUpdate());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e, bool isClosed = false)
        {
            if (IsCancel)
                Item = null;

            if (!isClosed)
                thisForm.Close();
        }


        public void TxtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            Item.PriceCurrent = Item.Price;
        }


        public void TxtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListId = bookVM.GetListId(InputQuantity);

            thisForm.txtIds.Text = string.Join(", ", ListId);
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
                    thisForm.Title = "Add New Book Form";
                    thisForm.lblTitle.Content = "Add New Book";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    thisForm.btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    thisForm.Title = "Update Book Information Form";
                    thisForm.lblTitle.Content = "Update Book Information";

                    thisForm.stkUpdateButton.Visibility = Visibility.Visible;
                    thisForm.btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
