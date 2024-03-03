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
    /// Interaction logic for frmAddParameter.xaml
    /// </summary>
    public partial class frmAddParameter : Window
    {
        #region getDatas
        public Func<ParameterDto> getItemToUpdate { get; set; }

        public Func<string> getIdParameter { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        public AddParameterViewModel Context;

        public frmAddParameter()
        {
            InitializeComponent();

            Context = new AddParameterViewModel();

            this.Loaded += FrmAddParameter_Loaded;
            this.DataContext = Context;
        }

        private void FrmAddParameter_Loaded(object sender, RoutedEventArgs e)
        {
            Context.onLoaded(sender, e);
        }
    }
}
