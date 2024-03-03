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
    /// Interaction logic for ucCategoryInformation.xaml
    /// </summary>
    public partial class ucCategoryInformation : UserControl, INotifyPropertyChanged
    {
        public Func<CategoryDto> getItem { get; set; }

        private CategoryDto _Item;
        public CategoryDto Item
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


        public ucCategoryInformation()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += ucCategoryInformation_Loaded;
            Utilities.SetToolTipForTextBlock(mainContent);
        }
        
        private void ucCategoryInformation_Loaded(object sender, RoutedEventArgs e)
        {
            Item = getItem();
        }
    }
}
