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
    /// Interaction logic for ucAuthorInformation.xaml
    /// </summary>
    public partial class ucAuthorInformation : UserControl, INotifyPropertyChanged
    {
        public Func<AuthorDto> getItem { get; set; }

        private AuthorDto _Item;
        public AuthorDto Item
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


        public ucAuthorInformation()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += ucAuthorInformation_Loaded;

            Utilities.SetToolTipForTextBlock(mainContent);
        }
        
        private void ucAuthorInformation_Loaded(object sender, RoutedEventArgs e)
        {
            Item = getItem();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Utilities.ExpandMore((sender as TextBlock).Text);
        }       
    }
}
