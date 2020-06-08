using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP3
{
    public partial class MenuForm : Form
    {
        public Player player;
        public MenuForm(Player p)
        {
            InitializeComponent();
            player = p;
        }

        private void Form1_Load(object sender, EventArgs e){}

        private void button4_Click(object sender, EventArgs e)
        {
            FileHandler.Write(player);
            Environment.Exit(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShopForm shop = new ShopForm(this);
            shop.Show();
        }        

        private void button2_Click(object sender, EventArgs e)
        {
            CreditsForm cr = new CreditsForm(this);
            cr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameForm g = new GameForm(player, this);
            g.Show();
            Hide();
        }
    }
}
