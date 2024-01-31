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
    /// Interaction logic for frmAddProvince.xaml
    /// </summary>
    public partial class frmAddProvince : Window
    {
        #region getDatas
        public Func<ProvinceDto> getItemToUpdate { get; set; }

        public Func<int> getIdProvince { get; set; }
        public Func<ProvinceRepository> getProvinceRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        public AddProvinceViewModel Context;

        public frmAddProvince()
        {
            InitializeComponent();

            Context = new AddProvinceViewModel();
            this.DataContext = Context;
        }
    }
}
