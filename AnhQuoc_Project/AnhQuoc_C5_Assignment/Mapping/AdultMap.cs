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
            var readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            var readerMap = UnitOfMap.Instance.ReaderMap;

            bool child_Status = true;
            ReaderDto readerDto = readerMap.ConvertToDto(readerVM.FindById(itemSource.IdReader));

            AdultDto newItem = new AdultDto(itemSource.IdReader);

            newItem.Identify = itemSource.Identify;
            newItem.Address = itemSource.Address;
            newItem.City = itemSource.City;
            newItem.Phone = itemSource.Phone;

            newItem.ExpireDate = itemSource.ExpireDate;
            newItem.Status = itemSource.Status;

            newItem.CreatedAt = itemSource.CreatedAt;
            newItem.ModifiedAt = itemSource.ModifiedAt;

            newItem.ReaderName = readerDto.FullName;
            newItem.ListChild = childVM.FillByIdAdult(itemSource.IdReader, child_Status);
            
            return newItem;
        }
    }
}
