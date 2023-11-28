using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class AdultMap : MapBase<Adult, AdultDto>
    {
        public override AdultDto ConvertToDto(Adult itemSource)
        {
            var childVM = UnitOfViewModel.Instance.ChildViewModel;

            bool child_Status = true;

            AdultDto newItem = new AdultDto(itemSource.IdReader);

            newItem.Identify = itemSource.Identify;
            newItem.Address = itemSource.Address;
            newItem.City = itemSource.City;
            newItem.Phone = itemSource.Phone;

            newItem.ExpireDate = itemSource.ExpireDate;
            newItem.Status = itemSource.Status;

            newItem.ListChild = childVM.GetChildFromAdult(itemSource.IdReader, child_Status);

            newItem.CreatedAt = itemSource.CreatedAt;
            newItem.ModifiedAt = itemSource.ModifiedAt;

            return newItem;
        }
    }
}
