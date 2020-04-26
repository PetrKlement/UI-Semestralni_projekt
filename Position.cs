using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_Projekt_AstarAlgoritmus
{
    /// <summary>
    /// Poloha jednotlivích dostupných bodů v bludišti
    /// </summary>
    class Position
    {
        public int[] stopXY = { 31, 31 };  
        public int[] actualXY = { 31, 31 };
        public enum RotationDirections { North, South, West, East };
        public int Price { get; set; }
        public RotationDirections directionOfRotation;
        /// <summary>
        /// Pozice pro určení ceny
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Position(int x, int y)
        {
            actualXY[0] = x;
            actualXY[1] = y;
        }
        /// <summary>
        /// Plnohodnotná pozice
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="Price"></param>
        /// <param name="smery"></param>
        /// <param name="xFinish"></param>
        /// <param name="yFinish"></param>
        public Position(int x, int y, int Price, Position.RotationDirections smery, int xFinish, int yFinish)
        {
            actualXY[0] = x;
            actualXY[1] = y;
            this.Price = Price;
            directionOfRotation = smery;
            stopXY[0] = xFinish;
            stopXY[1] = yFinish;
        }
        public Position(int x, int y, Position.RotationDirections direction)
        {
            actualXY[0] = x;
            actualXY[1] = y;
            directionOfRotation = direction;
        }
    }
}
