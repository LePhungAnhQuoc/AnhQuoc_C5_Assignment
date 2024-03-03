using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class AddBookTitleViewModel<T, TDto> : BaseViewModel<object>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }
        private frmAddBookTitle thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;

        private OpenFileDialog openFile;
        private string tempUrlImage;

        #endregion

        #region Properties
        private ObservableCollection<Category> _AllCategorys;
        public ObservableCollection<Category> AllCategorys
        {
            get { return _AllCategorys; }
            set 
            { 
                _AllCategorys = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region ViewModels
        private BookTitleViewModel bookTitleVM;
        private CategoryViewModel categoryVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private BookTitleMap bookTitleMap;
        #endregion

        #region ResultProperty
        private BookTitleDto _Item;
        public BookTitleDto Item
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
        public RelayCommand btnBrowseImageClickCmd { get; private set; }
        #endregion

        public AddBookTitleViewModel()
        {
            openFile = Utilities.CreateOpenFileDialog();

            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            categoryVM = UnitOfViewModel.Instance.CategoryViewModel;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;

            AllCategorys = categoryVM.Repo.Gets();

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => frmAddBookTitle_Loaded(para, null));
            ClosingCmd = new RelayCommand(para => onClosing(para, null));
            btnConfirmClickCmd = new RelayCommand((para) => BtnConfirm_Click(para, null));
            btnCancelClickCmd = new RelayCommand((para) => BtnCancel_Click(para, null));
            btnUpdateClickCmd = new RelayCommand((para) => BtnUpdate_Click(para, null));
            btnResetClickCmd = new RelayCommand((para) => BtnReset_Click(para, null));
            btnBrowseImageClickCmd = new RelayCommand((para) =>
BtnBrowseImage_Click(para, null));

            #endregion
        }

        public void onLoaded(object sender, RoutedEventArgs e)
        {
            IsCancel = true;

            thisForm = sender as frmAddBookTitle;

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

            tempUrlImage = Item.UrlImage;
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

            BookTitle getBookTitle = bookTitleVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(thisForm.getBookTitleRepo().Gets(), getBookTitle, true, Constants.checkPropBookTitle);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("book title"));
                return;
            }

            Utilities.SaveImageInUserControl(tempUrlImage, Item, openFile);
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

            BookTitle normalItem = bookTitleVM.CreateByDto(Item);
            BookTitle normalSourceItem = bookTitleVM.CreateByDto(thisForm.getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropBookTitle);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(thisForm.getBookTitleRepo().Gets(), normalItem, true, Constants.checkPropBookTitle);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("book title"));
                    return;
                }
            }

            Utilities.SaveImageInUserControl(tempUrlImage, Item, openFile);

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            bookTitleVM.Copy(Item, thisForm.getItemToUpdate());
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
            Item = new BookTitleDto(thisForm.getIdBookTitle());
        }

        private void CopyItem()
        {
            var getItem = thisForm.getItemToUpdate();
            Item = getItem.Clone() as BookTitleDto;
        }


        private void SetFormByAddOrUpdate(string feature)
        {
            switch (feature)
            {
                case "ADD":
                    thisForm.Title = "Add New Book Title Form";
                    thisForm.lblTitle.Content = "Add New Book Title";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    thisForm.btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    thisForm.Title = "Update Book Title Information Form";
                    thisForm.lblTitle.Content = "Update Book Title Information";

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


        private void BtnBrowseImage_Click(object sender, RoutedEventArgs e)
        {
            if (openFile.ShowDialog() == true)
            {
                Item.UrlImage = Utilities.GetUrlImage(openFile);
            }
        }

    }
}
