using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProjekt1
{
    class Program
    {
        static void Main(string[] args)
        {           
            Chuze chuze = new Chuze();
            Bitmapovani mapa = new Bitmapovani();
            mapa.ZobrazMojeHraciPlocha();                                               // Zobrazí prázdné hrací pole

            while (true)                                                                // Při chybném zadání je program znovu spouštěn
            {
                Console.WriteLine("\n ZADEJTE POLOHU STARTU.");
                Console.WriteLine("Poloha x-té souřadnice: ");                          // Zprácování startovních souřadnic
                string x = Console.ReadLine();                                          // Přečte zápis od uživatele
                if (!int.TryParse(x, out int x_start))                                  // Zkontroluju si vstup od uživatele, a když ho zadá špatně, tak se mu zobrazí chybová hláška 
                Console.WriteLine("Toto není číslo.");
                Console.WriteLine("Poloha y-té souřadnice: ");
                x = Console.ReadLine();
                if (!int.TryParse(x, out int y_start))
                Console.WriteLine("Toto není číslo");
                Console.WriteLine("\nZADEJTE POLOHU CÍLE.");                            // Zpracování cílových souřadnic
                Console.WriteLine("Poloha x-té souřadnice: ");
                x = Console.ReadLine();
                if (!int.TryParse(x, out int x_cil))
                    Console.WriteLine("Toto není číslo");
                Console.WriteLine("Poloha y-té souřadnice: ");
                x = Console.ReadLine();
                if (!int.TryParse(x, out int y_cil))
                    Console.WriteLine("Toto není číslo");

                if ((mapa.MojePlochaVygenerovanaZObrazku_0a1[x_start, y_start] == 0) && (mapa.MojePlochaVygenerovanaZObrazku_0a1[x_cil, y_cil] == 0) && x_cil < 33 && y_cil < 33 && x_start < 33 && y_start < 33)       // Zkontroluje, jestli jsou dané hodnoty v mezi pole a jestli nejsou umístěné do zdi bludiště
                    chuze.Jed(x_start, y_start, x_cil, y_cil);                  // Spuštění chodu funkce
                Console.WriteLine("CHYBA \n Zadal jsi něco jiného než číslo, nebo jsi dal některou polohu vetší než 32 (přesáhl jsi velikost pole, nebo jsi umístil některou polohu do zdi. \n Zkus to znovu");
            }           
        }
    }
}
