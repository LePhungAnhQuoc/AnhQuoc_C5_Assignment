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
    /// Interaction logic for frmTransferGuardian.xaml
    /// </summary>
    public partial class frmTransferGuardian : Window
    {
        #region getDatas
        public Func<ChildDto> getChild { get; set; }
        public Func<ObservableCollection<Adult>> getGuardians { get; set; }

        public Func<ChildRepository> getChildRepo { get; set; }
        #endregion

        public TransferGuardianViewModel Context;
        
        public frmTransferGuardian()
        {
            InitializeComponent();

            Context = new TransferGuardianViewModel();

            this.Loaded += FrmTransferGuardian_Loaded;
            this.DataContext = Context;
        }

        private void FrmTransferGuardian_Loaded(object sender, RoutedEventArgs e)
        {
            Context.onLoaded(sender, e);
        }
    }
}
