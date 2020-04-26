using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UI_Projekt_AstarAlgoritmus
{
    class Walk
    {
        /// <summary>
        /// frige - seznam poloh k prohledání// 
        /// </summary>
        public List<Position> fringe = new List<Position>();
        /// <summary>
        /// explored - seznam již prohledaných poloh 
        /// </summary>
        public List<Position> explored = new List<Position>();
        // Instance bitmapování
        public Bitmapping bitMap = new Bitmapping();     
        private int xCoordinates;
        private int yCoordinates;        
        private int xFinish;
        private int yFinish;
        /// <summary>
        /// Hlavní funkce pro chod programu 
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="xFinish"></param>
        /// <param name="yFinish"></param>
        public void Go(int X, int Y, int xFinish, int yFinish)        
        {
            this.xFinish = xFinish;
            this.yFinish = yFinish;
            // Vygenerovani polohy pro startovní odhad vzdálenosti            
            Position startPositionForPrice = new Position(X, Y, Position.RotationDirections.South);
            // Vytvoření prvního prvku ve fringe
            Position startPosition = new Position(X, Y, ReturnPriceOfPosition(startPositionForPrice), Position.RotationDirections.South, xFinish, yFinish);
            // Přidání "startovaci" do fringe
            fringe.Add(startPosition);
            // Zaručí projití všech prvků
            for (int c = 0; c < bitMap.LabyrinthZerosAndOnes.Length; c++)              
            {
                // Generace dalších poloh okolo té aktuální
                AddNearbyMatrixToFringe(fringe[0]);                  
                explored.Add(fringe[0]);
                fringe.RemoveAt(0);
                // Vytvoření pomocné "orderPosition" ve které jsou srovnané prvky z fringe
                IEnumerable<Position> orderPosition = fringe.OrderBy(a => a.Price);                                
                fringe = new List<Position>();
                // Uložení všech seřazených prvků zpět do fringe
                foreach (Position position in orderPosition)
                    fringe.Add(position);
                // Ověření, jestli program došel k cíly
                CheckOptions();
                // Zde ukládáme do matice pro zobrazení hodnoty ceny z fringe[0]. Tři if jsou použity pouze kvůli zarovnání tabulky, aby se vizuálně neposunovala
                if (fringe[0].Price >= 100)
                    bitMap.RendredMatrix[fringe[0].actualXY[0], fringe[0].actualXY[1]] = fringe[0].Price.ToString();               
                if (fringe[0].Price < 100 && fringe[0].Price > 9)
                    bitMap.RendredMatrix[fringe[0].actualXY[0], fringe[0].actualXY[1]] = fringe[0].Price.ToString() + " ";
                if (fringe[0].Price < 10)
                    bitMap.RendredMatrix[fringe[0].actualXY[0], fringe[0].actualXY[1]] = fringe[0].Price.ToString() + "  ";
                // Vymazání obsahu konzole
                Console.Clear();
                // Vykreslaní zobrazovací tabulky do konzole
                bitMap.ViewMyGameBoard();
                // Vyčkání 0,2s aby uživatel viděl aktuální polohu
                Thread.Sleep(200);                              
            }
            Console.ReadKey();
        }
        
        /// <summary>
        /// Podle směru natočení agenta volá funkci pro přidání okolních míst do fringe
        /// </summary>
        /// <param name="actualPosition"></param>
        public void AddNearbyMatrixToFringe(Position actualPosition)
        {
            int x = actualPosition.actualXY[0];
            int y = actualPosition.actualXY[1];
            // Pokud je směr natočení jih:
            if (actualPosition.directionOfRotation == Position.RotationDirections.South)                                            
                GenerateNerbyPosition(x, y, 2, 0, 1, 1, actualPosition);
            else if (actualPosition.directionOfRotation == Position.RotationDirections.North)
                GenerateNerbyPosition(x, y, 0, 2, 1, 1, actualPosition);
            else if (actualPosition.directionOfRotation == Position.RotationDirections.East)
                GenerateNerbyPosition(x, y, 1, 1, 0, 2, actualPosition);
            else if (actualPosition.directionOfRotation == Position.RotationDirections.West)
                GenerateNerbyPosition(x, y, 1, 1, 2, 0, actualPosition);
            else
            {
                Console.WriteLine("Problém s generací poloh.");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Funkce generuje až čtyři okolní pozice
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="northPositionPrice"></param>
        /// <param name="southPositionPrice"></param>
        /// <param name="eastPositionPrice"></param>
        /// <param name="westPositionPrice"></param>
        /// <param name="actualPosition"></param>
        private void GenerateNerbyPosition(int x, int y, int northPositionPrice, int southPositionPrice, int eastPositionPrice, int westPositionPrice, Position actualPosition)
        {
            // Prostor na sever
            x--;                                                                        
            GenerateNewPosition(x, y, northPositionPrice, actualPosition, Position.RotationDirections.North);
            x += 2;                                                                       
            GenerateNewPosition(x, y, southPositionPrice, actualPosition, Position.RotationDirections.South);
            x--;
            y++;
            // Prostor na západ
            GenerateNewPosition(x, y, eastPositionPrice, actualPosition, Position.RotationDirections.East);
            y -= 2;
            GenerateNewPosition(x, y, westPositionPrice, actualPosition, Position.RotationDirections.West);
        }

        /// <summary>
        /// Funkce generuje jednu konkrétní pozici
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="morePrice"></param>
        /// <param name="actualPosition"></param>
        /// <param name="smer"></param>
        private void GenerateNewPosition(int x, int y, int morePrice, Position actualPosition, Position.RotationDirections smer)
        {
            // Kontrola, jestli nejsme ve zdi
            if (bitMap.LabyrinthZerosAndOnes[x, y] == 0)             
            {
                // Generace polohy pro zjištění ceny
                Position pricePosition = new Position(x, y);                                                 
                int a = ReturnPriceOfPosition(pricePosition) + actualPosition.Price - ReturnPriceOfPosition(actualPosition) + 3;                     
                // PřidáníUrčení ceny konkrétního pole za natočení
                a += morePrice;
                // Vytvoření nové polohy
                Position newPosition = new Position(x, y, a, smer, xFinish, yFinish);
                // Zkontroluje duplicitu s fringe a explored, v případě neexistence dupliciti přidá tuto polohu do fringe                                   
                CheckDuplicatesAndGeneratePositions(newPosition);                               
            }
        }

        /// <summary>
        /// CheckOptions() slouží ke kontrole, jestli není fringe prázdný, pokud se fringe vyprázdní, tak nelze nalézt cílový bod, v našem případě to znamená, že cíl je ze startu nedostupný
        /// </summary>
        private void CheckOptions()
        {
            if (fringe.Count == 0)
            {
                Console.WriteLine(" Nelze se dostat k cíly, skončil jsem na pozici {0}-x,  {1}-y.", xCoordinates, yCoordinates);
                Console.ReadKey();
            }
        }

        /// <summary>
        ///  Vezme si polohu v bludišti, a vrátí její cenu, která je zavislá na vzdálenosti
        /// </summary>
        /// <param name="aktual"></param>
        /// <returns></returns>
        public int ReturnPriceOfPosition(Position aktual)
        {
            // *2 je použito k nastavení heuristické funkce - lze měnit
            return (Math.Abs(aktual.actualXY[0] - xFinish) + Math.Abs(aktual.actualXY[1] - yFinish)) * 2;           
        }

        /// <summary>
        /// CheckDuplicatesAndGeneratePositions vezme nově vygenerovaný prvek, porovná ho s fringe a explored a je-li unikátní, tak ho přidá do fringe.
        /// Také kontroluje shodu s cílovým stavem
        /// </summary>
        /// <param name="a"></param>
        private void CheckDuplicatesAndGeneratePositions(Position position)
        {
            // Povolení k naplnění fringe
            bool napln = true;                                         
            xCoordinates = position.actualXY[0];
            yCoordinates = position.actualXY[1];
            foreach (Position p in explored)
            {
                if (p.actualXY[0] == xCoordinates && p.actualXY[1] == yCoordinates)
                    napln = false;
            }
            foreach (Position d in fringe)
            {
                if (d.actualXY[0] == xCoordinates && d.actualXY[1] == yCoordinates)
                    napln = false;
            }
            if (napln)
                fringe.Add(position);
            if ((position.actualXY[0] == position.stopXY[0]) && (position.actualXY[1] == position.stopXY[1]))                    
                // Je-li poloha aktuálního prvku shodná s cílovou, tak vypíše cílovou hlášku a zastaví chod
            {
                Console.WriteLine(" Našel jsem cíl na pozici {0}-x,  {1}-y", xCoordinates, yCoordinates);
                Console.ReadKey();
            }
        }
    }
}
