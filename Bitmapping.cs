using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UI_Projekt_AstarAlgoritmus
{
    class Bitmapping
    {
        public Bitmap BitmapField { get; private set; }
        public int[,] LabyrinthZerosAndOnes { get; set; }
        public string[,] RendredMatrix { get; set; }
        
        /// <summary>
        /// Konstruktor pro třídu, která slouží k bitmapování
        /// </summary>
        public Bitmapping()
        {
            BitmapField = new Bitmap(@"d:\MAZE.bmp");
            FillInLabyrinthZerosAndOnes();
            GenerateRendredMatrix();
        }
        
        /// <summary>
        /// Naplní LabyrinthZerosAndOnes z obrázku
        /// </summary>
        public void GenerateRendredMatrix()
        {
            RendredMatrix = new string[BitmapField.Width, BitmapField.Height];
            for (int a = 0; a < BitmapField.Height; a++)
            {
                for (int b = 0; b < BitmapField.Height; b++)
                {
                    if (LabyrinthZerosAndOnes[b, a] == 0)
                        RendredMatrix[b, a] = "   ";
                    else
                        RendredMatrix[b, a] = "###";
                }
            }
        }
        
        /// <summary>
        /// Naplní LabyrinthZerosAndOnes nulami a jedničkami podle bitmapového obrázku.
        /// </summary>
        private void FillInLabyrinthZerosAndOnes()
        {
            LabyrinthZerosAndOnes = new int[BitmapField.Width, BitmapField.Height];
            for (int i = 0; i < BitmapField.Height; i++)
            {
                for (int j = 0; j < BitmapField.Width; j++)
                {
                    Color a = BitmapField.GetPixel(j, i);
                    // Stavy, po kterých se nedá chodit - čená políčka
                    if (a.A == 255 && a.R == 0 && a.B == 0 && a.R == 0) 
                    {
                        LabyrinthZerosAndOnes[j, i] = 1;
                    }
                    // Stavy, po kterých se dá chodit - bílá políčka
                    else if (a.A == 255 && a.R == 255 && a.B == 255 && a.R == 255)
                    {
                        LabyrinthZerosAndOnes[j, i] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Zobrazí hrací plochu.
        /// </summary>
        public void ViewMyGameBoard()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" 0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  16  17  18  19  20  21  22  23  24  25  26  27  28  29  30  31  32");
            for (int a = 0; a < RendredMatrix.GetLength(1); a++)
            {
                for (int b = 0; b < RendredMatrix.GetLength(0); b++)
                {
                    if (RendredMatrix[b, a] == "###")
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("{0} ", RendredMatrix[b, a]);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.WriteLine("{0} ", a);
            }
        }
    }
}
