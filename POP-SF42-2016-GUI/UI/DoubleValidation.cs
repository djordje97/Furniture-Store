using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF42_2016_GUI.UI
{
    public class DoubleValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string vrednost = value as string;
            vrednost.Trim();
            if (vrednost == null)
                return new ValidationResult(false, "Polje ne sme biti prazno");
            else if (vrednost.Length > 0)
            {
                try
                {
                    var broj = double.Parse(vrednost);
                    if (broj < 0)
                        return new ValidationResult(false, "Morate uneti pozitivan broj");
                }
                catch (Exception)
                {
                    return new ValidationResult(false, "Morate uneti pozitivan ceo broj za cenu/kolicinu");
                }
            }
            return new ValidationResult(true,null);


        }

    }
}
