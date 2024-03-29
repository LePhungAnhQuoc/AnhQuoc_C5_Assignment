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
    /// Interaction logic for ucBookStatusManagement.xaml
    /// </summary>
    public partial class ucBookStatusManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<BookStatusRepository> getBookStatusRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private BookStatusViewModel bookStatusVM;
        private BookViewModel bookVM;
        #endregion

        #region Mapping
        private BookStatusMap bookStatusMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<BookStatu> listFillBookStatuss;
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
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucBookStatusManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucBookStatusManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucBookStatusManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucBookStatusManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucBookStatusManagement), new PropertyMetadata(true));


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

        public ucBookStatusManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillBookStatuss = new ObservableCollection<BookStatu>();

            bookStatusVM = UnitOfViewModel.Instance.BookStatusViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;

            bookStatusMap = UnitOfMap.Instance.BookStatusMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucBookStatussTable.btnInfoClick += UcBookStatussTable_btnInfoClick;
            ucBookStatussTable.btnUpdateClick += UcBookStatussTable_btnUpdateClick;

            //ucBookStatussTable.btnDeleteClick += UcBookStatussTable_btnDeleteClick;
            //ucBookStatussTable.btnRestoreClick += UcBookStatussTable_btnRestoreClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucBookStatusManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucBookStatusManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucBookStatussTable.getItem_Status = () => null;

            AddToListFill(getBookStatusRepo().Gets());
            AddItemsToDataGrid(listFillBookStatuss);

            #region IsAllow-Feature
            ucBookStatussTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            ucBookStatussTable.IsAllowUpdate = IsAllowUpdate;
            ucBookStatussTable.IsAllowDelete = IsAllowDelete;
            ucBookStatussTable.IsAllowRestore = IsAllowRestore;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getBookStatusRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<BookStatu> FillByTextSearch(ObservableCollection<BookStatu> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<BookStatu> results = new ObservableCollection<BookStatu>();
            foreach (BookStatu item in source)
            {
                bool isCheck = item.Name.ContainsCorrectly(textSearch, true);
                if (isCheck)
                {
                    results.Add(item);
                }
            }
            return results;
        }
        #endregion

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddBookStatus frmAddBookStatus = MainWindow.UnitOfForm.FrmAddBookStatus(true);
            frmAddBookStatus.getIdBookStatus = () => bookStatusVM.GetId();
            frmAddBookStatus.ShowDialog();
         
            if (frmAddBookStatus.Context.Item == null) // Cancel the operation
            {
                return;
            }

            BookStatusDto newBookStatusDto = frmAddBookStatus.Context.Item;

            #region AddNewItem
            BookStatu newBookStatus = bookStatusVM.CreateByDto(newBookStatusDto);
            getBookStatusRepo().Add(newBookStatus);
            getBookStatusRepo().WriteAdd(newBookStatus);
            #endregion

            #region AddTo-listFill
            listFillBookStatuss.Add(newBookStatus);
            AddItemsToDataGrid(listFillBookStatuss);
            #endregion

            Utilities.ShowMessageBox1(Utilities.NotifyAddSuccessfully("book status"));
        }

        private void UcBookStatussTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucBookStatussTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("book status"));
                return;
            }

            BookStatusDto bookStatusDtoSelect = ucBookStatussTable.SelectedBookStatusDto;
            BookStatu bookStatusSelect = bookStatusVM.FindById(bookStatusDtoSelect.Id);
            
            ucBookStatusInformation ucBookStatusInformation = MainWindow.UnitOfForm.UcBookStatusInformation(true);
            ucBookStatusInformation.getItem = () => bookStatusDtoSelect;

            Window frmBookStatusInformation = Utilities.CreateDefaultForm();
            frmBookStatusInformation.Content = ucBookStatusInformation;
            frmBookStatusInformation.Show();

            ucBookStatusInformation.btnBack.Click += (_sender, _e) =>
            {
                frmBookStatusInformation.Close();
            };
        }

        private void UcBookStatussTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(false);
        }

        private void UcBookStatussTable_btnRestoreClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(true);
        }

        private void ChangeStatus(bool updateStatus)
        {
            string message = string.Empty;
            if (ucBookStatussTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("book status"));
                return;
            }

            BookStatusDto bookStatusDtoSelect = ucBookStatussTable.SelectedBookStatusDto;
            BookStatu bookStatusSelect = bookStatusVM.FindById(bookStatusDtoSelect.Id);

            if (updateStatus == false)
            {
                var tempList = bookVM.FillByIdBookStatus(bookStatusSelect.Id);
                if (tempList.Count > 0)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyNotValidToDelete("book status"));
                    return;
                }
                message = Utilities.NotifySureToDelete();
                if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                    return;
            }

            bookStatusSelect.Status = updateStatus;
            getBookStatusRepo().WriteUpdateStatus(bookStatusSelect, updateStatus);

            ucBookStatussTable.ModifiedPagination();

            if (updateStatus == false)
                message = Utilities.NotifyDeleteSuccessfully("book status");
            else
                message = Utilities.NotifyRestoreSuccessfully("book status");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void UcBookStatussTable_btnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (ucBookStatussTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("book status"));
                return;
            }

            BookStatusDto bookStatusDtoSelect = ucBookStatussTable.SelectedBookStatusDto;
            BookStatu bookStatusSelect = bookStatusVM.FindById(bookStatusDtoSelect.Id);

            frmAddBookStatus frmAddBookStatus = MainWindow.UnitOfForm.FrmAddBookStatus(true);
            frmAddBookStatus.getItemToUpdate = () => bookStatusDtoSelect;
            frmAddBookStatus.ShowDialog();

            if (frmAddBookStatus.Context.Item == null) // Cancel the operation
            {
                return;
            }

            BookStatusDto newBookStatusDto = frmAddBookStatus.Context.Item;

            #region UpdateItemInformation
            BookStatu getBookStatus = bookStatusVM.FindById(newBookStatusDto.Id);
            bookStatusVM.Copy(getBookStatus, newBookStatusDto);
            getBookStatusRepo().WriteUpdate(getBookStatus);

            #endregion

            ucBookStatussTable.ModifiedPagination();
            Utilities.ShowMessageBox1(Utilities.NotifyUpdateSuccessfully("book status"));

        }

        private void AddToListFill(ObservableCollection<BookStatu> items)
        {
            listFillBookStatuss.Clear();
            listFillBookStatuss.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<BookStatu> items)
        {
            ucBookStatussTable.getBookStatuss = () => items;
            ucBookStatussTable.ModifiedPagination();
        }
    }
}
