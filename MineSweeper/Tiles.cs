using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public struct Tiles
    {
		public bool hasBomb;
		public int howManyBombsNear;
		public int xPos;
		public int yPos;
    }
}
