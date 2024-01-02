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
    /// Interaction logic for frmDefault.xaml
    /// </summary>
    public partial class frmDefault : Window, INotifyPropertyChanged
    {
        #region dp-prop


        public string frmTitle
        {
            get { return (string)GetValue(frmTitleProperty); }
            set { SetValue(frmTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for frmTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty frmTitleProperty =
            DependencyProperty.Register("frmTitle", typeof(string), typeof(frmDefault), new PropertyMetadata(string.Empty));



        public string lblHeader
        {
            get { return (string)GetValue(lblHeaderProperty); }
            set { SetValue(lblHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for lblHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty lblHeaderProperty =
            DependencyProperty.Register("lblHeader", typeof(string), typeof(frmDefault), new PropertyMetadata(string.Empty));


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


        public frmDefault()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
