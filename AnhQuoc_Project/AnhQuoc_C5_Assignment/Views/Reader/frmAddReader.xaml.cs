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
    /// Interaction logic for frmAddReader.xaml
    /// </summary>
    public partial class frmAddReader : Window
    {
        #region getDatas
        public Func<UserDto> getItemToUpdate { get; set; }
        public Func<AdultDto> getAdultToUpdate { get; set; }
        public Func<ChildDto> getChildToUpdate { get; set; }

        public Func<string> getIdReader { get; set; }

        public Func<ReaderRepository> getReaderRepo { get; set; }
        public Func<AdultRepository> getAdultRepo { get; set; }
        public Func<ChildRepository> getChildRepo { get; set; }
        public Func<ProvinceRepository> getProvinceRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        public AddReaderViewModel Context;

        public frmAddReader()
        {
            InitializeComponent();
            
        
            Context = new AddReaderViewModel();

            this.Loaded += FrmAddReader_Loaded;
            this.DataContext = Context;
        }
        
        private void FrmAddReader_Loaded(object sender, RoutedEventArgs e)
        {
            Context.onLoaded(sender, e);
        }
    }
}
