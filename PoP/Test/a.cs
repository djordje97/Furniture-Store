using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostorimena
{
    class A
    {
        private string ime;
        public string Getime()
        {
            return this.ime;
        }
        public void Setime(string ime)
        {
            this.ime = ime;
        }
        public string Ime
        {
            get
            {
                return this.ime;
            }

            set
            {
                this.ime = value;
            }
        }

    }
}
