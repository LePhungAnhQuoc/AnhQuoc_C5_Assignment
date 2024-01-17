using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    class BorrowBookViewModel : BaseViewModel<object>, IPageViewModel
    {

        // DTOs (Data, Pagination, PropertyChanged-tool)


        #region Data-GiaoDien
        #region Selected-Item

        #endregion


        #endregion

        public string Name { get; }
        


        // All RelayCommand



        public BorrowBookViewModel()
        {
            Name = "Search Page";
            
            // Init Dtos

            // Implement command


        }

    }
}
