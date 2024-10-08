﻿using Microsoft.Win32;
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
    /// Interaction logic for frmAddRole.xaml
    /// </summary>
    public partial class frmAddRole : Window
    {
        #region getDatas
        public Func<RoleDto> getItemToUpdate { get; set; }

        public Func<string> getIdRole { get; set; }
        public Func<RoleRepository> getRoleRepo { get; set; }
        public Func<ObservableCollection<string>> getRoleGroups { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        public AddRoleViewModel Context;

        public frmAddRole()
        {
            InitializeComponent();

            Context = new AddRoleViewModel();

            this.Loaded += FrmAddRole_Loaded;
            this.DataContext = Context;
        }

        private void FrmAddRole_Loaded(object sender, RoutedEventArgs e)
        {
            Context.onLoaded(sender, e);
        }
    }
}
