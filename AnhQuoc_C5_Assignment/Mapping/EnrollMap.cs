using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class EnrollMap : MapBase<Enroll, EnrollDto>
    {
        public override EnrollDto ConvertToDto(Enroll sourceItem)
        {
            EnrollDto newItem = new EnrollDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);

            newItem.Reader = UnitOfViewModel.Instance.ReaderViewModel.FindById(sourceItem.IdReader);

            if (sourceItem.IdBook != null)
                newItem.Book = UnitOfViewModel.Instance.BookViewModel.FindById((int)sourceItem.IdBook, null);

            return newItem;
        }
    }
}
