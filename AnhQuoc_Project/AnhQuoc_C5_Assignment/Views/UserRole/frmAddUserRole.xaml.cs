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
    /// Interaction logic for frmAddUserRole.xaml
    /// </summary>
    public partial class frmAddUserRole : Window
    {
        #region getDatas
        public Func<UserRoleDto> getItemToUpdate { get; set; }
        public Func<UserRepository> getUserRepo { get; set; }
        public Func<UserRoleRepository> getUserRoleRepo { get; set; }
        public Func<RoleRepository> getRoleRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        public AddUserRoleViewModel Context;

        public frmAddUserRole()
        {
            InitializeComponent();

            Context = new AddUserRoleViewModel();
            this.DataContext = Context;
        }
    }
}
