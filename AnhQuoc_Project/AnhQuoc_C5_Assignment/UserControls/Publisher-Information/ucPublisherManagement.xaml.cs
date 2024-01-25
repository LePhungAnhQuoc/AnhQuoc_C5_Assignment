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
    /// Interaction logic for ucPublisherManagement.xaml
    /// </summary>
    public partial class ucPublisherManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<PublisherRepository> getPublisherRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private PublisherViewModel publisherVM;
        #endregion

        #region Mapping
        private PublisherMap publisherMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<Publisher> listFillPublishers;
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
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucPublisherManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucPublisherManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucPublisherManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucPublisherManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucPublisherManagement), new PropertyMetadata(true));


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

        public ucPublisherManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillPublishers = new ObservableCollection<Publisher>();

            publisherVM = UnitOfViewModel.Instance.PublisherViewModel;

            publisherMap = UnitOfMap.Instance.PublisherMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucPublishersTable.btnInfoClick += UcPublishersTable_btnInfoClick;
            ucPublishersTable.btnUpdateClick += UcPublishersTable_btnUpdateClick;
            ucPublishersTable.btnDeleteClick += UcPublishersTable_btnDeleteClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucPublisherManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucPublisherManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucPublishersTable.getItem_Status = () => null;

            AddToListFill(getPublisherRepo().Gets());
            AddItemsToDataGrid(listFillPublishers);

            #region IsAllow-Feature
            ucPublishersTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            ucPublishersTable.IsAllowUpdate = IsAllowUpdate;
            ucPublishersTable.IsAllowDelete = IsAllowDelete;
            ucPublishersTable.IsAllowRestore = IsAllowRestore;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getPublisherRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<Publisher> FillByTextSearch(ObservableCollection<Publisher> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<Publisher> results = new ObservableCollection<Publisher>();
            foreach (Publisher item in source)
            {
                var itemDto = publisherMap.ConvertToDto(item);
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
            frmAddPublisher frmAddPublisher = MainWindow.UnitOfForm.FrmAddPublisher(true);
            frmAddPublisher.getIdPublisher = () => publisherVM.GetId();
            frmAddPublisher.ShowDialog();
         
            if (frmAddPublisher.Item == null) // Cancel the operation
            {
                return;
            }

            PublisherDto newPublisherDto = frmAddPublisher.Item;

            #region AddNewItem
            Publisher newPublisher = publisherVM.CreateByDto(newPublisherDto);
            getPublisherRepo().Add(newPublisher);
            getPublisherRepo().WriteAdd(newPublisher);
            
            #endregion

            #region AddTo-listFill
            listFillPublishers.Add(newPublisher);
            AddItemsToDataGrid(listFillPublishers);
            #endregion
        }

        private void UcPublishersTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucPublishersTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("publisher"));
                return;
            }

            PublisherDto publisherDtoSelect = ucPublishersTable.SelectedPublisherDto;
            Publisher publisherSelect = publisherVM.FindById(publisherDtoSelect.Id);
            
            ucPublisherInformation ucPublisherInformation = MainWindow.UnitOfForm.UcPublisherInformation(true);
            ucPublisherInformation.getItem = () => publisherDtoSelect;

            Window frmPublisherInformation = Utilities.CreateDefaultForm();
            frmPublisherInformation.Content = ucPublisherInformation;
            frmPublisherInformation.Show();

            ucPublisherInformation.btnBack.Click += (_sender, _e) =>
            {
                frmPublisherInformation.Close();
            };
        }

        private void UcPublishersTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            if (ucPublishersTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("publisher"));
                return;
            }

            PublisherDto publisherDtoSelect = ucPublishersTable.SelectedPublisherDto;
            Publisher publisherSelect = publisherVM.FindById(publisherDtoSelect.Id);

           
            message = Utilities.NotifySureToDelete();
            if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                return;


            getPublisherRepo().Remove(publisherSelect);
            getPublisherRepo().WriteDelete(publisherSelect);

            ucPublishersTable.ModifiedPagination();

            message = Utilities.NotifyDeleteSuccessfully("publisher");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }
        

        private void UcPublishersTable_btnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (ucPublishersTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("publisher"));
                return;
            }

            PublisherDto publisherDtoSelect = ucPublishersTable.SelectedPublisherDto;
            Publisher publisherSelect = publisherVM.FindById(publisherDtoSelect.Id);

            frmAddPublisher frmAddPublisher = MainWindow.UnitOfForm.FrmAddPublisher(true);
            frmAddPublisher.getItemToUpdate = () => publisherDtoSelect;
            frmAddPublisher.ShowDialog();

            if (frmAddPublisher.Item == null) // Cancel the operation
            {
                return;
            }

            PublisherDto newPublisherDto = frmAddPublisher.Item;

            #region UpdateItemInformation
            
            Publisher getPublisher = publisherVM.FindById(newPublisherDto.Id);
            publisherVM.Copy(getPublisher, newPublisherDto);
            getPublisherRepo().WriteUpdate(getPublisher);

            #endregion

            ucPublishersTable.ModifiedPagination();
        }

        private void AddToListFill(ObservableCollection<Publisher> items)
        {
            listFillPublishers.Clear();
            listFillPublishers.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<Publisher> items)
        {
            ucPublishersTable.getPublishers = () => items;
            ucPublishersTable.ModifiedPagination();
        }
    }
}
