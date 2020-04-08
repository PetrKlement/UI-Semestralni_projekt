using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UIProjekt1
{
    class Chuze
    {
        public List<Poloha> fringe = new List<Poloha>();        // Frige - seznam k prohledání
        public List<Poloha> explored = new List<Poloha>();      // Explored - seznam již prohledaných
        public Poloha pol;                                      // Poloha určená pouze ke zjištění ceny
       // public Poloha polc;
        public Poloha polcs;                                    // Kompletní poloha v bludišti
        public Bitmapovani bitmapovani = new Bitmapovani();     // Instance bitmapování
        private int xSouradnice;
        private int ySouradnice;
        bool napln;                                             // Povolení k naplnění fringe
        private int xCil;
        private int yCil;
        public void Jed(int X, int Y, int XCil,int YCil)        // Hlavní funkce pro chod programu
        {
            xCil = XCil;
            yCil = YCil;
            //int kroku = 0;
            Poloha start = new Poloha(X, Y,  Poloha.SmerNatoceni.Jih);                                  // Vygenerovani polohy pro startovni odhad vzdálenosti
            
            Poloha startovaci = new Poloha(X, Y, Cena(start), Poloha.SmerNatoceni.Jih, xCil, yCil);     // Vytvoření prvního prvku ve fringe

            fringe.Add(startovaci);                                                                     // Přidání "startovaci" do fringe

            for (int c =0; c < bitmapovani.MojePlochaVygenerovanaZObrazku_0a1.Length; c++)              // Zaručí projití všech prvků
            {
                ProjdiOkoliADejMoznyMaticeDoFringe(fringe[0], fringe[0].smernatoceni);                  // Generace dalších poloh okolo té aktuální
                explored.Add(fringe[0]);                                                                //Uložení do explored
                fringe.RemoveAt(0);                                                                     // a odstranění z fringe               
                IEnumerable<Poloha> polas = fringe.OrderBy(a => a.cena);                                // Vytvoření pomocné "polas" ve které jsou srovnané prvky z fringe
                fringe = new List<Poloha>();                                                            // Smazání fringe
                foreach (Poloha vvv in polas)
                    fringe.Add(vvv);                                                                    // Uložení všech seřazených prvků zpět do fringe 
                KontrolujMoznosti();                                                                    // Ověření, jestli program došel k cíly
                //seradim si prvky podle ceny
                if(fringe[0].cena>=100 )
                  bitmapovani.MaticeKZobrazeni[fringe[0].aktualniXY[0], fringe[0].aktualniXY[1]] = fringe[0].cena.ToString();               // zde ukládáme do matice pro zobrazení hodnoty ceny z fringe[0]. Tři if jsou použity pouye kvůli zarovnání tabulky, aby se vizuálně neposunovala
                if (fringe[0].cena < 100 && fringe[0].cena > 9)
                  bitmapovani.MaticeKZobrazeni[fringe[0].aktualniXY[0], fringe[0].aktualniXY[1]] = fringe[0].cena.ToString() + " ";
                if (fringe[0].cena < 10 )
                    bitmapovani.MaticeKZobrazeni[fringe[0].aktualniXY[0], fringe[0].aktualniXY[1]] = fringe[0].cena.ToString() + "  ";

                Console.Clear();                                // Vymazání obsahu konzole
                bitmapovani.ZobrazMojeHraciPlocha();            // Vykreslaní zobrazovací tabulky do konzole
                Thread.Sleep(500);                              // Vyčkání 0,5s aby uživatel viděl aktuální polohu
            }
            Console.ReadKey();
        }
        /// <summary>
        /// KontrolujMoznosti() slouží ke kontrole, jestli není fringe prázdný, pokud se fringe vyprázdní, tak nelze nalézt cílový bod, v našem případě to znamená, že cíl je ze startu nedostupný
        /// </summary>
        private void KontrolujMoznosti()
        {
            if (fringe.Count==0)
            {
                Console.WriteLine(" Nelze se dostat k cíly, skončil jsem na pozici {0}-x,  {1}-y.", xSouradnice, ySouradnice);
                Console.ReadKey();
            }
        }
        /// <summary>
        ///  Cena(Poloha aktual) si vezme polohu v bludišti, a vrátí její cenu, která je zavislá na vzdálenosti
        /// </summary>
        /// <param name="aktual"></param>
        /// <returns></returns>

        public int Cena(Poloha aktual)
        {
            return (Math.Abs(aktual.aktualniXY[0] - xCil) + Math.Abs(aktual.aktualniXY[1] - yCil))*2;           // *2 je použito k nastavení heuristické funkce
        }
        /// <summary>
        /// ZkontrolujDuplicityAGenerujPolohy(Poloha a) vezme nově vybenerovaný prvek, porovná ho s fringe a explored a je-li unikátní, tak ho přidá do fringe
        /// Také kontroluje shodu s cílovým stavem
        /// </summary>
        /// <param name="a"></param>

        private void ZkontrolujDuplicityAGenerujPolohy(Poloha a)
        {
             xSouradnice = a.aktualniXY[0];
             ySouradnice = a.aktualniXY[1];
            foreach (Poloha p in explored)
            {
                if (p.aktualniXY[0] == xSouradnice && p.aktualniXY[1] == ySouradnice)
                    napln = false;
            }
            foreach (Poloha d in fringe)
            {
                if (d.aktualniXY[0] == xSouradnice && d.aktualniXY[1] == ySouradnice)
                    napln = false;
            }
            if (napln)
                fringe.Add(a);
            napln = true;
            if((a.aktualniXY[0] == a.stopXY[0]) && (a.aktualniXY[1] == a.stopXY[1]))                    // Je-li poloha aktuálního prvku shodná s cílovou, tak vypíše cílovou hlášku a zastaví chod
            {
                Console.WriteLine(" Našel jsem cíl na pozici {0}-x,  {1}-y", xSouradnice, ySouradnice);
                Console.ReadKey();
            }
        }

        public void ProjdiOkoliADejMoznyMaticeDoFringe(Poloha aktual, Poloha.SmerNatoceni smer)
        {          
            int x = aktual.aktualniXY[0];
            int y = aktual.aktualniXY[1];
            napln = true;                                                                   // Rozhoduje, jestli dojde k přidání nového prvku 
            if (smer == Poloha.SmerNatoceni.Jih)                                            // Pokud je směr natočení Jih:
            {
                x--;                                                                        // Prostor na sever
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0 )             // Kontrola, jestli nejsme ve zdi
                {
                    pol = new Poloha(x, y);                                                 // Generace polohy pro zjištění ceny
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;                     // Funkce určení ceny konkrétního pole
                    a +=2;                                                                  // +2, protože změna o 180 stupňů stojí 2
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Sever, xCil, yCil);     // Vytvoření nové polohy
                    //if(!path.Contains(polcs) && !fringe.Contains(polcs))
                    ZkontrolujDuplicityAGenerujPolohy(polcs);                               // zkontroluce duplicitu s fringe a explored, v případě neexistence dupliciti přidá tuto polohu do fringe
                }
                x+=2;                                                                       // Prostor na jih
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Jih, xCil, yCil);       
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
                x--;
                y++;                                                                        // Prostor na Západ
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    a++;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Vychod, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
                y-=2;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    a++;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Zapad, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
            }
            else if (smer == Poloha.SmerNatoceni.Sever)
            {
                x--;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Sever, xCil, yCil);
                    //if(!path.Contains(polcs) && !fringe.Contains(polcs))
                    ZkontrolujDuplicityAGenerujPolohy(polcs);

                }
                x += 2;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    a += 2;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Jih, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
                x--;
                y++;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    a++;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Vychod, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
                y -= 2;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    a++;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Zapad, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
            }
            else if (smer == Poloha.SmerNatoceni.Vychod)
            {
                x--;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    a++;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Sever, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
                x += 2;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    a++;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Jih, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
                x--;
                y++;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Vychod, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
                y -= 2;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    a += 2;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Zapad, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }

            }
            else if (smer == Poloha.SmerNatoceni.Zapad)
            {
                x--;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    a++;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Sever, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
                x += 2;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    a++;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Jih, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
                x--;
                y++;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    a += 2;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Vychod, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
                y -= 2;
                if (bitmapovani.MojePlochaVygenerovanaZObrazku_0a1[x, y] == 0)
                {
                    pol = new Poloha(x, y);
                    int a = Cena(pol) + aktual.cena - Cena(aktual) + 3;
                    polcs = new Poloha(x, y, a, Poloha.SmerNatoceni.Zapad, xCil, yCil);
                    ZkontrolujDuplicityAGenerujPolohy(polcs);
                }
            }
            else
            {
                Console.WriteLine("hej mas tu problem s generaci poloh");
                Console.ReadKey();
            }
        }
    }
}
