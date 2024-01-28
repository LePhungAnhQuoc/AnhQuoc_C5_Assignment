using Microsoft.Win32;
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
    /// Interaction logic for ucAddBookTitle.xaml
    /// </summary>
    public partial class ucAddBookTitle : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<string> getId { get; set; }
        public Func<BookTitleRepository> getBookTitleRepo { get; set; }
        public Func<CategoryRepository> getCategoryRepo { get; set; }
        public Func<AuthorRepository> getAuthorRepo { get; set; }
        #endregion

        #region Fields
        private ObservableCollection<BookTitle> allBookTitles;
        private OpenFileDialog openFile;
        private string tempUrlImage;
        #endregion

        #region Properties
        private ObservableCollection<CategoryDto> _Categories;
        public ObservableCollection<CategoryDto> Categories
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

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion


        public ucAddBookTitle()
        {
            InitializeComponent();

            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            authorVM = UnitOfViewModel.Instance.AuthorViewModel;
            authorMap = UnitOfMap.Instance.AuthorMap;

            #region Set-Max-Length-For-TextBoxs
            int maxLength = Constants.textBoxMaxLength;

            txtName.MaxLength = maxLength;
            txtSummary.MaxLength = Constants.textAreaMaxLength;
            txtUrlImage.MaxLength = Constants.textAreaMaxLength;
            #endregion

            btnConfirm.Click += btnConfirm_Click;
            btnCancel.Click += btnCancel_Click;
            btnBrowseImage.Click += BtnBrowseImage_Click;

            txtName.LostFocus += TxtFormatValue_LostFocus;
            txtSummary.LostFocus += TxtFormatValue_LostFocus;

            this.Loaded += UcAddBookTitle_Loaded;
            this.DataContext = this;
        }

        private void UcAddBookTitle_Loaded(object sender, RoutedEventArgs e)
        {
            Item = new BookTitleDto(getId());

            Categories = categoryMap.ConvertToDto(getCategoryRepo().Gets());
            allBookTitles = getBookTitleRepo().Gets();

            IsCheckValidForm = false;

            tempUrlImage = Item.UrlImage;
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
            getBookTitleRepo().Add(newItem);
            getBookTitleRepo().WriteAdd(newItem);
        }
        
        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(txtName))
                return true;
            if (Validation.GetHasError(cbCategory))
                return true;
            if (Validation.GetHasError(txtSummary))
                return true;
            if (Validation.GetHasError(txtNote))
                return true;
            if (Validation.GetHasError(txtUrlImage))
                return true;
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = cbCategory.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();
            be = txtSummary.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtNote.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtUrlImage.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        private void TxtFormatValue_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
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
    }
}
