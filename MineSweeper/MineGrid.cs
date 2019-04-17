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

		private Random random;

		public MineGrid(int _gridHeight, int _gridWidth, int _howManyBombs)
		{
			tiles = new List<Tiles>();
			gridHeight = _gridHeight;
			gridWidth = _gridWidth;
			howManyBombs = _howManyBombs;
			random = new Random();
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
			for (int rand = 0; rand < howManyBombs; rand++)
			{
				int randomX = random.Next(0, gridWidth);
				int randomY = random.Next(0, gridHeight);
				int index = randomX + randomY * gridHeight;
				if (!tiles[index].hasBomb)
				{
					tiles[index].hasBomb = true;
				}
				else
				{
					rand--;
				}
			}
		}
    }
}
