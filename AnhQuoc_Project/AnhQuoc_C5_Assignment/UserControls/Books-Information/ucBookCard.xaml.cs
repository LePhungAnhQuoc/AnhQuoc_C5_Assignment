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
    /// Interaction logic for ucBookCard.xaml
    /// </summary>
    public partial class ucBookCard : UserControl, INotifyPropertyChanged
    {
        public Func<BookDto> getItem { get; set; }

        private BookDto _Item;
        public BookDto Item
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

        #region Events
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnDeleteClick;
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


        public ucBookCard()
        {
            InitializeComponent();

            #region Events
            btnInfo.Click += btnInfo_Click;
            btnDelete.Click += btnDelete_Click;
            #endregion

            this.DataContext = this;
            this.Loaded += ucBookCard_Loaded;
        }

        private void ucBookCard_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnDeleteClick == null)
            {
                btnDelete.Visibility = Visibility.Collapsed;
            }
            if (btnInfoClick == null)
            {
                btnInfo.Visibility = Visibility.Collapsed;
            }

            Item = getItem();
        }
        
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            btnInfoClick?.Invoke(sender, e);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            btnDeleteClick?.Invoke(sender, e);
        }


    }
}
