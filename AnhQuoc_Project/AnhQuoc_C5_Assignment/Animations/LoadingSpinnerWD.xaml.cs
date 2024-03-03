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

namespace AnhQuoc_C5_Assignment.Animations
{
    /// <summary>
    /// Interaction logic for LoadingSpinnerWD.xaml
    /// </summary>
    public partial class LoadingSpinnerWD : Window
    {
        private readonly Action _action;
        public LoadingSpinnerWD(Action action)
        {
            InitializeComponent();

            _action = action;

            this.Loaded += LoadingSpinnerWD_Loaded;
        }

        private async void LoadingSpinnerWD_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(_action);
            this.Close();
        }
    }
}
