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
    /// Interaction logic for ucUserRolesTable.xaml
    /// </summary>
    public partial class ucUserRolesTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<UserRepository> getUserRepo { get; set; }
        public Func<ObservableCollection<UserRole>> getUserRoles { get; set; }
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
            DependencyProperty.Register("AllowPagination", typeof(bool), typeof(ucUserRolesTable), new PropertyMetadata(true));
        
        public int NumberItems
        {
            get { return (int)GetValue(NumberItemsProperty); }
            set { SetValue(NumberItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumberItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberItemsProperty =
            DependencyProperty.Register("NumberItems", typeof(int), typeof(ucUserRolesTable), new PropertyMetadata(10));



        #endregion

        #region Fields
        #endregion

        #region Events
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnDeleteClick;
        public event RoutedEventHandler btnUpdateClick;
        public event RoutedEventHandler btnSetRoleClick;
        #endregion

        #region Properties
        private ObservableCollection<UserDto> _UserDtos;
        public ObservableCollection<UserDto> UserDtos
        {
            get
            {
                return _UserDtos;
            }
            set
            {
                _UserDtos = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<UserRoleDto> _UserRoleDtos;
        public ObservableCollection<UserRoleDto> UserRoleDtos
        {
            get
            {
                return _UserRoleDtos;
            }
            set
            {
                _UserRoleDtos = value;
                OnPropertyChanged();
            }
        }

        private UserRoleDto _SelectedUserRoleDto;
        public UserRoleDto SelectedUserRoleDto
        {
            get
            {
                return _SelectedUserRoleDto;
            }
            set
            {
                _SelectedUserRoleDto = value;
                OnPropertyChanged();
            }
        }
      
        #endregion

        #region Mapping
        private UserMap userMap;
        private UserRoleMap userRoleMap;
        #endregion

        #region ViewModels
        public UserViewModel userVM { get; set; }
        public UserRoleViewModel userRoleVM { get; set; }
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

        public ucUserRolesTable()
        {
            InitializeComponent();

            #region Allocations
            userVM = UnitOfViewModel.Instance.UserViewModel;
            userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;

            userMap = UnitOfMap.Instance.UserMap;
            userRoleMap = UnitOfMap.Instance.UserRoleMap;
            #endregion

            dgDatas.MouseDoubleClick += DgDatas_MouseDoubleClick;

            this.Loaded += ucUserRolesTable_Loaded;
            this.DataContext = this;
        }
        
        private void ucUserRolesTable_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnDeleteClick == null)
            {
                dtgbtnDelete.Visibility = Visibility.Collapsed;
            }
            if (btnInfoClick == null)
            {
                dtgbtnInfo.Visibility = Visibility.Collapsed;
            }
            if (btnUpdateClick == null)
            {
                dtgbtnUpdate.Visibility = Visibility.Collapsed;
            }
            if (btnSetRoleClick == null)
            {
                dtgbtnSetRole.Visibility = Visibility.Collapsed;
            }


            if (getExceptProperties != null)
            {
                Utilities.SetExceptPropertiesForDataGrid(dgDatas, getExceptProperties());
            }

            if (getUserRoles != null)
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateClick?.Invoke(sender, e);
        }

        private void btnSetRole_Click(object sender, RoutedEventArgs e)
        {
            btnSetRoleClick?.Invoke(sender, e);
        }

        private void DgDatas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnInfo_Click(dgDatas, null);
        }

        public void ModifiedPagination()
        {
            var allUserRoles = getUserRoles();
            UserRoleDtos = userRoleMap.ConvertToDto(allUserRoles);
            dgDatas.ItemsSource = UserRoleDtos;

            if (AllowPagination == true)
            {
                ucPagination.getUcUserRolesTable = () => this;
                ucPagination.getNumberItems = () => NumberItems;
                ucPagination.getItems = () => UserRoleDtos.ToObservableCollectionObj();
            }
        }
    }
}
