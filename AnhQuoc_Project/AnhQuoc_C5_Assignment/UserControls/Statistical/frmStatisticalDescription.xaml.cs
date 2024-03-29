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
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for frmStatisticalDescription.xaml
    /// </summary>
    public partial class frmStatisticalDescription : Window
    {


        public double TextFontSize
        {
            get { return (double)GetValue(TextFontSizeProperty); }
            set { SetValue(TextFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextFontSizeProperty =
            DependencyProperty.Register("TextFontSize", typeof(double), typeof(frmStatisticalDescription), new PropertyMetadata(30.0));



        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Foreground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(frmStatisticalDescription), new PropertyMetadata(Brushes.Black));



        public PathGeometry Icon
        {
            get { return (PathGeometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(PathGeometry), typeof(frmStatisticalDescription), new PropertyMetadata(Application.Current.FindResource("book") as PathGeometry));



        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(frmStatisticalDescription), new PropertyMetadata("Description"));



        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(frmStatisticalDescription), new PropertyMetadata("value"));


        public frmStatisticalDescription()
        {
            InitializeComponent();

            this.Closing += StatisticalDescription_Closing;
        }

        private void StatisticalDescription_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            return;
        }
    }
}
