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
    /// Interaction logic for frmBookISBNInformation.xaml
    /// </summary>
    public partial class frmBookISBNInformation : Window, INotifyPropertyChanged
    {
        public Func<BookISBNDto> getItem { get; set; }

        #region Properties
        private BookISBNDto _Item;
        public BookISBNDto Item
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


        public frmBookISBNInformation()
        {
            InitializeComponent();
            btnBack.Click += BtnBack_Click;

            this.Loaded += frmBookISBNInformation_Loaded;
            this.DataContext = this;

            Utilitys.SetToolTipForTextBlock(mainContent);
        }

        private void frmBookISBNInformation_Loaded(object sender, RoutedEventArgs e)
        {
            Item = getItem();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Utilitys.ExpandMore((sender as TextBlock).Text);
        }
    }
}
