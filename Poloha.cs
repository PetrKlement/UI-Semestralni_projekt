using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProjekt1
{
    class Poloha
    {
        public int[] stopXY = { 31, 31 };  //od nuly
        public int[] aktualniXY = { 31, 31 };       
        public enum SmerNatoceni { Sever, Jih, Zapad, Vychod };
        public int cena { get; set; }
        public SmerNatoceni smernatoceni;

        public Poloha(int x, int y)
        {
            aktualniXY[0] = x;
            aktualniXY[1] = y;
        }
        public Poloha(int x, int y, int Cena, Poloha.SmerNatoceni smery, int xCil, int yCil)
        {
            aktualniXY[0] = x;
            aktualniXY[1] = y;
            cena = Cena;
            smernatoceni = smery;
            stopXY[0] = xCil;
            stopXY[1] = yCil;
        }
        public Poloha(int x, int y, Poloha.SmerNatoceni smery)
        {
            aktualniXY[0] = x;
            aktualniXY[1] = y;        
            smernatoceni = smery;
        }
        public Poloha()
        {          
        }
    }
}
