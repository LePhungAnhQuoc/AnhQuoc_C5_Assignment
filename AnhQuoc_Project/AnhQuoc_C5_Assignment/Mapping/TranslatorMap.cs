using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class TranslatorMap : MapBase<Translator, TranslatorDto>
    {
        public override TranslatorDto ConvertToDto(Translator sourceItem)
        {
            TranslatorDto newItem = new TranslatorDto(sourceItem.Id);
            Utilities.Copy(newItem, sourceItem);

            return newItem;
        }
    }
}
