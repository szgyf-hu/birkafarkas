using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace birkafarkas
{
    class Palya
    {
        public Csomopont[,] Csomopontok = new Csomopont[7, 7]; // [x,y]

        public Palya()
        {
            for (int y = 0; y < 7; y++)
                for (int x = 0; x < 7; x++)
                    if (x > 1 && x < 5 || y > 1 && y < 5)
                        Csomopontok[x, y] = new Csomopont();

            for (int i = 0, x = 2, y = 0; i < 4; i++)
                Csomopontok[x, y].Ellista[(int)EIrany.JobbraLe] = Csomopontok[++x, ++y];

            for (int i = 0, x = 4, y = 0; i < 4; i++)
                Csomopontok[x, y].Ellista[(int)EIrany.BalraLe] = Csomopontok[--x, ++y];

            for (int i = 0, x = 0, y = 2; i < 4; i++)
                Csomopontok[x, y].Ellista[(int)EIrany.JobbraLe] = Csomopontok[++x, ++y];

            for (int i = 0, x = 6, y = 2; i < 4; i++)
                Csomopontok[x, y].Ellista[(int)EIrany.BalraLe] = Csomopontok[--x, ++y];

            for (int y = 0; y < 7; y++)
                for (int x = 0; x < 7; x++)
                    if (Csomopontok[x, y] != null // Aktuális csomópontnak léteznie kell
                        &&
                        x < 6 // utolsó oszlopban lévő csomópontnak nincs jobboldali szomszédja
                        &&
                        Csomopontok[x + 1, y] != null) // jobb oldali szomszédjának élőnek kell lenni
                        Csomopontok[x, y].Ellista[(int)EIrany.Jobbra] = Csomopontok[x + 1, y];

            for (int y = 0; y < 7; y++)
                for (int x = 0; x < 7; x++)
                    if (Csomopontok[x, y] != null // Aktuális csomópontnak léteznie kell
                        &&
                        y < 6 // utolsó sorban lévő csomópontnak nincs alsó szomszédja
                        &&
                        Csomopontok[x, y + 1] != null) // alsó szomszédjának élőnek kell lenni
                        Csomopontok[x, y].Ellista[(int)EIrany.Le] = Csomopontok[x, y + 1];

            for (int y = 0; y < 7; y++)
                for (int x = 0; x < 7; x++)
                    if (Csomopontok[x, y] != null)
                    {
                        // BalraFel vizsgálat
                        if (x > 0 && y > 0
                            &&
                            Csomopontok[x - 1, y - 1] != null
                            &&
                            Csomopontok[x - 1, y - 1].Ellista[(int)EIrany.JobbraLe] != null)
                            Csomopontok[x, y].Ellista[(int)EIrany.BalraFel] = Csomopontok[x - 1, y - 1];

                        // Felfele vizsgálat
                        if (y > 0
                            &&
                            Csomopontok[x, y - 1] != null
                            &&
                            Csomopontok[x, y - 1].Ellista[(int)EIrany.Le] != null)
                            Csomopontok[x, y].Ellista[(int)EIrany.Fel] = Csomopontok[x, y - 1];

                        // JobbraFel vizsgálat
                        if (x < 6 && y > 0
                            &&
                            Csomopontok[x + 1, y - 1] != null
                            &&
                            Csomopontok[x + 1, y - 1].Ellista[(int)EIrany.BalraLe] != null)
                            Csomopontok[x, y].Ellista[(int)EIrany.JobbraFel] = Csomopontok[x + 1, y - 1];

                        // Balra vizsgálat 
                        if (x > 0
                            &&
                            Csomopontok[x - 1, y] != null
                            &&
                            Csomopontok[x - 1, y].Ellista[(int)EIrany.Jobbra] != null)
                            Csomopontok[x, y].Ellista[(int)EIrany.Balra] = Csomopontok[x - 1, y];

                    }

            Alaphelyzet();
        }

        public void Alaphelyzet()
        {
      /*      for (int y = 0; y < 7; y++)
                for (int x = 0; x < 7; x++)
                    if (Csomopontok[x, y] != null)
                        if (x < 4)
                            Csomopontok[x, y].Tipus = ECsomopontTipus.Birka;
                        else
                            Csomopontok[x, y].Tipus = ECsomopontTipus.Üres;
                            */

            Csomopontok[5, 3].Tipus = ECsomopontTipus.Farkas;
            Csomopontok[4, 3].Tipus = ECsomopontTipus.Birka;
            Csomopontok[4, 2].Tipus = ECsomopontTipus.Birka;
            Csomopontok[2, 3].Tipus = ECsomopontTipus.Birka;
        }

        public void TippekBeallitasa(int px, int py)
        {
            for (int y = 0; y < 7; y++)
                for (int x = 0; x < 7; x++)
                    if (Csomopontok[x, y] != null)
                        Csomopontok[x, y].LepesTipp = ETipp.Semmi;


            if (px < 0
                ||
                px > 6
                ||
                py < 0
                ||
                py > 6)
                return;

            Csomopont cs = Csomopontok[px, py];

            if (cs == null)
                return;

            if (cs.Tipus == ECsomopontTipus.Üres)
                return;

            switch (cs.Tipus)
            {
                case ECsomopontTipus.Birka:
                    for (int i = 0; i < cs.Ellista.Length; i++)
                        if (cs.Ellista[i] != null) // arra van út
                            if (cs.Ellista[i].Tipus == ECsomopontTipus.Üres)
                                cs.Ellista[i].LepesTipp = ETipp.Lepes;

                    break;
                case ECsomopontTipus.Farkas:
                    farkasTippek(px, py, false);
                    break;
            }
        }

        void farkasTippek(int x, int y, bool ugrassalerkezett)
        {
            if (x < 0
                ||
                x > 6
                ||
                y < 0
                ||
                y > 6)
                return;

            Csomopont cs = Csomopontok[x, y];

            if (cs == null)
                return;

            for (int i = 0; i < cs.Ellista.Length; i++)
                if (cs.Ellista[i] != null)
                    switch (cs.Ellista[i].Tipus)
                    {
                        case ECsomopontTipus.Üres:
                            if (!ugrassalerkezett)
                                cs.Ellista[i].LepesTipp = ETipp.Lepes;
                            break;
                        case ECsomopontTipus.Birka:
                            if (cs.Ellista[i].Ellista[i] != null)
                                if (
                                    cs.Ellista[i].Ellista[i].Tipus == ECsomopontTipus.Üres
                                    &&
                                    cs.Ellista[i].Ellista[i].LepesTipp == ETipp.Semmi
                                    )
                                {
                                    cs.Ellista[i].Ellista[i].LepesTipp = ETipp.Ugras;
                                    switch ((EIrany)i)
                                    {
                                        case EIrany.Fel: farkasTippek(x, y - 2, true); break;
                                        case EIrany.JobbraFel: farkasTippek(x + 2, y - 2, true); break;
                                        case EIrany.Jobbra: farkasTippek(x + 2, y, true); break;
                                        case EIrany.JobbraLe: farkasTippek(x + 2, y + 2, true); break;
                                        case EIrany.Le: farkasTippek(x, y + 2, true); break;
                                        case EIrany.BalraLe: farkasTippek(x - 2, y + 2, true); break;
                                        case EIrany.Balra: farkasTippek(x - 2, y, true); break;
                                        case EIrany.BalraFel: farkasTippek(x - 2, y - 2, true); break;
                                    }
                                }
                            break;
                    }
        }
    }
}
