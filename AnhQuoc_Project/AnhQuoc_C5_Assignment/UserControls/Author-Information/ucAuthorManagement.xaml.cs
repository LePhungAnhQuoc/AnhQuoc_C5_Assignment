using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for ucAuthorManagement.xaml
    /// </summary>
    public partial class ucAuthorManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<AuthorRepository> getAuthorRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private AuthorViewModel authorVM;
        private BookISBNViewModel bookISBNVM;
        #endregion

        #region Mapping
        private AuthorMap authorMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<Author> listFillAuthors;
        #endregion

        #region Properties

        #endregion

        #region propDp

        #region IsAllow-Features
        public bool IsAllowViewInformation
        {
            get { return (bool)GetValue(IsAllowViewInformationProperty); }
            set { SetValue(IsAllowViewInformationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowViewInformation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowViewInformationProperty =
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucAuthorManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucAuthorManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucAuthorManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucAuthorManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucAuthorManagement), new PropertyMetadata(true));


        #endregion

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

        public ucAuthorManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillAuthors = new ObservableCollection<Author>();

            authorVM = UnitOfViewModel.Instance.AuthorViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;

            authorMap = UnitOfMap.Instance.AuthorMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucAuthorsTable.btnInfoClick += UcAuthorsTable_btnInfoClick;
            ucAuthorsTable.btnUpdateClick += UcAuthorsTable_btnUpdateClick;
            ucAuthorsTable.btnDeleteClick += UcAuthorsTable_btnDeleteClick;
            ucAuthorsTable.btnRestoreClick += UcAuthorsTable_btnRestoreClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucAuthorManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucAuthorManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucAuthorsTable.getItem_Status = () => null;

            AddToListFill(getAuthorRepo().Gets());
            AddItemsToDataGrid(listFillAuthors);

            #region IsAllow-Feature
            ucAuthorsTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            ucAuthorsTable.IsAllowUpdate = IsAllowUpdate;
            ucAuthorsTable.IsAllowDelete = IsAllowDelete;
            ucAuthorsTable.IsAllowRestore = IsAllowRestore;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var source = getAuthorRepo().Gets();

            var results = authorVM.FillByTextSearch(source, txtSearch.Text, true);

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }
        #endregion

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddAuthor frmAddAuthor = MainWindow.UnitOfForm.FrmAddAuthor(true);
            frmAddAuthor.getIdAuthor = () => authorVM.GetId();
            frmAddAuthor.ShowDialog();
         
            if (frmAddAuthor.Context.Item == null) // Cancel the operation
            {
                return;
            }

            AuthorDto newAuthorDto = frmAddAuthor.Context.Item;

            #region AddNewItem
            Author newAuthor = authorVM.CreateByDto(newAuthorDto);
            getAuthorRepo().Add(newAuthor);
            getAuthorRepo().WriteAdd(newAuthor);
            
            #endregion

            #region AddTo-listFill
            listFillAuthors.Add(newAuthor);
            AddItemsToDataGrid(listFillAuthors);
            #endregion

            Utilitys.ShowMessageBox1(Utilitys.NotifyAddSuccessfully("author"));
        }

        private void UcAuthorsTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucAuthorsTable.dgDatas.SelectedIndex == -1)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyPleaseSelect("author"));
                return;
            }

            AuthorDto authorDtoSelect = ucAuthorsTable.SelectedAuthorDto;
            Author authorSelect = authorVM.FindById(authorDtoSelect.Id);
            
            ucAuthorInformation ucAuthorInformation = MainWindow.UnitOfForm.UcAuthorInformation(true);
            ucAuthorInformation.getItem = () => authorDtoSelect;

            Window frmAuthorInformation = Utilitys.CreateDefaultForm();
            frmAuthorInformation.Content = ucAuthorInformation;
            frmAuthorInformation.Show();

            ucAuthorInformation.btnBack.Click += (_sender, _e) =>
            {
                frmAuthorInformation.Close();
            };
        }

        private void UcAuthorsTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(false);
        }

        private void UcAuthorsTable_btnRestoreClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(true);
        }

        private void ChangeStatus(bool updateStatus)
        {
            string message = string.Empty;
            if (ucAuthorsTable.dgDatas.SelectedIndex == -1)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyPleaseSelect("author"));
                return;
            }

            AuthorDto authorDtoSelect = ucAuthorsTable.SelectedAuthorDto;
            Author authorSelect = authorVM.FindById(authorDtoSelect.Id);

            if (updateStatus == false)
            {
                var listTemp = bookISBNVM.FillByIdAuthor(bookISBNVM.Repo.Gets(), authorSelect.Id, null);
                if (listTemp.Count > 0)
                {
                    Utilitys.ShowMessageBox1(Utilitys.NotifyNotValidToDelete("author"));
                    return;
                }

                message = Utilitys.NotifySureToDelete();
                if (Utilitys.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                    return;
            }

            authorSelect.Status = updateStatus;
            getAuthorRepo().WriteUpdate(authorSelect);

            ucAuthorsTable.ModifiedPagination();

            if (updateStatus == false)
                message = Utilitys.NotifyDeleteSuccessfully("author");
            else
                message = Utilitys.NotifyRestoreSuccessfully("author");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void UcAuthorsTable_btnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (ucAuthorsTable.dgDatas.SelectedIndex == -1)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyPleaseSelect("author"));
                return;
            }

            AuthorDto authorDtoSelect = ucAuthorsTable.SelectedAuthorDto;
            Author authorSelect = authorVM.FindById(authorDtoSelect.Id);

            frmAddAuthor frmAddAuthor = MainWindow.UnitOfForm.FrmAddAuthor(true);
            frmAddAuthor.getItemToUpdate = () => authorDtoSelect;
            frmAddAuthor.ShowDialog();

            if (frmAddAuthor.Context.Item == null) // Cancel the operation
            {
                return;
            }

            AuthorDto newAuthorDto = frmAddAuthor.Context.Item;

            #region UpdateItemInformation
            
            Author getAuthor = authorVM.FindById(newAuthorDto.Id);
            authorVM.Copy(getAuthor, newAuthorDto);
            getAuthorRepo().WriteUpdate(getAuthor);

            #endregion

            ucAuthorsTable.ModifiedPagination();

            Utilitys.ShowMessageBox1(Utilitys.NotifyUpdateSuccessfully("author"));
        }

        private void AddToListFill(ObservableCollection<Author> items)
        {
            listFillAuthors.Clear();
            listFillAuthors.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<Author> items)
        {
            ucAuthorsTable.getAuthors = () => items;
            ucAuthorsTable.ModifiedPagination();
        }
    }
}
