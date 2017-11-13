﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.Model
{
   public class TipNamestaja
    {
        public int ID { get; set; }
        public bool Obrisan { get; set; }
        public string Naziv { get; set; }

        public override string ToString()
        {
            if (!Obrisan)
            {
                return Naziv;

            }
            return null;
        }

        public static TipNamestaja PronadjiTip(int id)
        {
            foreach (var tip in Projekat.Instance.TipNamestaja)
            {
                if (tip.ID == id)
                {
                    return tip;
                }

            }
            return null;
        }
    }
}
