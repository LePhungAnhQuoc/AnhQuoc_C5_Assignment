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
    /// Interaction logic for ucAdultInformation.xaml
    /// </summary>
    public partial class ucAdultInformation : UserControl, INotifyPropertyChanged
    {
        public Func<AdultDto> getItem { get; set; }
        public Func<IsCheckProperties> getIsCheckProperties { get; set; }

        private AdultDto _Item;
        public AdultDto Item
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

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucAdultInformation()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += FrmAdultInformation_Loaded;

            Utilities.SetToolTipForTextBlock(mainContent);
        }
        
        private void FrmAdultInformation_Loaded(object sender, RoutedEventArgs e)
        {
            if (getIsCheckProperties != null)
            {

            }
            Item = getItem();
        }
    }
}
