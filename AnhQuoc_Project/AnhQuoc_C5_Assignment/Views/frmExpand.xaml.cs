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
    /// Interaction logic for frmExpand.xaml
    /// </summary>
    public partial class frmExpand : Window, INotifyPropertyChanged
    {
        #region dp-prop



        public TextBox TxtBox
        {
            get { return (TextBox)GetValue(TxtBoxProperty); }
            set { SetValue(TxtBoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TxtBox.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TxtBoxProperty =
            DependencyProperty.Register("TxtBox", typeof(TextBox), typeof(frmExpand), new PropertyMetadata(null));



        public bool IsShowInfoOnly
        {
            get { return (bool)GetValue(IsShowInfoOnlyProperty); }
            set { SetValue(IsShowInfoOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowInfoOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowInfoOnlyProperty =
            DependencyProperty.Register("IsShowInfoOnly", typeof(bool), typeof(frmExpand), new PropertyMetadata(true));




        public string frmTitle
        {
            get { return (string)GetValue(frmTitleProperty); }
            set { SetValue(frmTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for frmTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty frmTitleProperty =
            DependencyProperty.Register("frmTitle", typeof(string), typeof(frmExpand), new PropertyMetadata(string.Empty));



        public string lblHeader
        {
            get { return (string)GetValue(lblHeaderProperty); }
            set { SetValue(lblHeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for lblHeader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty lblHeaderProperty =
            DependencyProperty.Register("lblHeader", typeof(string), typeof(frmExpand), new PropertyMetadata(string.Empty));


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

        public frmExpand()
        {
            InitializeComponent();

            btnOk.Click += BtnOk_Click;
            this.DataContext = this;
            this.Loaded += FrmExpand_Loaded;
        }

        private void FrmExpand_Loaded(object sender, RoutedEventArgs e)
        {
            textArea.Style = this.IsShowInfoOnly ? 
                this.FindResource("txtStyleTextAreaDisplayInfo") as Style : this.FindResource("txtStyleTextArea") as Style;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (TxtBox != null)
            {
                TxtBox.Text = textArea.Text;
            }
            this.Close();
        }
    }
}
