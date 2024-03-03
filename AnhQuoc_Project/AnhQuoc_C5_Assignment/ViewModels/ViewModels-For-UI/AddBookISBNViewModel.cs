using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class AddBookISBNViewModel<T, TDto> : BaseViewModel<object>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }
        private frmAddBookISBN thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;
        #endregion

        #region Properties
        private ObservableCollection<BookTitle> _AllBookTitles;
        public ObservableCollection<BookTitle> AllBookTitles
        {
            get { return _AllBookTitles; }
            set 
            {
                _AllBookTitles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Author> _AllAuthors;

        public ObservableCollection<Author> AllAuthors
        {
            get { return _AllAuthors; }
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

        #region ViewModels
        private BookISBNViewModel bookISBNVM;
        private BookTitleViewModel bookTitleVM;
        private AuthorViewModel authorVM;
        private ParameterViewModel paraVM;

        #endregion

        #region Mapping
        private BookISBNMap bookISBNMap;
        #endregion

        #region ResultProperty
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
        #endregion

        #region Commands
        public RelayCommand LoadedCmd { get; private set; }
        public RelayCommand ClosingCmd { get; private set; }
        public RelayCommand btnConfirmClickCmd { get; private set; }
        public RelayCommand btnCancelClickCmd { get; private set; }
        public RelayCommand btnUpdateClickCmd { get; private set; }
        public RelayCommand btnResetClickCmd { get; private set; }
        #endregion

        public AddBookISBNViewModel()
        {
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            authorVM = UnitOfViewModel.Instance.AuthorViewModel;
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;

            AllBookTitles = bookTitleVM.Repo.Gets();
            AllAuthors = authorVM.Repo.Gets();
            AllLanguages = Utilities.GetListAllLanguages();

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => frmAddBookISBN_Loaded(para, null));
            ClosingCmd = new RelayCommand(para => onClosing(para, null));
            btnConfirmClickCmd = new RelayCommand((para) => BtnConfirm_Click(para, null));
            btnCancelClickCmd = new RelayCommand((para) => BtnCancel_Click(para, null));
            btnUpdateClickCmd = new RelayCommand((para) => BtnUpdate_Click(para, null));
            btnResetClickCmd = new RelayCommand((para) => BtnReset_Click(para, null));
            #endregion
        }

        public void onLoaded(object sender, RoutedEventArgs e)
        {
            IsCancel = true;

            thisForm = sender as frmAddBookISBN;

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

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            Utilities.RunAllValidations(mainContentControls);

            bool isHasError = Utilities.IsValidationGetHasError(mainContentControls);
            if (isHasError)
            {
                return;
            }

            BookISBN getBookISBN = bookISBNVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(thisForm.getBookISBNRepo().Gets(), getBookISBN, true, Constants.checkPropBookISBN);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("book ISBN"));
                return;
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

            BookISBN normalItem = bookISBNVM.CreateByDto(Item);
            BookISBN normalSourceItem = bookISBNVM.CreateByDto(thisForm.getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropBookISBN);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(thisForm.getBookISBNRepo().Gets(), normalItem, true, Constants.checkPropBookISBN);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("book ISBN"));
                    return;
                }
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            bookISBNVM.Copy(Item, thisForm.getItemToUpdate());
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e, bool isClosed = false)
        {
            if (IsCancel)
            {
                Item = null;
            }


            if (!isClosed)
                thisForm.Close();
        }


        private void NewItem()
        {
            Item = new BookISBNDto(thisForm.getIdBookISBN());
            Item.Status = false; //!!
        }

        private void CopyItem()
        {
            var getItem = thisForm.getItemToUpdate();
            Item = getItem.Clone() as BookISBNDto;
        }


        private void SetFormByAddOrUpdate(string feature)
        {
            switch (feature)
            {
                case "ADD":
                    thisForm.Title = "Add New Book ISBN Form";
                    thisForm.lblTitle.Content = "Add New Book ISBN";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    thisForm.btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    thisForm.Title = "Update Book ISBN Information Form";
                    thisForm.lblTitle.Content = "Update Book ISBN Information";

                    thisForm.stkUpdateButton.Visibility = Visibility.Visible;
                    thisForm.btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.Text = txt.Text.Trim();
        }
    }
}
