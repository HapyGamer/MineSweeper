using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class MineGrid
    {
		public int gridHeight;
		public int gridWidth;
		public int howManyBombs;

		public List<Tiles> tiles;

		public MineGrid(int _gridHeight, int _gridWidth, int _howManyBombs)
		{
			gridHeight = _gridHeight;
			gridWidth = _gridWidth;
			howManyBombs = _howManyBombs;
			for (int y = 0; y < gridHeight; y++)
			{
				for(int x = 0; x < gridWidth; x++)
				{
					var newTile = new Tiles();
					newTile.xPos = x;
					newTile.yPos = y;
					tiles.Add(newTile);
				}
			}
		}
    }
}
