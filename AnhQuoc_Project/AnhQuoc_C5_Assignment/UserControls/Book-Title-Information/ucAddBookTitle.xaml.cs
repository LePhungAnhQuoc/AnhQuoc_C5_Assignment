using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucAddBookTitle.xaml
    /// </summary>
    public partial class ucAddBookTitle : UserControl
    {
        #region GetDatas
        public Func<string> getId { get; set; }
        public Func<BookTitleRepository> getBookTitleRepo { get; set; }
        public Func<CategoryRepository> getCategoryRepo { get; set; }
        public Func<AuthorRepository> getAuthorRepo { get; set; }
        #endregion

        public AddBookTitleViewModel Context;

        public ucAddBookTitle()
        {
            InitializeComponent();
            
            Context = new AddBookTitleViewModel();
            this.DataContext = Context;
        }
    }
}
