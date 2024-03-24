using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{


    public class ExpireDateRule : ValidationRule
    {
        public Wrapper Wrapper { get; set; }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            AdultViewModel adultVM = UnitOfViewModel.Instance.AdultViewModel;
            ReaderViewModel readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            ChildViewModel childVM = UnitOfViewModel.Instance.ChildViewModel;

            ReaderDto reader = Wrapper.Reader;
            if (reader == null)
                return ValidationResult.ValidResult;

            DateTime exprireDateValue;
            if (reader.ReaderType == ReaderType.Adult)
            {
                Adult adult = adultVM.FindByIdReader(reader.Id);
                exprireDateValue = adult.ExpireDate;

                if (DateTime.Now > exprireDateValue)
                {
                    return new ValidationResult(false, "Sorry, your reader card has expired");
                }
            }
            else if (reader.ReaderType == ReaderType.Child)
            {
                Child child = childVM.FindByIdReader(reader.Id);

                Adult adult = adultVM.FindByIdReader(child.IdAdult);
                exprireDateValue = adult.ExpireDate;

                if (DateTime.Now > exprireDateValue)
                {
                    return new ValidationResult(false, "Sorry, your guardian's reader card has expired");
                }
            }
            return ValidationResult.ValidResult;
        }

    }

    public class Wrapper : DependencyObject
    {


        public ReaderDto Reader
        {
            get { return (ReaderDto)GetValue(ReaderProperty); }
            set { SetValue(ReaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Reader.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReaderProperty =
            DependencyProperty.Register("Reader", typeof(ReaderDto), typeof(Wrapper), new PropertyMetadata(null));


    }

}
