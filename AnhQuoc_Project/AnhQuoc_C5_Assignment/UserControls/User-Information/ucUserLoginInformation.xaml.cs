using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ucUserLoginInformation.xaml
    /// </summary>
    public partial class ucUserLoginInformation : UserControl, INotifyPropertyChanged
    {
        public Func<UserDto> getItem { get; set; }

        #region ViewModels
        private UserRoleViewModel userRoleVM;
        private RoleViewModel roleVM;
        #endregion

        private UserDto _Item;
        public UserDto Item
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

        private Role _Role;
        public Role Role
        {
            get { return _Role; }
            set 
            {
                _Role = value;
                OnPropertyChanged();
            }
        }


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion


        public ucUserLoginInformation()
        {
            InitializeComponent();
            this.DataContext = this;

            userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;
            roleVM = UnitOfViewModel.Instance.RoleViewModel;

            Loaded += ucUserLoginInformation_Loaded;
            Utilitys.SetToolTipForTextBlock(mainContent);
        }

        private void ucUserLoginInformation_Loaded(object sender, RoutedEventArgs e)
        {
            Item = getItem();

            UserRole getUserRole = userRoleVM.FindByIdUser(Item.Id);
            Role = roleVM.FindById(getUserRole.IdRole);
        }
    }
}
