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
    /// Interaction logic for ucFunctionsTable.xaml
    /// </summary>
    public partial class ucFunctionsTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ObservableCollection<Function>> getFunctions { get; set; }
        public Func<bool?> getItem_Status { get; set; }
        public Func<List<PropertyInfo>> getExceptProperties { get; set; }
        #endregion

        #region Propdps
        
        public bool AllowPagination
        {
            get { return (bool)GetValue(AllowPaginationProperty); }
            set { SetValue(AllowPaginationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowPagination.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowPaginationProperty =
            DependencyProperty.Register("AllowPagination", typeof(bool), typeof(ucFunctionsTable), new PropertyMetadata(true));
        
        public int NumberItems
        {
            get { return (int)GetValue(NumberItemsProperty); }
            set { SetValue(NumberItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumberItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberItemsProperty =
            DependencyProperty.Register("NumberItems", typeof(int), typeof(ucFunctionsTable), new PropertyMetadata(10));
        
        #endregion

        #region Fields
        #endregion

        #region Events
        public event RoutedEventHandler btnDeleteClick;
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnRestoreClick;
        public event RoutedEventHandler btnUpdateClick;
        #endregion

        #region Properties
        private ObservableCollection<FunctionDto> _FunctionDtos;
        public ObservableCollection<FunctionDto> FunctionDtos
        {
            get
            {
                return _FunctionDtos;
            }
            set
            {
                _FunctionDtos = value;
                OnPropertyChanged();
            }
        }


        private FunctionDto _SelectedFunctionDto;
        public FunctionDto SelectedFunctionDto
        {
            get
            {
                return _SelectedFunctionDto;
            }
            set
            {
                _SelectedFunctionDto = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<FunctionDto> _SelectedFunctionDtos;
        public ObservableCollection<FunctionDto> SelectedFunctionDtos
        {
            get
            {
                return _SelectedFunctionDtos;
            }
            set
            {
                _SelectedFunctionDtos = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Mapping
        private FunctionMap functionMap;
        #endregion

        #region ViewModels
        public FunctionViewModel functionVM { get; set; }
        public RoleFunctionViewModel roleFunctionVM { get; set; }
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

        public ucFunctionsTable()
        {
            InitializeComponent();
            dgDatas.MouseDoubleClick += DgDatas_MouseDoubleClick;

            #region Allocations
            functionVM = UnitOfViewModel.Instance.FunctionViewModel;
            roleFunctionVM = UnitOfViewModel.Instance.RoleFunctionViewModel;
            functionMap = UnitOfMap.Instance.FunctionMap;
            #endregion

            this.Loaded += ucFunctionsTable_Loaded;
            this.DataContext = this;    
        }

        private void ucFunctionsTable_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnDeleteClick == null)
            {
                dtgbtnDelete.Visibility = Visibility.Collapsed;
            }
            if (btnUpdateClick == null)
            {
                dtgbtnUpdate.Visibility = Visibility.Collapsed;
            }
            if (btnRestoreClick == null)
            {
                dtgbtnRestore.Visibility = Visibility.Collapsed;
            }


            if (getExceptProperties != null)
            {
                Utilities.SetExceptPropertiesForDataGrid(dgDatas, getExceptProperties());
            }

            if (getFunctions != null)
            {
                ModifiedPagination();
            }

            if (!AllowPagination)
            {
                ucPagination.Visibility = Visibility.Collapsed;
            }
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            btnInfoClick?.Invoke(sender, e);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            btnDeleteClick?.Invoke(sender, e);
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            btnRestoreClick?.Invoke(sender, e);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateClick?.Invoke(sender, e);
        }

        private void DgDatas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnInfo_Click(dgDatas, null);
        }

        public void ModifiedPagination()
        {
            var allFunctions = getFunctions();
            var listFillStatusFunction = functionVM.FillByStatus(allFunctions, getItem_Status?.Invoke());

            FunctionDtos = functionMap.ConvertToDto(listFillStatusFunction);
            dgDatas.ItemsSource = FunctionDtos;

            if (AllowPagination == true)
            {
                ucPagination.getUcFunctionsTable = () => this;
                ucPagination.getNumberItems = () => NumberItems;
                ucPagination.getItems = () => FunctionDtos.ToObservableCollectionObj();
            }
        }
    }
}
