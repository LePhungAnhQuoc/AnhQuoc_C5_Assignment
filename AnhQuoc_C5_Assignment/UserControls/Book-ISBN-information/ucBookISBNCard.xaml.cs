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
    /// Interaction logic for ucBookISBNCard.xaml
    /// </summary>
    public partial class ucBookISBNCard : UserControl
    {
        public Func<BookISBNDto> getItem { get; set; }

        public ucBookISBNCard()
        {
            InitializeComponent();
            this.Loaded += UcBookISBNCard_Loaded;
        }

        private void UcBookISBNCard_Loaded(object sender, RoutedEventArgs e)
        {
            ucBookISBNInformation.getItem = getItem;
        }
    }
}
