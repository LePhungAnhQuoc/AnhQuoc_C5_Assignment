using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class TranslatorViewModel: BaseViewModel<Translator>
    {
        public TranslatorViewModel()
        {
            Item = new Translator();
            Repo = new TranslatorRepository();
            prefix = Constants.prefixTranslator;
            numberPrefix = 2;
        }

        public string GetId()
        {
            int index = base.getMaxIndexId(nameof(Item.Id));
            return base.GetId(index);
        }

        public Translator FindById(string idValue)
        {
            return FindById(Repo.Gets(), idValue);
        }

        public Translator FindById(ObservableCollection<Translator> source, string idValue)
        {
            return source.FirstOrDefault(item => item.Id == idValue);

        }


        public bool IsCheckEmptyItem(TranslatorDto item)
        {
            return Utilities.IsCheckEmptyItem(item, false);
        }

        public Translator CreateByDto(TranslatorDto dto)
        {
            Translator translator = new Translator();
            Copy(translator, dto);
            return translator;
        }

        public void Copy(Translator dest, TranslatorDto source)
        {
            Utilities.Copy(dest, source);
        }

        public void Copy(TranslatorDto dest, TranslatorDto source)
        {
            Utilities.Copy(dest, source);
        }
    }
}
