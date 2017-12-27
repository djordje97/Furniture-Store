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
    public class KorisnickoimeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string korIme = value as string;
            if (korIme.Length==0)
            {
                return new ValidationResult(false, "Morate uneti korisnicko ime");
            }
            if (korIme == null)
            {
                return new ValidationResult(false, "Morate uneti korisnicko ime");
            }
           foreach(var korisnik in Projekat.Instance.Korisnici)
            {
                if(korisnik.Korisnicko_Ime== korIme)
                {
                    return new ValidationResult(false, "To korisnicko ime vec postoji");
                }
            }
            return new ValidationResult(true, "");
          
        }
    }

}