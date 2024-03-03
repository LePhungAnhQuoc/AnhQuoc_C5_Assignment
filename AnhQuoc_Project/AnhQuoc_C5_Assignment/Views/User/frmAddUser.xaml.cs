using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for frmAddUser.xaml
    /// </summary>
    public partial class frmAddUser : Window
    {
        #region getDatas
        public Func<UserDto> getItemToUpdate { get; set; }

        public Func<string> getIdUser { get; set; }
        public Func<UserRepository> getUserRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        public AddUserViewModel<User, UserDto> Context;
        
        public frmAddUser()
        {
            InitializeComponent();
            Context = new AddUserViewModel<User, UserDto>();

            this.Loaded += FrmAddUser_Loaded;
            this.DataContext = Context;
        }

        private void FrmAddUser_Loaded(object sender, RoutedEventArgs e)
        {
            Context.onLoaded(sender, e);
        }
    }
}
