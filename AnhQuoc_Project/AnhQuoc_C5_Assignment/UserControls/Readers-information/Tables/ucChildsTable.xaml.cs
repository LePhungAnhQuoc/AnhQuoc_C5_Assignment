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
    /// Interaction logic for ucChildsTable.xaml
    /// </summary>
    public partial class ucChildsTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<bool?> getItem_Status { get; set; }
        public Func<ReaderRepository> getReaderRepo { get; set; }
        public Func<AdultRepository> getAdultRepo { get; set; }
        public Func<ObservableCollection<Child>> getChilds { get; set; }
        #endregion

        #region Fields
        private ObservableCollection<Child> listFilledChild;
        #endregion

        #region Events
        public event RoutedEventHandler btnInfoClick;
        public event RoutedEventHandler btnDeleteClick;
        #endregion

        #region Properties
        private ObservableCollection<ChildDto> _ChildDtos;
        public ObservableCollection<ChildDto> ChildDtos
        {
            get
            {
                return _ChildDtos;
            }
            set
            {
                _ChildDtos = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ViewModels
        private ChildViewModel childVM;
        #endregion

        #region Mapping
        private ChildMap childMap;
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


        public ucChildsTable()
        {
            InitializeComponent();

            childVM = UnitOfViewModel.Instance.ChildViewModel;
            childMap = UnitOfMap.Instance.ChildMap;
            dgDatas.MouseDoubleClick += DgDatas_MouseDoubleClick;


            this.Loaded += ucChildsTable_Loaded;
            this.DataContext = this;
        }

        private void ucChildsTable_Loaded(object sender, RoutedEventArgs e)
        {
            if (getChilds != null)
            {
                var listChild = getChilds();
                listFilledChild = childVM.FillByStatus(listChild, getItem_Status());

                ChildDtos = childMap.ConvertToDto(listFilledChild);
            }
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            btnInfoClick?.Invoke(sender, e);
        }

        private void DgDatas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnInfo_Click(null, null);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            btnDeleteClick?.Invoke(sender, e);
        }
    }
}
