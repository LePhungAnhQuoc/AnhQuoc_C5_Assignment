using Microsoft.Win32;
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
    public class AddBookTitleViewModel: BaseViewModel<object>, IPageViewModel
    {
        #region Fields
        private ucAddBookTitle thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;

        private ObservableCollection<BookTitle> allBookTitles;
        private OpenFileDialog openFile;
        private string tempUrlImage;

        #endregion

        #region Properties
        private ObservableCollection<Category> _Categories;
        public ObservableCollection<Category> Categories
        {
            get
            {
                return _Categories;
            }
            set
            {
                _Categories = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ResultProperties
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

        public bool IsCheckValidForm { get; set; }
        #endregion

        #region ViewModels
        private BookTitleViewModel bookTitleVM;
        private AuthorViewModel authorVM;
        #endregion

        #region Mapping
        private AuthorMap authorMap;
        private CategoryMap categoryMap;
        #endregion

        #region Commands
        public RelayCommand LoadedCmd { get; private set; }
        public RelayCommand btnConfirmClickCmd { get; private set; }
        public RelayCommand btnCancelClickCmd { get; private set; }
        public RelayCommand btnBrowseImageClickCmd { get; private set; }
        #endregion

        public AddBookTitleViewModel()
        {
            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            authorVM = UnitOfViewModel.Instance.AuthorViewModel;
            authorMap = UnitOfMap.Instance.AuthorMap;

            #region Init-Commands
            LoadedCmd = new RelayCommand((para) => frmAddBookTitle_Loaded(para, null));
            btnConfirmClickCmd = new RelayCommand((para) => btnConfirm_Click(para, null));
            btnCancelClickCmd = new RelayCommand((para) => btnCancel_Click(para, null));
            btnBrowseImageClickCmd = new RelayCommand((para) => 
            BtnBrowseImage_Click(para, null));
            #endregion
        }


        private void frmAddBookTitle_Loaded(object sender, RoutedEventArgs e)
        {
            thisForm = sender as ucAddBookTitle;

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

            Categories = thisForm.getCategoryRepo().Gets();
            allBookTitles = thisForm.getBookTitleRepo().Gets();

            IsCheckValidForm = false;
            tempUrlImage = Item.UrlImage;
        }

        private void NewItem()
        {
            Item = new BookTitleDto(bookTitleVM.GetId());
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
            var getBookTitle = bookTitleVM.CreateByDto(Item);
            bool isExistInformation = Utilities.IsExistInformation(allBookTitles, getBookTitle, true, Constants.checkPropBookTitle);
            if (isExistInformation)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyBookTitleExistInfo());
                return;
            }



            AddNewBookTitle(getBookTitle);

            var message = Utilities.NotifyAddSuccessfully("book title");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);

            IsCheckValidForm = true;
            SaveImage();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;
        }

        public void AddNewBookTitle(BookTitle newItem)
        {
            thisForm.getBookTitleRepo().Add(newItem);
            thisForm.getBookTitleRepo().WriteAdd(newItem);
        }

        public void SaveImage()
        {
            Constants.rememberDirectoryOpenFile = openFile.FileName.Replace(openFile.SafeFileName, string.Empty);
            Utilities.SaveImage(openFile);
            if (tempUrlImage != null)
            {
                if (MessageBox.Show("Do you want remove an old image?", string.Empty,
           MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.OK) == MessageBoxResult.OK)
                {
                    Utilities.RemoveImage(tempUrlImage);
                }
            }
        }

        private void BtnBrowseImage_Click(object sender, RoutedEventArgs e)
        {
            openFile = new OpenFileDialog();
            openFile.Title = "Select Image";
            openFile.InitialDirectory = Constants.rememberDirectoryOpenFile;

            openFile.Filter = "Bitmaps|*.bmp|PNG files|*.png|JPEG files|*.jpg|GIF files|*.gif|TIFF files|*.tif|Image files|*.bmp;*.jpg;*.gif;*.png;*.tif";

            openFile.FilterIndex = 6;
            openFile.Multiselect = false;
            openFile.RestoreDirectory = false;

            if (openFile.ShowDialog() == true)
            {
                Item.UrlImage = Utilities.GetUrlImage(openFile);
            }
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.Text = txt.Text.Trim();
        }
    }
}
