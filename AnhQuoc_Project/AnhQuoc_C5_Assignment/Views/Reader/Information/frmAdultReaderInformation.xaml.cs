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
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for frmAdultInformation.xaml
    /// </summary>
    public partial class frmAdultReaderInformation : Window, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<AdultDto> getItem { get; set; }
        public Func<Reader> getReader { get; set; }
        public Func<bool?> getChilds_Status { get; set; }

        // Repositories
        public Func<AdultRepository> getAdultRepo { get; set; }
        public Func<ReaderRepository> getReaderRepo { get; set; }
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

        public frmAdultReaderInformation()
        {
            InitializeComponent();
            btnBack.Click += BtnBack_Click;

            this.DataContext = this;
            this.Loaded += FrmAdultReaderInformation_Loaded;
        }

        private void FrmAdultReaderInformation_Loaded(object sender, RoutedEventArgs e)
        {
            ucReaderInformation.getItem = getReader;
            ucAdultInformation.getItem = getItem;

            ucChildsTable.getChilds = () => (getItem().ListChild);
            ucChildsTable.getItem_Status = getChilds_Status;
            ucChildsTable.getAdultRepo = getAdultRepo;
            ucChildsTable.getReaderRepo = getReaderRepo;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
