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
    /// Interaction logic for ucRolesTable.xaml
    /// </summary>
    public partial class ucRolesTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ObservableCollection<Role>> getRoles { get; set; }
        public Func<RoleFunctionRepository> getRoleFunctionRepo { get; set; }
        public Func<ObservableCollection<string>> getRoleGroups { get; set; }
        public Func<bool?> getItem_Status { get; set; }
        public Func<string[]> getExceptProperties { get; set; }
        #endregion

        #region Propdps
        
        public bool AllowPagination
        {
            get { return (bool)GetValue(AllowPaginationProperty); }
            set { SetValue(AllowPaginationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowPagination.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowPaginationProperty =
            DependencyProperty.Register("AllowPagination", typeof(bool), typeof(ucRolesTable), new PropertyMetadata(true));
        
        public int NumberItems
        {
            get { return (int)GetValue(NumberItemsProperty); }
            set { SetValue(NumberItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumberItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberItemsProperty =
            DependencyProperty.Register("NumberItems", typeof(int), typeof(ucRolesTable), new PropertyMetadata(10));



        #endregion

        #region Fields
        #endregion

        #region Events
        public event RoutedEventHandler btnDeleteClick;
        public event RoutedEventHandler btnUpdateClick;
        #endregion

        #region Properties
        private ObservableCollection<RoleDto> _RoleDtos;
        public ObservableCollection<RoleDto> RoleDtos
        {
            get
            {
                return _RoleDtos;
            }
            set
            {
                _RoleDtos = value;
                OnPropertyChanged();
            }
        }

        private RoleDto _SelectedRoleDto;
        public RoleDto SelectedRoleDto
        {
            get
            {
                return _SelectedRoleDto;
            }
            set
            {
                _SelectedRoleDto = value;
                OnPropertyChanged();
            }
        }
      
        #endregion

        #region Mapping
        private RoleMap roleMap;
        #endregion

        #region ViewModels
        public RoleViewModel roleVM { get; set; }
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

        public ucRolesTable()
        {
            InitializeComponent();

            #region Allocations
            roleVM = UnitOfViewModel.Instance.RoleViewModel;
            roleFunctionVM = UnitOfViewModel.Instance.RoleFunctionViewModel;
            roleMap = UnitOfMap.Instance.RoleMap;
            #endregion

            this.Loaded += ucRolesTable_Loaded;
            this.DataContext = this;    
        }
        
        private void ucRolesTable_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnDeleteClick == null)
            {
                dtgbtnDelete.Visibility = Visibility.Collapsed;
            }
            if (btnUpdateClick == null)
            {
                dtgbtnUpdate.Visibility = Visibility.Collapsed;
            }
          

            if (getExceptProperties != null)
            {
                Utilitys.SetExceptPropertiesForDataGrid(dgDatas, getExceptProperties());
            }

            if (getRoles != null)
            {
                ModifiedPagination();
            }

            if (!AllowPagination)
            {
                ucPagination.Visibility = Visibility.Collapsed;
            }
        }
        
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            btnDeleteClick?.Invoke(sender, e);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateClick?.Invoke(sender, e);
        }
        
        public void ModifiedPagination()
        {
            var allItems = getRoles();
            var listFillStatus = roleVM.FillByStatus(allItems, getItem_Status());

            RoleDtos = roleMap.ConvertToDto(listFillStatus);
            dgDatas.ItemsSource = RoleDtos;

            if (AllowPagination == true)
            {
                ucPagination.getUcRolesTable = () => this;
                ucPagination.getNumberItems = () => NumberItems;
                ucPagination.getItems = () => RoleDtos.ToObservableCollectionObj();
            }
        }
    }
}
