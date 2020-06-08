using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OOP3
{
    public class Dot
    {
        public int x, y;
        public Dot (int i, int j)
        {
            x = i;
            y = j;
        }
    }
    public class Cell: Dot
    {
        public char cond, content;
        public PictureBox pic;
        public Cell(int i, int j, Panel p, char content = ' ', char cond = 'i') : base(i, j)
        {
            this.content = content;
            this.cond = cond;
            pic = new PictureBox();
            pic.Location = new Point(64 * i, 64 * j);
            pic.Parent = p;
            pic.Width = 64;
            pic.Height = 64;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void Display()
        {
            switch(cond)
            {
                case 'i':
                    pic.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\textures\1.jpg");
                    break;
                case 'f':
                    pic.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\textures\2.jpg");
                    break;
                default:
                    if (content == 's')
                        pic.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\textures\4.jpg");
                    else if (content == 'L')
                        pic.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\textures\8.jpg");
                    else if (content == 'D')
                        pic.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\textures\9.jpg");
                    else if (content == 'k')
                        pic.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\textures\5.jpg");
                    else if (content == ' ')
                        pic.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\textures\3.jpg");
                    break;
            }
        }
        public void Update()
        {
            if (cond == 'i')
                cond = 'v';
            Display();
        }
        public int UpdateByPlayer()
        {
            if (content == 's')
                return 5;
            if (cond == 'f' || cond == 'v')
                return 0;
            cond = 'v';
            if (content == 'L')
                return 1;
            if (content == 'k')
                return 3;
            if (content == 'D')
                return 4;
            return 2;
        }
    }
}