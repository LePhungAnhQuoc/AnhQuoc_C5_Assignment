using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucButtonCircle.xaml
    /// </summary>
    public partial class ucButtonCircle : UserControl
    {
        #region dp-prop


        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register("ButtonContent", typeof(string), typeof(ucButtonCircle), new PropertyMetadata(string.Empty));


        #endregion
        public ucButtonCircle()
        {
            InitializeComponent();
            this.Loaded += UcButtonCircle_Loaded;
        }

        private void UcButtonCircle_Loaded(object sender, RoutedEventArgs e)
        {
            btnCircle.Content = ButtonContent;
        }
    }
}
