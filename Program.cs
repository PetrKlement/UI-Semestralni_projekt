using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Projekt_AstarAlgoritmus
{
    class Program
    {
        static void Main(string[] args)
        {
            Walk walk = new Walk();
            Bitmapping map = new Bitmapping();
            // Zobrazí prázdné hrací pole
            map.ViewMyGameBoard();
            // Při chybném zadání je program znovu spouštěn
            while (true)                                                                
            {
                Console.WriteLine("\n ZADEJTE POLOHU STARTU.");
                Console.WriteLine(" Poloha x-té souřadnice: ");
                // Zprácování startovních souřadnic od uživatele
                if (!int.TryParse(Console.ReadLine(), out int x_start))                                  
                    // Zkontroluju si vstup od uživatele, a když ho zadá špatně, tak se mu zobrazí chybová hláška 
                    Console.WriteLine(" Toto není číslo.");
                Console.WriteLine(" Poloha y-té souřadnice: ");
                if (!int.TryParse(Console.ReadLine(), out int y_start))
                    Console.WriteLine("  není číslo");
                Console.WriteLine(" Poloha x-té souřadnice: ");
                Console.WriteLine("\n ZADEJTE POLOHU CÍLE.");                            
                // Zpracování cílových souřadnic
                if (!int.TryParse(Console.ReadLine(), out int x_finish))
                    Console.WriteLine(" Toto není číslo");
                Console.WriteLine(" Poloha y-té souřadnice: ");
                if (!int.TryParse(Console.ReadLine(), out int y_finish))
                    Console.WriteLine(" Toto není číslo");
                // Zkontroluje, jestli jsou dané hodnoty v mezi pole a jestli nejsou umístěné do zdi bludiště
                if ((map.LabyrinthZerosAndOnes[x_start, y_start] == 0) && (map.LabyrinthZerosAndOnes[x_finish, y_finish] == 0) && x_finish < 33 && y_finish < 33 && x_start < 33 && y_start < 33)
                    // Spuštění chodu funkce
                    walk.Go(x_start, y_start, x_finish, y_finish);                  
                Console.WriteLine("CHYBA \n Zadal jsi něco jiného než číslo, nebo jsi dal některou polohu vetší než 32 (přesáhl jsi velikost pole, nebo jsi umístil některou polohu do zdi. \n Zkus to znovu");
            }
        }
    }
}
