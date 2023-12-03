using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class EnrollViewModel : ViewModelBase<Enroll>
    {
        public EnrollViewModel()
        {
            Item = new Enroll();
            Repo = new EnrollRepository();
            prefix = Constants.prefixEnroll;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public Enroll FindById(string id)
        {
            foreach (Enroll item in Repo)
            {
                if (string.Compare(item.Id, id, true) == 0)
                {
                    return item;
                }
            }
            return null;
        }

        public bool IsCheckEmptyItem(Enroll item)
        {
            if (Utilities.IsCheckEmptyString(item.Id))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.ISBN))
            {
                return false;
            }

            if (Utilities.IsCheckEmptyString(item.IdReader))
            {
                return false;
            }

            if (item.EnrolDate == null)
            {
                return false;
            }

            if (item.ExpDate == null)
            {
                return false;
            }

            //if (item.Note == null)
            //{
            //    return false;
            //}

            if (item.IdBook == null)
            {
                return false;
            }

            return true;
        }
    }
}
