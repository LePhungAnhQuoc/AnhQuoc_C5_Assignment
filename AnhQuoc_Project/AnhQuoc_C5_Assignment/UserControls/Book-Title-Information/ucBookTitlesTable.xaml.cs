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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucBookTitlesTable.xaml
    /// </summary>
    public partial class ucBookTitlesTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ObservableCollection<BookTitleDto>> getBookTitles { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<BookTitleDto> _BookTitles;
        public ObservableCollection<BookTitleDto> BookTitles
        {
            get
            {
                return _BookTitles;
            }
            set
            {
                _BookTitles = value;
                OnPropertyChanged();
            }
        }

        private BookTitleDto _SelectedBookTitleDto;
        public BookTitleDto SelectedBookTitleDto
        {
            get { return _SelectedBookTitleDto; }
            set 
            {
                _SelectedBookTitleDto = value;
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


        #region Events
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnDeleteClick;
        #endregion

        public ucBookTitlesTable()
        {
            InitializeComponent();
            Loaded += UcBookTitlesTable_Loaded;

            dgDatas.MouseDoubleClick += DgDatas_MouseDoubleClick;
            this.DataContext = this;
        }


        private void UcBookTitlesTable_Loaded(object sender, RoutedEventArgs e)
        {
            if (btnInfoClick == null)
            {
                dtgbtnInfo.Visibility = Visibility.Collapsed;
            }

            if (getBookTitles != null)
                BookTitles = getBookTitles();
        }

        private void DgDatas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnInfo_Click(null, null);
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            btnInfoClick?.Invoke(sender, e);
        }

    }
}
