using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    /// Interaction logic for frmChildReaderInformation.xaml
    /// </summary>
    public partial class frmChildReaderInformation : Window
    {
        #region GetDatas
        public Func<Reader> getReader { get; set; }
        public Func<ChildDto> getItem { get; set; }

        public Func<ChildRepository> getChildRepo { get; set; }
        public Func<ReaderRepository> getReaderRepo { get; set; }
        #endregion

        #region Fields
        private AdultMap adultMap;
        #endregion

        #region ViewModels
        private ReaderViewModel readerVM;
        #endregion


        public frmChildReaderInformation()
        {
            InitializeComponent();

            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            adultMap = UnitOfMap.Instance.AdultMap;

            btnBack.Click += BtnBack_Click;

            this.DataContext = this;
            this.Loaded += frmChildReaderInformation_Loaded;
        }

        private void frmChildReaderInformation_Loaded(object sender, RoutedEventArgs e)
        {
            ucReaderInformation.getItem = getReader;

            ucChildInformation.getItem = getItem;
            ucChildInformation.getReaderRepo = getReaderRepo;

            Adult adult = getItem().Adult;
            Reader adultReader = readerVM.FindById(adult.IdReader);
            ucAdultReaderInformation.getItem = () => adultReader;

            AdultDto adultDto = adultMap.ConvertToDto(adult);
            ucAdultInformation.getItem = () => adultDto;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
