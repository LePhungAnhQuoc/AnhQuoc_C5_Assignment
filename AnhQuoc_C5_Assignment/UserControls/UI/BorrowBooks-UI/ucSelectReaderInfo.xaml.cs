using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for ucSelectReaderInfo.xaml
    /// </summary>
    public partial class ucSelectReaderInfo : UserControl, INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucSelectReaderInfo()
        {
            InitializeComponent();

            #region SetTextBoxMaxLength
            #endregion

            var context = MainWindow.borrowBookContext;
            context.getucSelectReaderInfo = () => this;
            context.getcbAdultTxtIdentify = () => cbAdultTxtIdentify;
            context.getstkAdultInformation = () => stkAdultInformation;
            context.getstkChildInformation = () => stkChildInformation;
            context.getcbGuardianTxtIdentify = () => cbGuardianTxtIdentify;
            context.getcbSelectReaderType = () => cbSelectReaderType;
            context.gettxtAdultInputIdentify = () => txtAdultInputIdentify;
            context.getgdInputAdultIndentify = () => gdInputAdultIndentify;
            context.getucLoanDetailsBorrowedTable = () => ucLoanDetailsBorrowedTable;
            context.gettxtGuardianInputIdentify = () => txtGuardianInputIdentify;
            context.gettxtChildInputReaderName = () => txtChildInputReaderName;
            context.getcbChildTxtReaderName = () => cbChildTxtReaderName;

            context.getgdInputChildName = () => gdInputChildName;
            context.getgdGuardianInput = () => gdGuardianInput;


            this.DataContext = context;
        }
    }
}
