using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace birkafarkas
{
    class Csomopont
    {
        /*         7 0 1
         *          \|/
         *         6-*-2
         *          /|\
         *         5 4 3
         */

        public ECsomopontTipus Tipus = ECsomopontTipus.Üres;

        public ETipp LepesTipp = ETipp.Semmi;

        public Csomopont[] Ellista = new Csomopont[8];
    }
}
