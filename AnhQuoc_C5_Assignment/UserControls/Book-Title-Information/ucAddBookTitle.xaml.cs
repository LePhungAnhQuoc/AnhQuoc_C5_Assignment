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
        #endregion

        #region Properties
        private Category _SelectedCategory;
        public Category SelectedCategory
        {
            get
            {
                return _SelectedCategory;
            }
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged();
            }
        }

        private Author _SelectedAuthor;
        public Author SelectedAuthor
        {
            get
            {
                return _SelectedAuthor;
            }
            set
            {
                _SelectedAuthor = value;
                OnPropertyChanged();
            }
        }


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

        private ObservableCollection<Author> _Authors;
        public ObservableCollection<Author> Authors
        {
            get
            {
                return _Authors;
            }
            set
            {
                _Authors = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ResultProperties
        private BookTitle _Item;
        public BookTitle Item
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
            txtSummary.MaxLength = Constants.txtBookTitleSummaryMaxLength;
            #endregion

            btnConfirm.Click += btnConfirm_Click;
            btnCancel.Click += btnCancel_Click;

            txtName.LostFocus += TxtFormatValue_LostFocus;
            txtSummary.LostFocus += TxtFormatValue_LostFocus;

            this.Loaded += UcAddBookTitle_Loaded;
            this.DataContext = this;
        }

        private void UcAddBookTitle_Loaded(object sender, RoutedEventArgs e)
        {
            Item = new BookTitle();
            Item.Id = getId();

            Categories = getCategoryRepo().Gets();
            Authors = getAuthorRepo().Gets();
            allBookTitles = getBookTitleRepo().Gets();

            IsCheckValidForm = false;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAllSelecting())
                return;

            bool isCheckEmptyItem = bookTitleVM.IsCheckEmptyItem(Item);
            bool isHasError = IsValidationGetHasError();

            if (!isCheckEmptyItem || isHasError)
            {
                RunAllValidations();
                return;
            }
            
            // Truyền dữ liệu
            PassValueToItem(Item, SelectedAuthor, SelectedCategory);

            // Kiểm tra sự trùng lặp
            bool isExistInformation = Utilities.IsExistInformation(allBookTitles, Item, true, Constants.checkPropBookTitle);
            if (isExistInformation)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyBookTitleExistInfo());
                return;
            }

            AddNewBookTitle(Item);
            var message = Utilities.NotifyAddSuccessfully("book title");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
            IsCheckValidForm = true;
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

        private void PassValueToItem(BookTitle item, Author selectedAuthor, Category selectedCategory)
        {
            item.IdCategory = selectedCategory.Id;
            item.IdAuthor = selectedAuthor.Id;
        }

        private bool IsAllSelecting()
        {
            if (SelectedCategory == null)
            {
                Utilities.ShowMessageBox1("Please select category");
                return false;
            }
            if (SelectedAuthor == null)
            {
                Utilities.ShowMessageBox1("Please select author");
                return false;
            }
            return true;
        }

        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(txtName))
                return true;
            if (Validation.GetHasError(cbCategory))
                return true;
            if (Validation.GetHasError(cbAuthor))
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
            be = cbAuthor.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();
        }

        private void TxtFormatValue_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
        }
    }
}
