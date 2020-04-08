using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UIProjekt1
{
    class Bitmapovani
    {
        public List<int[]> UpraveneSloupceJsouPole = new List<int[]>();

        public Bitmap PoleZObrazku { get; private set; }
        public int[,] MojePlochaVygenerovanaZObrazku_0a1 { get; set; }
        public string[,] MaticeKZobrazeni { get; set; }
        
        // vlastnost jako ProslePozice, ktera bude obsahovat prosle pozice cesty hrace. Pokud se vrati, tak odebere posledni pozice a doplni pak
        // ty kam bude pokracovat

        public Bitmapovani()
        {
            PoleZObrazku = new Bitmap(@"d:\MAZE.bmp");
            NaplnMojePlochaVygenerovanaZObrazku_0a1();
            VygenerujMaticiProZobrazeniChodu();
        }
        /// <summary>
        /// Naplní MojePlochaVygenerovanaZObrazku_0a1 z obrázku
        /// </summary>
        public void VygenerujMaticiProZobrazeniChodu()
        {
            MaticeKZobrazeni = new string[PoleZObrazku.Width, PoleZObrazku.Height];
            for (int a = 0; a < PoleZObrazku.Height; a++)
            {
                for (int b = 0; b < PoleZObrazku.Height; b++)
                {
                    if (MojePlochaVygenerovanaZObrazku_0a1[b, a] == 0)
                        MaticeKZobrazeni[b, a] = "   ";
                    else
                        MaticeKZobrazeni[b, a] = "###";
                }
            }
        }

        private void NaplnMojePlochaVygenerovanaZObrazku_0a1()
        {
            MojePlochaVygenerovanaZObrazku_0a1 = new int[PoleZObrazku.Width, PoleZObrazku.Height];

            for (int i = 0; i < PoleZObrazku.Height; i++)
            {
                for (int j = 0; j < PoleZObrazku.Width; j++)
                {
                    Color a = PoleZObrazku.GetPixel(j, i);

                    if (a.A == 255 && a.R == 0 && a.B == 0 && a.R == 0) // Stavy, po ktery se neda chodit - cerna policka
                    {
                        MojePlochaVygenerovanaZObrazku_0a1[j, i] = 1;
                    }

                    else if (a.A == 255 && a.R == 255 && a.B == 255 && a.R == 255) // Stavy, po ktery se da chodit - bila policka
                    {
                        MojePlochaVygenerovanaZObrazku_0a1[j, i] = 0;
                    }
                }
            }

        }
        /// <summary>
        /// Zobrazi pole nul a jednicek
        /// </summary>
        public void ZobrazMojePlochaVygenerovanaZObrazku_0a1()
        {
            for (int i = 0; i < MojePlochaVygenerovanaZObrazku_0a1.GetLength(1); i++)
            {
                for (int j = 0; j < MojePlochaVygenerovanaZObrazku_0a1.GetLength(0); j++)
                {
                    Console.Write("{0} ", MojePlochaVygenerovanaZObrazku_0a1[j, i]);
                }
                Console.Write("\n");
            }
            Console.WriteLine("\n");
        }

        public void ZobrazMojeHraciPlocha()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" 0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  16  17  18  19  20  21  22  23  24  25  26  27  28  29  30  31  32");
            for (int a = 0; a < MaticeKZobrazeni.GetLength(1); a++)
            {               
                for (int b = 0; b < MaticeKZobrazeni.GetLength(0); b++)
                {
                    if(MaticeKZobrazeni[b,a] == "###")
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("{0} ", MaticeKZobrazeni[b, a]);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.WriteLine("{0} ", a  );
            }           
        }
    }
}
