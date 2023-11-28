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
    /// Interaction logic for ucAuthorsTable.xaml
    /// </summary>
    public partial class ucAuthorsTable : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ObservableCollection<Author>> getAuthors { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<AuthorDto> _Authors;
        public ObservableCollection<AuthorDto> Authors
        {
            get
            {
                return _Authors;
            }
            set
            {
                _Authors = value;
                OnPropertyChanged();
            }
        }

        private AuthorDto _SelectedDto;
        public AuthorDto SelectedDto
        {
            get { return _SelectedDto; }
            set
            {
                _SelectedDto = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Mapping
        private AuthorMap authorMap;
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


        public ucAuthorsTable()
        {
            InitializeComponent();
            authorMap = UnitOfMap.Instance.AuthorMap;

            Loaded += UcAuthorsTable_Loaded;
            this.DataContext = this;
        }

        private void UcAuthorsTable_Loaded(object sender, RoutedEventArgs e)
        {            
            if (getAuthors != null)
            {
                var listAuthor = getAuthors();
                Authors = authorMap.ConvertToDto(listAuthor);
            }
        }
    }
}
