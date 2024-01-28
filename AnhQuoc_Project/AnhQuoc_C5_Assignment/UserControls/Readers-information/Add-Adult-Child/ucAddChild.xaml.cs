using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for ucAddChild.xaml
    /// </summary>
    public partial class ucAddChild : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<string> getIdReader { get; set; }
        public Func<ReaderRepository> getReaderRepo { get; set; }
        public Func<AdultRepository> getAdultRepo { get; set; }
        public Func<ChildRepository> getChildRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Properties
        #endregion

        #region ResultProperties
        private Child _Item;
        public Child Item
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

        #region ViewModels
        private ReaderViewModel readerVM;
        private AdultViewModel adultVM;
        private ChildViewModel childVM;
        private ParameterViewModel parameterVM;
        #endregion

        #region Fields

        #endregion

        #region Mapping
        private ReaderMap readerMap;
        private AdultMap adultMap;
        private ChildMap childMap;
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

        public ucAddChild()
        {
            InitializeComponent();

            #region Allocate
            parameterVM = UnitOfViewModel.Instance.ParameterViewModel;
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;

            readerMap = UnitOfMap.Instance.ReaderMap;
            adultMap = UnitOfMap.Instance.AdultMap;
            childMap = UnitOfMap.Instance.ChildMap;
            #endregion

            #region SetTextBoxMaxLength
            #endregion

            ucReadersTable.btnInfoClick += UcReadersTable_btnInfoClick;

            this.Loaded += UcAddChild_Loaded;
            this.DataContext = this;
        }

        private void UcAddChild_Loaded(object sender, RoutedEventArgs e)
        {
            bool adult_Status = true;
            Item = new Child();
            Item.IdReader = getIdReader();

            #region getParameter
            Parameter itemFind = parameterVM.FindById(Constants.paraQD1);
            if (itemFind == null)
            {
                Utilities.CatchExceptionError();
                return;
            }
            int valueParameter = Convert.ToInt32(itemFind.Value);
            #endregion

            var allAdults = getAdultRepo().Gets();
            var allAdultDtos = adultMap.ConvertToDto(allAdults);
            
            var listFillValidAdult = adultVM.FillByChildsQuantityRule(allAdultDtos, valueParameter - 1, adult_Status);
            var listAdultReader = readerVM.GetListFromAdults(listFillValidAdult);


            ucReadersTable.getItem_Status = () => adult_Status;
            AddItemsToDataGrid(listAdultReader);

            ucReadersTable.getExceptProperties = () => Constants.exceptDtgReaderChild;
        }

        private void UcReadersTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            bool childs_Status = true;

            Utilities.OnClickButtonReaderInfo(ucReadersTable, childs_Status, readerVM, adultVM, childVM, adultMap, childMap);
        }

        public void PassValueToItem(Child item, Reader reader, ReaderDto selectedAdultReaderDto)
        {
            item.IdAdult = selectedAdultReaderDto.Id;
            item.Status = reader.Status;
            item.CreatedAt = reader.CreatedAt;
            item.ModifiedAt = reader.ModifiedAt;
        }

        public bool IsAllSelecting()
        {
            ReaderDto selectedAdultReaderDto = ucReadersTable.SelectedReaderDto;

            if (selectedAdultReaderDto == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("guardian adult"));
                return false;
            }
            return true;
        }

        public void AddItemsToDataGrid(ObservableCollection<Reader> source)
        {
            ucReadersTable.getReaders = () => source;
            ucReadersTable.ModifiedPagination();
        }
    }
}
