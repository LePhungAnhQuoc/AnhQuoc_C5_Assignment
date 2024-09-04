using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment.DTOs.ApiDtos
{
    public interface IMapFromModel
    {
        object MapToAdd();
        object MapToUpdate();
    }
}
