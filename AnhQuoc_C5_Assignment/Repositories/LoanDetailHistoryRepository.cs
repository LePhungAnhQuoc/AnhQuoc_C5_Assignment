﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailHistoryRepository : Repository<LoanDetailHistory>
    {
        public LoanDetailHistoryRepository()
        {
        }

        public LoanDetailHistoryRepository(ObservableCollection<LoanDetailHistory> items) : base(items)
        {
        }
    }
}
