using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;




namespace MineSweeper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

		public int tileSize = 100;
		public Color[] numColours = new Color[8];

		private int gridHeight;
		private int gridWidth;
		private bool gameStarted = false;
		private bool gameOver = false;

		private MineGrid mineGrid;

        public MainPage()
        {
            this.InitializeComponent();
        }

		private void StartNewGame(object sender, RoutedEventArgs e)
		{
			//fill the MainGrid with a bunch of tiles
			gameStarted = false;
			MainGrid.Background = new SolidColorBrush(Color.FromArgb(255, 48, 179, 221));
			MainGrid.Items.Clear();
			gridHeight = Convert.ToInt32(Height.Text);
			gridWidth = Convert.ToInt32(Width.Text);
			MainGrid.Height = gridHeight * tileSize;
			MainGrid.Width = gridWidth * tileSize;

			for (int y = 0; y < gridHeight; y++)
			{
				for (int x = 0; x < gridWidth; x++)
				{
					Button newTile = new Button();
					newTile.Height = tileSize;
					newTile.Width = tileSize;
					newTile.Click += new RoutedEventHandler(BreakTile);
					newTile.Name = x.ToString() + "," + y.ToString();
					MainGrid.Items.Add(newTile);
				}
			}
			
		}

		private void BreakTile(object sender, RoutedEventArgs e)
		{
			Button thisButton = sender as Button;
			int thisXPos = Convert.ToInt32(thisButton.Name.Substring(0, thisButton.Name.IndexOf(',')));
			int thisYPos = Convert.ToInt32(thisButton.Name.Substring(2, 1));
			
			if (!gameOver)
			{
				thisButton.Background = new SolidColorBrush(Color.FromArgb(255, 80, 120, 120));
				if (gameStarted)
				{
					Tiles thisTile = mineGrid.tiles[thisXPos + thisYPos * mineGrid.gridWidth];
					if (thisTile.hasBomb)
					{
						thisButton.Content = thisTile.hasBomb.ToString();
						GameOver();
					}
					else
					{
						CheckHowManyBombsNear(thisTile);
						thisButton.Content = thisTile.howManyBombsNear.ToString();
					}
				}
				else
				{
					//after first tile click add a set number of bombs randomly placed
					makeMineGrid();
					//make it so if you clicked a tile with no bombs near it, it automatically clicks all the ones with no bombs until surrounded by ones with bombs near
					Tiles thisTile = mineGrid.tiles[thisXPos + thisYPos * mineGrid.gridWidth];
					CheckHowManyBombsNear(thisTile);
					thisButton.Content = thisTile.howManyBombsNear.ToString();
					//if bomb near then make that tile show how many bombs near it with a designated colour

					gameStarted = true;
				}
			}
			thisButton.Click -= new RoutedEventHandler(BreakTile);
		}

		private void makeMineGrid()
		{
			mineGrid = new MineGrid(gridHeight, gridWidth, 10);
		}

		private void GameOver()
		{
			//reveal all bomb locations
			for (int y = 0; y < gridHeight; y++)
			{
				for (int x = 0; x < gridWidth; x++)
				{
					if (mineGrid.tiles[x + y * gridHeight].hasBomb)
					{
						Button button = MainGrid.Items[x + y * gridHeight] as Button;
						button.Content = mineGrid.tiles[x + y * gridHeight].hasBomb.ToString();
					}
				}
			}

			gameOver = true;
		}

		private void CheckHowManyBombsNear(Tiles tile)
		{
			int howManyBombsNearThis = 0;
			if (tile.xPos + 1 < mineGrid.gridWidth)
			{
				if (tile.yPos + 1 < mineGrid.gridHeight)
				{
					if (mineGrid.tiles[(tile.xPos + 1) + (tile.yPos + 1) * mineGrid.gridHeight].hasBomb)
					{
						howManyBombsNearThis++;
					}
				}
				if (tile.yPos - 1 >= 0)
				{
					if (mineGrid.tiles[(tile.xPos + 1) + (tile.yPos - 1) * mineGrid.gridHeight].hasBomb)
					{
						howManyBombsNearThis++;
					}
				}

				if (mineGrid.tiles[(tile.xPos + 1) + (tile.yPos) * mineGrid.gridHeight].hasBomb)
				{
					howManyBombsNearThis++;
				}

			}
			if (tile.xPos - 1 >= 0)
			{
				if (tile.yPos + 1 < mineGrid.gridHeight)
				{
					if (mineGrid.tiles[(tile.xPos - 1) + (tile.yPos + 1) * mineGrid.gridHeight].hasBomb)
					{
						howManyBombsNearThis++;
					}
				}
				if (tile.yPos - 1 >= 0)
				{
					if (mineGrid.tiles[(tile.xPos - 1) + (tile.yPos - 1) * mineGrid.gridHeight].hasBomb)
					{
						howManyBombsNearThis++;
					}
				}

				if (mineGrid.tiles[(tile.xPos - 1) + (tile.yPos) * mineGrid.gridHeight].hasBomb)
				{
					howManyBombsNearThis++;
				}

			}
			if (tile.yPos + 1 < mineGrid.gridHeight)
			{
				if (mineGrid.tiles[(tile.xPos) + (tile.yPos + 1) * mineGrid.gridHeight].hasBomb)
				{
					howManyBombsNearThis++;
				}
			}
			if (tile.yPos - 1 >= 0)
			{
				if (mineGrid.tiles[(tile.xPos) + (tile.yPos - 1) * mineGrid.gridHeight].hasBomb)
				{
					howManyBombsNearThis++;
				}
			}
			tile.howManyBombsNear = howManyBombsNearThis;
		}
	}
}
