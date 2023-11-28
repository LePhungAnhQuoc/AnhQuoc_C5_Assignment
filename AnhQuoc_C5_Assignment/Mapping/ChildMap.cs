using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ChildMap : MapBase<Child, ChildDto>
    {
        public override ChildDto ConvertToDto(Child itemSource)
        {
            var readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            var adultVM = UnitOfViewModel.Instance.AdultViewModel;

            ChildDto newItem = new ChildDto(itemSource.IdReader);
            newItem.Reader = readerVM.FindById(itemSource.IdReader);

            newItem.AdultReader = readerVM.FindById(itemSource.IdAdult);
            newItem.Adult = adultVM.FindByIdReader(itemSource.IdAdult, null);

            newItem.Status = itemSource.Status;
            newItem.CreatedAt = itemSource.CreatedAt;
            newItem.ModifiedAt = itemSource.ModifiedAt;

            return newItem;
        }
    }
}
