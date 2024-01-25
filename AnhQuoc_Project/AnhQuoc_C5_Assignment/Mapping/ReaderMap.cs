using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class ReaderMap : MapBase<Reader, ReaderDto>
    {
        public override ReaderDto ConvertToDto(Reader sourceItem)
        {
            var childVM = UnitOfViewModel.Instance.ChildViewModel;

            bool child_Status = true;

            ReaderDto newItem = new ReaderDto(sourceItem.Id);
            newItem.LName = sourceItem.LName;
            newItem.FName = sourceItem.FName;

            newItem.boF = sourceItem.boF;

            newItem.ReaderType = sourceItem.ReaderType.ConvertValue();

            if (sourceItem.ReaderType.ConvertValue() == ReaderType.Adult)
            {
                var childsInAdult = childVM.FillByIdAdult(sourceItem.Id, child_Status);
                newItem.ChildsQuantity = childsInAdult.Count;
            }
            else if (sourceItem.ReaderType.ConvertValue() == ReaderType.Child)
            {
                newItem.ChildsQuantity = -1;
            }
            newItem.Status = sourceItem.Status;
            newItem.CreatedAt = sourceItem.CreatedAt;
            newItem.ModifiedAt = sourceItem.ModifiedAt;

            return newItem;
        }
    }
}
