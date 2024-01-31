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
    /// Interaction logic for ucReaderManagement.xaml
    /// </summary>
    public partial class ucReaderManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ReaderRepository> getReaderRepo { get; set; }
        public Func<AdultRepository> getAdultRepo { get; set; }
        public Func<ChildRepository> getChildRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        public Func<ProvinceRepository> getProvinceRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        public bool? StatusValue { get; set; } = null;
        #endregion

        #region ViewModels
        private ReaderViewModel readerVM;
        private AdultViewModel adultVM;
        private ChildViewModel childVM;
        #endregion

        #region Mapping
        private AdultMap adultMap;
        private ChildMap childMap;
        private ReaderMap readerMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<Reader> listFillReaders;
        #endregion

        #region Properties
        private ReaderType? _SelectedReaderType;
        public ReaderType? SelectedReaderType
        {
            get { return _SelectedReaderType; }
            set
            {
                _SelectedReaderType = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ReaderType> _ReaderTypes;
        public ObservableCollection<ReaderType> ReaderTypes
        {
            get { return _ReaderTypes; }
            set
            {
                _ReaderTypes = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region prop-Dp

        #region IsAllow-Features
        public bool IsAllowViewInformation
        {
            get { return (bool)GetValue(IsAllowViewInformationProperty); }
            set { SetValue(IsAllowViewInformationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowViewInformation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowViewInformationProperty =
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucReaderManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucReaderManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucReaderManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucReaderManagement), new PropertyMetadata(true));


        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucReaderManagement), new PropertyMetadata(true));


        public bool IsAllowSearchByIdentify
        {
            get { return (bool)GetValue(IsAllowSearchByIdentifyProperty); }
            set { SetValue(IsAllowSearchByIdentifyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowSearchByIdentify.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowSearchByIdentifyProperty =
            DependencyProperty.Register("IsAllowSearchByIdentify", typeof(bool), typeof(ucReaderManagement), new PropertyMetadata(true));



        public bool IsAllowSearchByName
        {
            get { return (bool)GetValue(IsAllowSearchByNameProperty); }
            set { SetValue(IsAllowSearchByNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowSearchByName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowSearchByNameProperty =
            DependencyProperty.Register("IsAllowSearchByName", typeof(bool), typeof(ucReaderManagement), new PropertyMetadata(true));



        public bool IsAllowTransferGuardians
        {
            get { return (bool)GetValue(IsAllowTransferGuardiansProperty); }
            set { SetValue(IsAllowTransferGuardiansProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowTransferGuardians.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowTransferGuardiansProperty =
            DependencyProperty.Register("IsAllowTransferGuardians", typeof(bool), typeof(ucReaderManagement), new PropertyMetadata(true));


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

        public ucReaderManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillReaders = new ObservableCollection<Reader>();

            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;

            adultMap = UnitOfMap.Instance.AdultMap;
            childMap = UnitOfMap.Instance.ChildMap;
            readerMap = UnitOfMap.Instance.ReaderMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucReadersTable.btnInfoClick += UcReadersTable_btnInfoClick;
            ucReadersTable.btnDeleteClick += UcReadersTable_btnDeleteClick;
            ucReadersTable.btnRestoreClick += UcReadersTable_btnRestoreClick;
            ucReadersTable.btnTransferGuardian += UcReadersTable_btnTransferGuardian;
            cbReaderTypes.SelectionChanged += CbReaderTypes_SelectionChanged;
            txtSearchByName.TextChanged += TxtSearch_TextChanged;
            txtSearchByIdentify.TextChanged += TxtSearch_TextChanged;

            btnClearComboBox.Click += BtnClearComboBox_Click;
            
            Loaded += UcReaderManagement_Loaded;
            this.DataContext = this;
        }

        private void UcReaderManagement_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = Utilities.GetListFromEnum<ReaderType>().ToList();
            ReaderTypes = new ObservableCollection<ReaderType>(temp);
            
            ucReadersTable.getItem_Status = () => null;
            AddToListFillReaders(getReaderRepo().Gets());
            AddItemsToDataGrid(listFillReaders);

            btnClearComboBox.Visibility = Visibility.Hidden;

            #region IsAllow-Feature
            ucReadersTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            ucReadersTable.dtgbtnUpdate.Visibility = (IsAllowUpdate) ? Visibility.Visible : Visibility.Collapsed;
            ucReadersTable.dtgbtnDelete.Visibility = (IsAllowDelete) ? Visibility.Visible : Visibility.Collapsed;
            ucReadersTable.dtgbtnRestore.Visibility = (IsAllowRestore) ? Visibility.Visible : Visibility.Collapsed;
            ucReadersTable.dtgbtnTransferGuardian.Visibility = (IsAllowTransferGuardians) ? Visibility.Visible : Visibility.Collapsed;

            gdTxtSearchByIdentify.Visibility = (IsAllowSearchByIdentify) ? Visibility.Visible : Visibility.Collapsed;
            gdTxtSearchByName.Visibility = (IsAllowSearchByName) ? Visibility.Visible : Visibility.Collapsed;

            #endregion
        }

        #region Fillter-Methods
        private void CbReaderTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Fillter();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var source = getReaderRepo().Gets();
            var results = FillByReaderType(source);
            results = FillByTxtSearchByName(results);
            results = FillByTxtSearchByIdentify(results);

            AddToListFillReaders(results);
            AddItemsToDataGrid(listFillReaders);
        }

        private ObservableCollection<Reader> FillByReaderType(ObservableCollection<Reader> source)
        {
            ReaderType? selectedType = null;
            try
            {
                if (cbReaderTypes.SelectedIndex != -1)
                {
                    selectedType = (ReaderType)cbReaderTypes.SelectedItem;
                    btnClearComboBox.Visibility = Visibility.Visible;
                }
                else
                {
                    selectedType = null;
                }
            }
            catch
            {
            }
            if (selectedType == null)
            {
                return source;
            }
            else
            {
                ReaderViewModel viewModel = new ReaderViewModel();
                viewModel.Repo = new ReaderRepository(source);

                return viewModel.FillListByType((ReaderType)selectedType, StatusValue);
            }
        }

        private ObservableCollection<Reader> FillByTxtSearchByName(ObservableCollection<Reader> source)
        {
            string textSearch = txtSearchByName.Text.ToLower();

            ObservableCollection<Reader> results = new ObservableCollection<Reader>();
            foreach (Reader item in source)
            {
                var itemDto = readerMap.ConvertToDto(item);
                bool isCheck = itemDto.FullName.ContainsCorrectly(textSearch, true);
                if (isCheck)
                {
                    results.Add(item);
                }
            }
            return results;
        }

        private ObservableCollection<Reader> FillByTxtSearchByIdentify(ObservableCollection<Reader> source)
        {
            string textSearch = txtSearchByIdentify.Text.ToLower();

            if (Utilities.IsCheckEmptyString(textSearch))
            {
                return source;
            }

            source = readerVM.FillAdults(source, StatusValue);
           
            ObservableCollection<Reader> results = new ObservableCollection<Reader>();
            foreach (Reader item in source)
            {
                Adult adult = adultVM.FindByIdReader(item.Id, StatusValue);
                var itemDto = adultMap.ConvertToDto(adult);
                bool isCheck = itemDto.Identify.ContainsCorrectly(textSearch, true);
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
            frmAddReader frmAddReader = MainWindow.UnitOfForm.FrmAddReader(true);
            frmAddReader.getIdReader = () => readerVM.GetId();
            frmAddReader.ShowDialog();
         
            if (frmAddReader.Context.Item == null) // Cancel the operation
            {
                return;
            }

            ReaderDto newReaderDto = frmAddReader.Context.Item;

            #region AddNewReader
            Reader newReader = readerVM.CreateByDto(newReaderDto);
            getReaderRepo().Add(newReader);
            getReaderRepo().WriteAdd(newReader);
            #endregion

            #region AddAdultOrChild
            if (newReaderDto.ReaderType == ReaderType.Adult)
            {
                AdultDto newAdultDto = frmAddReader.Context.AdultItem;
                Adult newAdult = adultVM.CreateByDto(newAdultDto);
                getAdultRepo().Add(newAdult);
                getAdultRepo().WriteAdd(newAdult);

                var message = Utilities.NotifyAddSuccessfully("Adult Reader");
                MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
            }
            else if (newReaderDto.ReaderType == ReaderType.Child)
            {
                ChildDto newChildDto = frmAddReader.Context.ChildItem;
                Child newChild = childVM.CreateByDto(newChildDto);
                getChildRepo().Add(newChild);
                getChildRepo().WriteAdd(newChild);

                var message = Utilities.NotifyAddSuccessfully("Child Reader");
                MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
            }
            #endregion

            #region AddTo-listFill
            listFillReaders.Add(newReader);
            AddItemsToDataGrid(listFillReaders);
            #endregion
        }

        private void UcReadersTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            bool childs_Status = true;

            Utilities.OnClickButtonReaderInfo(ucReadersTable, childs_Status, readerVM, adultVM, childVM, adultMap, childMap);
        }

        private void UcReadersTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            bool updateStatus = false;
            if (ucReadersTable.dgReaders.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("reader"));
                return;
            }

            ReaderDto readerDtoSelect = ucReadersTable.SelectedReaderDto;
            Reader readerSelect = readerVM.FindById(readerDtoSelect.Id);

            if (readerSelect.ReaderType.ConvertValue() == ReaderType.Adult)
            {
                bool childStatus = !updateStatus;
                Adult adultFinded = adultVM.FindByIdReader(readerSelect.Id, StatusValue);
                AdultDto adultDtoFinded = adultMap.ConvertToDto(adultFinded);

                frmAdultReaderInformation frmAdultReaderInformation = MainWindow.UnitOfForm.FrmAdultReaderInformation(true);
                frmAdultReaderInformation.getItem = () => adultDtoFinded;
                frmAdultReaderInformation.getReader = () => readerSelect;
                frmAdultReaderInformation.getChilds_Status = () => childStatus;

                #region BtnDelete
                Button btnDeleteAllChildAndAdult = new Button();
                btnDeleteAllChildAndAdult.Style = Application.Current.FindResource(Constants.styleBtnDelete) as Style;
                btnDeleteAllChildAndAdult.Content = "Delete all child and adult";
                btnDeleteAllChildAndAdult.HorizontalAlignment = HorizontalAlignment.Center;
                btnDeleteAllChildAndAdult.VerticalAlignment = VerticalAlignment.Center;

                btnDeleteAllChildAndAdult.Click += (_sender, _e) =>
                {
                    var message1 = Utilities.NotifySureToDelete();
                    if (Utilities.ShowMessageBox2(message1) == MessageBoxResult.Cancel)
                        return;
                  
                    ObservableCollection<Child> listChildTemp = adultDtoFinded.ListChild;
                    ObservableCollection<Child> listFilledChild = childVM.FillByStatus(listChildTemp, childStatus);

                    for (int i = listFilledChild.Count - 1; i >= 0; i--)
                    {
                        Child child = adultDtoFinded.ListChild[i];

                        Reader childReader = readerVM.FindById(child.IdReader);
                        UpdateReadersStatus(childReader, updateStatus);
                    }
                    UpdateReadersStatus(readerSelect, updateStatus);

                    ucReadersTable.ModifiedPagination();

                    bool isAdultHasChild = listFilledChild.Count > 0;
                    var message2 = Utilities.NotifyDeleteSuccessfullyAdultReader(isAdultHasChild);
                    MessageBox.Show(message2, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);

                    frmAdultReaderInformation.Close();
                };
                #endregion

                #region BtnCancel
                Button btnCancel = new Button();
                btnCancel.Style = Application.Current.FindResource(Constants.styleBtnCancel) as Style;
                btnCancel.Content = "Cancel";
                btnCancel.Margin = new Thickness(10, 0, 0, 0);
                btnCancel.HorizontalAlignment = HorizontalAlignment.Center;
                btnCancel.VerticalAlignment = VerticalAlignment.Center;

                btnCancel.Click += (_sender, _e) =>
                {
                    frmAdultReaderInformation.Close();
                    return;
                };
                #endregion

                frmAdultReaderInformation.stkWrapButton.Children.Clear();
                frmAdultReaderInformation.stkWrapButton.Children.Add(btnDeleteAllChildAndAdult);
                frmAdultReaderInformation.stkWrapButton.Children.Add(btnCancel);
                frmAdultReaderInformation.ShowDialog();
            }
            else if (readerSelect.ReaderType.ConvertValue() == ReaderType.Child)
            {
                var message = Utilities.NotifySureToDelete();
                if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                    return;

                UpdateReadersStatus(readerSelect, updateStatus);

                ucReadersTable.ModifiedPagination();

                message = Utilities.NotifyDeleteSuccessfully("Child Reader");
                MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
            }
        }

        private void UcReadersTable_btnRestoreClick(object sender, RoutedEventArgs e)
        {
            string message;
            bool updateStatus = true;
            if (ucReadersTable.dgReaders.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("reader"));
                return;
            }
            ReaderDto readerDtoSelect = ucReadersTable.SelectedReaderDto;
            Reader readerSelect = readerVM.FindById(readerDtoSelect.Id);

            if (readerSelect.ReaderType.ConvertValue() == ReaderType.Child)
            {
                Child childFinded = childVM.FindByIdReader(readerSelect.Id, StatusValue);
                Adult adultFinded = adultVM.FindByIdReader(childFinded.IdAdult, StatusValue);
                if (adultFinded == null)
                {
                    Utilities.CatchExceptionError();
                    return;
                }
                ChildDto childDtoFinded = childMap.ConvertToDto(childFinded);
                AdultDto adultDtoFinded = adultMap.ConvertToDto(adultFinded);

                if (adultFinded.Status == updateStatus)
                {
                    Reader childReader = readerVM.FindById(childFinded.IdReader);
                    UpdateReadersStatus(childReader, updateStatus);

                    ucReadersTable.ModifiedPagination();

                    message = Utilities.NotifyRestoreSuccessfullyAdultReader(false);
                    MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
                    return;
                }

                frmChildReaderInformation frmChildReaderInformation = MainWindow.UnitOfForm.FrmChildReaderInformation(true);
                frmChildReaderInformation.getItem = () => childDtoFinded;
                frmChildReaderInformation.getReader = () => readerSelect;

                #region BtnRestore
                Button btnRestoreAdultAndChild = new Button();
                btnRestoreAdultAndChild.Style = Application.Current.FindResource(Constants.styleBtnRestore) as Style;
                btnRestoreAdultAndChild.Content = "Restore adult and child";
                btnRestoreAdultAndChild.HorizontalAlignment = HorizontalAlignment.Center;
                btnRestoreAdultAndChild.VerticalAlignment = VerticalAlignment.Center;

                btnRestoreAdultAndChild.Click += (_sender, _e) =>
                {
                    Reader adultReader = readerVM.FindById(childFinded.IdAdult);
                    UpdateReadersStatus(adultReader, updateStatus);

                    Reader childReader = readerVM.FindById(childFinded.IdReader);
                    UpdateReadersStatus(childReader, updateStatus);

                    ucReadersTable.ModifiedPagination();

                    message = Utilities.NotifyRestoreSuccessfullyAdultReader(true);
                    MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);

                    frmChildReaderInformation.Close();
                };
                #endregion

                #region BtnCancel
                Button btnCancel = new Button();

                btnCancel.Style = Application.Current.FindResource(Constants.styleBtnCancel) as Style;
                btnCancel.Content = "Cancel";
                btnCancel.Margin = new Thickness(10, 0, 0, 0);

                btnCancel.HorizontalAlignment = HorizontalAlignment.Center;
                btnCancel.VerticalAlignment = VerticalAlignment.Center;

                btnCancel.Click += (_sender, _e) =>
                {
                    return;
                };
                #endregion

                frmChildReaderInformation.stkFooter.Children.Clear();
                frmChildReaderInformation.stkFooter.Children.Add(btnRestoreAdultAndChild);
                frmChildReaderInformation.stkFooter.Children.Add(btnCancel);
                frmChildReaderInformation.ShowDialog();
            }
            else if (readerSelect.ReaderType.ConvertValue() == ReaderType.Adult)
            {
                UpdateReadersStatus(readerSelect, updateStatus);

                ucReadersTable.ModifiedPagination();

                message = Utilities.NotifyRestoreSuccessfully("Adult Reader");
                MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
            }
        }

        private void UpdateReadersStatus(Reader reader, bool updateStatus)
        {
            bool? childStatusValue = null;
            bool? adultStatusValue = null;

            Child childFinded = null;
            Adult adultFinded = null;

            childFinded = childVM.FindByIdReader(reader.Id, childStatusValue);
            if (childFinded == null)
            {
                adultFinded = adultVM.FindByIdReader(reader.Id, adultStatusValue);
                if (adultFinded == null)
                {
                    Utilities.CatchExceptionError();
                    return;
                }
            }

            if (childFinded != null)
            {
                childFinded.Status = updateStatus;
                getChildRepo().WriteUpdateStatus(childFinded, updateStatus);
            }
            else
            {
                adultFinded.Status = updateStatus;
                getAdultRepo().WriteUpdateStatus(adultFinded, updateStatus);
            }
            reader.Status = updateStatus;
            getReaderRepo().WriteUpdateStatus(reader, updateStatus);
        }

        private void UcReadersTable_btnTransferGuardian(object sender, RoutedEventArgs e)
        {
            bool? adultStatus = true;
            bool? childStatus = true;

            if (ucReadersTable.dgReaders.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("reader"));
                return;
            }

            ReaderDto readerDtoSelect = ucReadersTable.SelectedReaderDto;
            Reader readerSelect = readerVM.FindById(readerDtoSelect.Id);

            if (readerSelect.ReaderType.ConvertValue() == ReaderType.Adult)
            {
                Utilities.ShowMessageBox1("Adult reader can not use this feature!");
                return;
            }
            else if (readerSelect.ReaderType.ConvertValue() == ReaderType.Child)
            {
                frmTransferGuardian frmTransferGuardian = MainWindow.UnitOfForm.FrmTransferGuardian(true);
                Child child = childVM.FindByIdReader(readerSelect.Id, childStatus);
                ChildDto childDto = childMap.ConvertToDto(child);
                frmTransferGuardian.getChild = () => childDto;

                var adults = adultVM.FillByStatus(getAdultRepo().Gets(), adultStatus);
                frmTransferGuardian.getGuardians = () => adults;

                frmTransferGuardian.ShowDialog();

                ucReadersTable.ModifiedPagination();
            }
        }


        private void BtnClearComboBox_Click(object sender, RoutedEventArgs e)
        {
            cbReaderTypes.SelectedIndex = -1;
            btnClearComboBox.Visibility = Visibility.Hidden;
        }

        private void AddToListFillReaders(ObservableCollection<Reader> items)
        {
            listFillReaders.Clear();
            listFillReaders.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<Reader> items)
        {
            ucReadersTable.getReaders = () => items;
            ucReadersTable.ModifiedPagination();
        }
    }
}
