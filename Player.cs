using System;
using System.Drawing;
using System.IO;

namespace OOP3{
    public class Player
    {
        public int x, y, lives, coins, lvl, coinsforlvl, keys;
        public char dir = 'v';
        public Item[] items;
        public string nick, pass;
        public Player(string n, string p, int x = 1, int y = 1, int lives = 3)
        {
            nick = n;
            pass = p;
            this.x = x;
            this.y = y;
            this.lives = lives;
            items = new Item[4];
            items[0] = new Item("Stone");
            items[1] = new Item("Lockpick");
            items[2] = new Item("Keylight", 30);
            items[3] = new Item("Doorlight", 50);
        }
        public int this[int i]
        {
            get { return items[i].quantity; }
            set { items[i].quantity = value; }
        }
        public Image Display(int n = 1)
        {
            if (n == 0)
                return Image.FromFile(Directory.GetCurrentDirectory() + @"\textures\6.jpg");
            return Image.FromFile(Directory.GetCurrentDirectory() + @"\textures\7.jpg");
        }
        public void Move(int i = 0, int j = 0)
        {
            if (x + i >= 0 && x + i < 15) 
            { 
                x += i;
                if (i > 0)
                    dir = '>';
                if (i < 0)
                    dir = '<';
            }
            if (y + j >= 0 && y + j < 15) 
            { 
                y += j;
                if (j > 0)
                    dir = 'v';
                if (j < 0)
                    dir = '^';
            }
        }

        public void Death()
        {
            lives = 3;
            lvl = 1;
            coins = 0;
            for (int i = 0; i < 4; ++i)
                items[i].quantity = 0;
        }

    }
}
