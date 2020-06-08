using System;
using System.Drawing;
using System.Windows.Forms;

namespace OOP3
{
	public class Field
	{
		public Cell[,] f;
		public Doors doors;
		public Mines mines;
		public Keys_ keys;
		public int keyneed;
		public Player player;
		Random rnd = new Random();
		public Field(int lvl, Player p, Panel pan)
		{
			keyneed = rnd.Next(0, lvl / 3 + 2);
			f = new Cell[15, 15];
			for (int i = 0; i < 15; ++i)	
				for (int j = 0; j < 15; ++j)
					f[i, j] = new Cell(j, i, pan);
			player = p;
			player.coinsforlvl = 0;
			player.keys = 0;
			MinesGen();
			KeysGen();
			DoorsGen();
			SpikesGen();
			do
			{
				player.x = rnd.Next(0, 15);
				player.y = rnd.Next(0, 15);
			} while (this[player.y, player.x].content != ' ');
			this[player.y, player.x].UpdateByPlayer();
		}
		public int TryEscape()
        {
			if (this[player.y, player.x].content == 'D' && this[player.y, player.x].cond == 'v')
			{
				if (player.keys < keyneed)
				{
					keyneed -= player.keys;
					player.keys = 0;
					return keyneed;
				}
				else
				{
					++player.lvl;
					player.coins += player.coinsforlvl;
					player.keys = 0;
					return 0;
				}
			}
			return -1;
		}
		public void DrawPlayer()
        {
			if (this[player.y, player.x].cond == 'v' && 
				(this[player.y, player.x].content == ' ' || this[player.y, player.x].content == 'L'))
				this[player.y, player.x].pic.Image = player.Display();
			else
				this[player.y, player.x].pic.Image = player.Display(0);
			if (player.dir == '<')
				this[player.y, player.x].pic.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
			else if (player.dir == '^')
				this[player.y, player.x].pic.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
			else if (player.dir == '>')
				this[player.y, player.x].pic.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
		}
		public void DoorsGen()
		{
			while (true)
			{
				doors = new Doors();
				int i = rnd.Next(0, 15);
				int j = rnd.Next(0, 15);
				if (f[i, j].content == ' ')
				{
					f[i, j].content = 'D';
					doors.Push(new Dot(i, j));
					break;
				}
			}
		}
		public void MinesGen() {
			int c = 225;
			int n = rnd.Next(c / 10, c / 5);
			mines = new Mines(n);
			for (int i = 0; i < 15; ++i)
				for (int j = 0; j < 15; ++j, --c)
					if (rnd.Next(0, c) < n)
					{
						f[i, j].content = 'L';
						mines.Push(new Dot(i, j));
						--n;
					}
		}
		public void KeysGen()
		{
			int n = keyneed + 3;
			int c = 225 - mines.ar.Length;
			keys = new Keys_(n);
			for (int i = 0; i < 15; ++i)
				for (int j = 0; j < 15 && c > 0; ++j, --c)
					if (rnd.Next(0, c) < n && f[i, j].content == ' ')
					{
						f[i, j].content = 'k';
						keys.Push(new Dot(i, j));
						--n;
					}
		}
		public void SpikesGen()
		{
			int c = 225 - mines.ar.Length - keys.ar.Length;
			int n = rnd.Next(c / 20, c / 15);
			for (int i = 0; i < 15; ++i)
				for (int j = 0; j < 15 && c > 0; ++j, --c)
					if (rnd.Next(0, c) < n && f[i, j].content == ' ')
					{
						f[i, j].content = 's';
						f[i, j].cond = 'v';
						--n;
					}
		}
		public void Draw()
		{
			for (int i = 0; i < 15; ++i)
				for (int j = 0; j < 15; ++j)
					f[i, j].Display();
			DrawPlayer();
		}
		public int AdjMines()
		{
			int c = 0;
			for (int i = -1; i < 2 && player.y + i < 15 && player.y + i > -1; ++i)
				for (int j = -1; j < 2 && player.x + j < 15 && player.x + j > -1; ++j)
					if (f[player.y + i, player.x + j].content == 'L' && 
						f[player.y + i, player.x + j].cond != 'v' && Math.Abs(i) + Math.Abs(j) != 0)
						++c;
			return c;
		}
		public Cell this[int x, int y]
		{
			get
			{
				return f[x, y];
			}
			set
			{
				f[x, y] = value;
			}
		}
	}
}