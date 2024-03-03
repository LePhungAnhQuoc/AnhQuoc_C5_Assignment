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
    /// Interaction logic for ucChildInformation.xaml
    /// </summary>
    public partial class ucChildInformation : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ChildDto> getItem { get; set; }
        public Func<ReaderRepository> getReaderRepo { get; set; }
        #endregion

        #region Fields
        private ReaderViewModel readerVM;
        #endregion

        #region Properties
        private ChildDto _Item;
        public ChildDto Item
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

        private ReaderDto _AdultReaderDto;
        public ReaderDto AdultReaderDto
        {
            get
            {
                return _AdultReaderDto;
            }
            set
            {
                _AdultReaderDto = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Mapping
        private ReaderMap readerMap;
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


        public ucChildInformation()
        {
            InitializeComponent();

            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            readerMap = UnitOfMap.Instance.ReaderMap;

            this.DataContext = this;
            Loaded += UcChildInformation_Loaded;

            Utilities.SetToolTipForTextBlock(mainContent);
        }

        private void UcChildInformation_Loaded(object sender, RoutedEventArgs e)
        {
            Item = getItem();

            Reader adultReader = readerVM.FindById(Item.Adult.IdReader);
            AdultReaderDto = readerMap.ConvertToDto(adultReader);
        }
    }
}
