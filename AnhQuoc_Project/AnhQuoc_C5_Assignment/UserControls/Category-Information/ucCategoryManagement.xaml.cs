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
    /// Interaction logic for ucCategoryManagement.xaml
    /// </summary>
    public partial class ucCategoryManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<CategoryRepository> getCategoryRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private CategoryViewModel categoryVM;
        private BookTitleViewModel bookTitleVM;
        #endregion

        #region Mapping
        private CategoryMap categoryMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<Category> listFills;
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
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucCategoryManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucCategoryManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucCategoryManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucCategoryManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucCategoryManagement), new PropertyMetadata(true));


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

        public ucCategoryManagement()
        {
            InitializeComponent();

            #region Allocations
            listFills = new ObservableCollection<Category>();

            categoryVM = UnitOfViewModel.Instance.CategoryViewModel;
            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;

            categoryMap = UnitOfMap.Instance.CategoryMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucCategorysTable.btnInfoClick += UcCategorysTable_btnInfoClick;
            ucCategorysTable.btnUpdateClick += UcCategorysTable_btnUpdateClick;
            ucCategorysTable.btnDeleteClick += UcCategorysTable_btnDeleteClick;
            ucCategorysTable.btnRestoreClick += UcCategorysTable_btnRestoreClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucCategoryManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucCategoryManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucCategorysTable.getItem_Status = () => null;

            AddToListFill(getCategoryRepo().Gets());
            AddItemsToDataGrid(listFills);

            #region IsAllow-Feature
            ucCategorysTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            ucCategorysTable.IsAllowUpdate = IsAllowUpdate;
            ucCategorysTable.IsAllowDelete = IsAllowDelete;
            ucCategorysTable.IsAllowRestore = IsAllowRestore;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getCategoryRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<Category> FillByTextSearch(ObservableCollection<Category> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<Category> results = new ObservableCollection<Category>();
            foreach (Category item in source)
            {
                var itemDto = categoryMap.ConvertToDto(item);
                bool isCheck = itemDto.Name.ContainsCorrectly(textSearch, true);
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
            frmAddCategory frmAddCategory = MainWindow.UnitOfForm.FrmAddCategory(true);
            frmAddCategory.getIdCategory = () => categoryVM.GetId();
            frmAddCategory.ShowDialog();
         
            if (frmAddCategory.Context.Item == null) // Cancel the operation
            {
                return;
            }

            CategoryDto newCategoryDto = frmAddCategory.Context.Item;

            #region AddNewItem
            Category newCategory = categoryVM.CreateByDto(newCategoryDto);
            getCategoryRepo().Add(newCategory);
            getCategoryRepo().WriteAdd(newCategory);
            #endregion

            #region AddTo-listFill
            listFills.Add(newCategory);
            AddItemsToDataGrid(listFills);
            #endregion

            var message = Utilities.NotifyAddSuccessfully("category");
            Utilities.ShowMessageBox1(message);

        }

        private void UcCategorysTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucCategorysTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("category"));
                return;
            }

            CategoryDto categoryDtoSelect = ucCategorysTable.SelectedCategoryDto;
            Category categorySelect = categoryVM.FindById(categoryDtoSelect.Id);
            
            ucCategoryInformation ucCategoryInformation = MainWindow.UnitOfForm.UcCategoryInformation(true);
            ucCategoryInformation.getItem = () => categoryDtoSelect;

            Window frmCategoryInformation = Utilities.CreateDefaultForm();
            frmCategoryInformation.Content = ucCategoryInformation;
            frmCategoryInformation.Show();

            ucCategoryInformation.btnBack.Click += (_sender, _e) =>
            {
                frmCategoryInformation.Close();
            };
        }

        private void UcCategorysTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(false);
        }

        private void UcCategorysTable_btnRestoreClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(true);
        }

        private void ChangeStatus(bool updateStatus)
        {
            string message = string.Empty;
            if (ucCategorysTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("category"));
                return;
            }

            CategoryDto categoryDtoSelect = ucCategorysTable.SelectedCategoryDto;
            Category categorySelect = categoryVM.FindById(categoryDtoSelect.Id);

            if (updateStatus == false)
            {
                var listTemp = bookTitleVM.FillByIdCategory(bookTitleVM.Repo.Gets(), categorySelect.Id, null);
                if (listTemp.Count > 0)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyNotValidToDelete("category"));
                    return;
                }

                message = Utilities.NotifySureToDelete();
                if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                    return;
            }

            categorySelect.Status = updateStatus;
            getCategoryRepo().WriteUpdate(categorySelect);

            ucCategorysTable.ModifiedPagination();

            if (updateStatus == false)
                message = Utilities.NotifyDeleteSuccessfully("category");
            else
                message = Utilities.NotifyRestoreSuccessfully("category");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void UcCategorysTable_btnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (ucCategorysTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("category"));
                return;
            }

            CategoryDto categoryDtoSelect = ucCategorysTable.SelectedCategoryDto;
            Category categorySelect = categoryVM.FindById(categoryDtoSelect.Id);

            frmAddCategory frmAddCategory = MainWindow.UnitOfForm.FrmAddCategory(true);
            frmAddCategory.getItemToUpdate = () => categoryDtoSelect;
            frmAddCategory.ShowDialog();

            if (frmAddCategory.Context.Item == null) // Cancel the operation
            {
                return;
            }

            CategoryDto newCategoryDto = frmAddCategory.Context.Item;

            #region UpdateItemInformation
            Category getCategory = categoryVM.FindById(newCategoryDto.Id);
            categoryVM.Copy(getCategory, newCategoryDto);
            getCategoryRepo().WriteUpdate(getCategory);

            #endregion

            ucCategorysTable.ModifiedPagination();

            var message = Utilities.NotifyUpdateSuccessfully("category");
            Utilities.ShowMessageBox1(message);
        }

        private void AddToListFill(ObservableCollection<Category> items)
        {
            listFills.Clear();
            listFills.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<Category> items)
        {
            ucCategorysTable.getCategorys = () => items;
            ucCategorysTable.ModifiedPagination();
        }
    }
}
