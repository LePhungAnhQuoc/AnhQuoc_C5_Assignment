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
    /// Interaction logic for ucUsersTable.xaml
    /// </summary>
    public partial class ucUsersTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ObservableCollection<User>> getUsers { get; set; }
        public Func<UserRoleRepository> getUserRoleRepo { get; set; }
        public Func<bool?> getItem_Status { get; set; }
        public Func<List<PropertyInfo>> getExceptProperties { get; set; }
        #endregion

        #region Propdps

        #region Paginations
        public bool AllowPagination
        {
            get { return (bool)GetValue(AllowPaginationProperty); }
            set { SetValue(AllowPaginationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowPagination.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowPaginationProperty =
            DependencyProperty.Register("AllowPagination", typeof(bool), typeof(ucUsersTable), new PropertyMetadata(true));
        
        public int NumberItems
        {
            get { return (int)GetValue(NumberItemsProperty); }
            set { SetValue(NumberItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumberItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberItemsProperty =
            DependencyProperty.Register("NumberItems", typeof(int), typeof(ucUsersTable), new PropertyMetadata(10));
        #endregion

        #region IsAllowFeatures
        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucUsersTable), new PropertyMetadata(true));
        
        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucUsersTable), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucUsersTable), new PropertyMetadata(true));


        #endregion


        #endregion

        #region Fields
        #endregion

        #region Events
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnDeleteClick;
        public event RoutedEventHandler btnUpdateClick;
        public event RoutedEventHandler btnRestoreClick;
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

        private UserDto _SelectedUserDto;
        public UserDto SelectedUserDto
        {
            get
            {
                return _SelectedUserDto;
            }
            set
            {
                _SelectedUserDto = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Mapping
        private UserMap userMap;
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

        public ucUsersTable()
        {
            InitializeComponent();

            #region Allocations
            userVM = UnitOfViewModel.Instance.UserViewModel;
            userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;

            userMap = UnitOfMap.Instance.UserMap;
            #endregion

            dgDatas.MouseDoubleClick += DgDatas_MouseDoubleClick;

            this.Loaded += ucUsersTable_Loaded;
            this.DataContext = this;    
        }
        
        private void ucUsersTable_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnDeleteClick == null)
            {
                dtgbtnDelete.Visibility = Visibility.Collapsed;
            }
            if (btnUpdateClick == null)
            {
                dtgbtnUpdate.Visibility = Visibility.Collapsed;
            }
            if (btnInfoClick == null)
            {
                dtgbtnInfo.Visibility = Visibility.Collapsed;
            }
         

            if (getExceptProperties != null)
            {
                Utilities.SetExceptPropertiesForDataGrid(dgDatas, getExceptProperties());
            }

            if (getUsers != null)
            {
                RefreshDataGrid();
            }

            if (!AllowPagination)
            {
                ucPagination.Visibility = Visibility.Collapsed;
            }

            #region IsAllow-Feature
            dtgbtnUpdate.Visibility = (IsAllowUpdate) ? Visibility.Visible : Visibility.Collapsed;
            dtgbtnDelete.Visibility = (IsAllowDelete) ? Visibility.Visible : Visibility.Collapsed;
            dtgbtnRestore.Visibility = (IsAllowRestore) ? Visibility.Visible : Visibility.Collapsed;
            #endregion

            #region Disable-admin-user
            #endregion
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

        public void RefreshDataGrid()
        {
            var allUsers = getUsers();
            var listFillStatusUser = userVM.FillByStatus(allUsers, getItem_Status());

            UserDtos = userMap.ConvertToDto(listFillStatusUser);
            dgDatas.ItemsSource = UserDtos;

            if (AllowPagination == true)
            {
                ucPagination.getUcUsersTable = () => this;
                ucPagination.getNumberItems = () => NumberItems;
                ucPagination.getItems = () => UserDtos.ToObservableCollectionObj();
            }
        }
    }
}
