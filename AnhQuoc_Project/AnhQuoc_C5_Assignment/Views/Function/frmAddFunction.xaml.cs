using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for frmAddFunction.xaml
    /// </summary>
    public partial class frmAddFunction : Window
    {
        #region getDatas
        public Func<FunctionRepository> getFunctionRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }

        public Func<FunctionDto> getItemToUpdate { get; set; }
        #endregion

        public AddFunctionViewModel Context;

        public frmAddFunction()
        {
            InitializeComponent();
            
            Context = new AddFunctionViewModel();
            this.DataContext = Context;

        }
    }
}
