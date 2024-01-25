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
    /// Interaction logic for frmTransferGuardian.xaml
    /// </summary>
    public partial class frmTransferGuardian : Window, INotifyPropertyChanged
    {
        #region geTDatas
        public Func<ChildDto> getChild { get; set; }
        public Func<ObservableCollection<AdultDto>> getGuardians { get; set; }

        public Func<ChildRepository> getChildRepo { get; set; }
        #endregion

        #region Properties
        private ReaderDto _SelectedGuardian;
        public ReaderDto SelectedGuardian
        {
            get { return _SelectedGuardian; }
            set
            {
                _SelectedGuardian = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ReaderDto> _Guardians;
        public ObservableCollection<ReaderDto> Guardians
        {
            get { return _Guardians; }
            set
            {
                _Guardians = value;
                OnPropertyChanged();
            }
        }

        private ChildDto _Item;
        public ChildDto Item
        {
            get { return _Item; }
            set
            {
                _Item = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region ViewModels
        private ReaderViewModel readerVM;
        private AdultViewModel adultVM;
        private ChildViewModel childVM;
        #endregion

        #region Mapping
        private ReaderMap readerMap;
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

        public frmTransferGuardian()
        {
            InitializeComponent();
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            readerMap = UnitOfMap.Instance.ReaderMap;
            childMap = UnitOfMap.Instance.ChildMap;


            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;
            btnCancel.Click += BtnCancel_Click;
            this.DataContext = this;
            this.Loaded += FrmTransferGuardian_Loaded;
        }

        private void FrmTransferGuardian_Loaded(object sender, RoutedEventArgs e)
        {
            Item = getChild().Clone() as ChildDto;
            ucChildInformation.getItem = () => Item;
            ucChildInformation.lblTitle.Visibility = Visibility.Collapsed;

            ObservableCollection<Reader> adultReaders = readerVM.GetListFromAdults(getGuardians());
            ObservableCollection<ReaderDto> adultReaderDtos = readerMap.ConvertToDto(adultReaders);
            Guardians = adultReaderDtos;

            Adult adult = Item.Adult;
            Reader adultRead = readerVM.FindById(adult.IdReader);

            SelectedGuardian = readerVM.FindById(Guardians, adultRead.Id);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool? statusValue = true;
            // PassValue
            Adult adult = adultVM.FindByIdReader(SelectedGuardian.Id, statusValue);
            Item.Adult = adult;

            Child childFinded = childVM.FindByIdReader(Item.IdReader, null);
            childVM.Copy(childFinded, Item);

            getChildRepo().WriteUpdate(childFinded);

            Utilities.ShowMessageBox1(Utilities.NotifyUpdateSuccessfully("child reader"));
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            Item = getChild().Clone() as ChildDto;
            ucChildInformation.getItem = () => Item;
            SelectedGuardian = readerVM.FindById(Guardians, Item.Adult.IdReader);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;
            this.Close();
        }
    }
}
