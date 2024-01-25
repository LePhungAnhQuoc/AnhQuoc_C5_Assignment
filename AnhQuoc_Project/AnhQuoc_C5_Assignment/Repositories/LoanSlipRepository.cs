using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanSlipRepository : Repository<LoanSlip>
    {
        public LoanSlipRepository() : base()
        {
        }
        public LoanSlipRepository(ObservableCollection<LoanSlip> items) : base(items)
        {
        }

       
    }
}
