using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailHistoryRepository : Repository<LoanDetailHistory>
    {
        public LoanDetailHistoryRepository(APIProvider<LoanDetailHistory> ApiProvider) : base(ApiProvider)
        {
        }
    }
}
