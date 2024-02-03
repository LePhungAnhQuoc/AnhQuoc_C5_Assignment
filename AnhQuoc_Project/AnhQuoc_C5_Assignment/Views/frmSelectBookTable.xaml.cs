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
    /// Interaction logic for frmSelectBookTable.xaml
    /// </summary>
    public partial class frmSelectBooksTable : Window, INotifyPropertyChanged
    {
        public Func<ObservableCollection<BookDto>> getBooks { get; set; }
        #region dp-prop


        public string frmTitle
        {
            get { return (string)GetValue(frmTitleProperty); }
            set { SetValue(frmTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for frmTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty frmTitleProperty =
            DependencyProperty.Register("frmTitle", typeof(string), typeof(frmSelectBooksTable), new PropertyMetadata(string.Empty));



        public string lblHeader
        {
            get { return (string)GetValue(lblHeaderProperty); }
            set { SetValue(lblHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for lblHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty lblHeaderProperty =
            DependencyProperty.Register("lblHeader", typeof(string), typeof(frmSelectBooksTable), new PropertyMetadata(string.Empty));


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


        public frmSelectBooksTable()
        {
            InitializeComponent();
            this.DataContext = this;

            this.Loaded += FrmSelectBooksTable_Loaded;
        }

        private void FrmSelectBooksTable_Loaded(object sender, RoutedEventArgs e)
        {
            ucBooksTable.Books = getBooks();
        }
    }
}
