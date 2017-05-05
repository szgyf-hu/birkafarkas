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

            for (int i = 0, x = 2, y = 0; i < 3; i++)
                Csomopontok[x, y].Ellista[(int)EIrany.JobbraLe] = Csomopontok[++x, ++y];

            for (int i = 0, x = 4, y = 0; i < 3; i++)
                Csomopontok[x, y].Ellista[(int)EIrany.BalraLe] = Csomopontok[--x, ++y];

            for (int i = 0, x = 0, y = 2; i < 3; i++)
                Csomopontok[x, y].Ellista[(int)EIrany.JobbraLe] = Csomopontok[++x, ++y];

            for (int i = 0, x = 6, y = 2; i < 3; i++)
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
                        if (x>0 && y>0
                            &&
                            Csomopontok[x - 1, y - 1] != null
                            &&
                            Csomopontok[x - 1, y - 1].Ellista[(int)EIrany.JobbraLe] != null)
                            Csomopontok[x, y].Ellista[(int)EIrany.BalraFel] = Csomopontok[x - 1, y - 1];

                        // Felfele vizsgálat
                        if (y>0
                            &&
                            Csomopontok[x, y - 1] != null
                            &&
                            Csomopontok[x, y - 1].Ellista[(int)EIrany.Le] != null)
                            Csomopontok[x, y].Ellista[(int)EIrany.Fel] = Csomopontok[x, y - 1];

                        // JobbraFel vizsgálat
                        if (x<6 && y>0
                            &&
                            Csomopontok[x + 1, y - 1] != null
                            &&
                            Csomopontok[x + 1, y - 1].Ellista[(int)EIrany.BalraLe] != null)
                            Csomopontok[x, y].Ellista[(int)EIrany.JobbraFel] = Csomopontok[x + 1, y - 1];

                        // Balra vizsgálat 
                        if (x>0
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
            for (int y = 0; y < 7; y++)
                for (int x = 0; x < 7; x++)
                    if (Csomopontok[x, y] != null)
                        if (x < 4)
                            Csomopontok[x, y].Tipus = ECsomopontTipus.Birka;
                        else
                            Csomopontok[x, y].Tipus = ECsomopontTipus.Üres;

            Csomopontok[5, 3].Tipus = ECsomopontTipus.Farkas;
        }
    }
}
