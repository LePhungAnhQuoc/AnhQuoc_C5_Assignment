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
    /// Interaction logic for frmAddPenaltyReason.xaml
    /// </summary>
    public partial class frmAddPenaltyReason : Window
    {
        #region getDatas
        public Func<PenaltyReasonDto> getItemToUpdate { get; set; }

        public Func<string> getIdPenaltyReason { get; set; }
        public Func<PenaltyReasonRepository> getPenaltyReasonRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        public AddPenaltyReasonViewModel Context;
     
        public frmAddPenaltyReason()
        {
            InitializeComponent();

            Context = new AddPenaltyReasonViewModel();

            this.Loaded += FrmAddPenaltyReason_Loaded;
            this.DataContext = Context;
        }

        private void FrmAddPenaltyReason_Loaded(object sender, RoutedEventArgs e)
        {
            Context.onLoaded(sender, e);
        }
    }
}
