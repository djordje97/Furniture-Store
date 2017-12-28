using PoP.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POP_SF42_2016_GUI.UI
{
    public class KolicinaValidation: ValidationRule
    {
        public static Namestaj Nam { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
          
             try
                {
                var broj = (int)value;
                    if (broj < 0)
                        return new ValidationResult(false, "Morate uneti pozitivan broj");
                    if(broj> Nam.Kolicina)
                        return new ValidationResult(false, "Namestaja nema u datoj kolicini");
                return new ValidationResult(true, null);
            }
                catch (Exception)
                {
                    return new ValidationResult(false, "Morate uneti pozitivan ceo broj za kolicinu");
                }
        }
    }
}
