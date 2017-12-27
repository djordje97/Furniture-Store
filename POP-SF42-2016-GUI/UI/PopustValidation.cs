using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF42_2016_GUI.UI
{
    public class PopustValidation: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {


            try
            {
                int vrednost = (int)value;
                var broj = (int)value;
                if (broj < 0)
                    return new ValidationResult(false, "Morate uneti pozitivan broj");
                else if (broj < 5 || broj > 90)
                    return new ValidationResult(false, "uneseni popust nije omogucen");
                else
                return new ValidationResult(true, "");
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Morate uneti pozitivan ceo broj");
            }
        }
    }
}
