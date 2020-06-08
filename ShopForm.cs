using System;
using System.Windows.Forms;

namespace OOP3
{
    public partial class ShopForm : Form
    {
        Player p;
        MenuForm menu;
        public ShopForm(MenuForm m)
        {
            menu = m;
            m.Hide();
            InitializeComponent();
            p = menu.player;
        }

        private void ShopForm_Load(object sender, EventArgs e)
        {
            button0.Text = p.items[0].ToString(2);
            button1.Text = p.items[1].ToString(2);
            button2.Text = p.items[2].ToString(2);
            button3.Text = p.items[3].ToString(2);
            buttonlife.Text = "New body: 50 ₿";
            Reload();
        }
        private void Reload()
        {
            label0.Text = p.items[0].quantity.ToString();
            label1.Text = p.items[1].quantity.ToString();
            label2.Text = p.items[2].quantity.ToString();
            label3.Text = p.items[3].quantity.ToString();
            labellife.Text = p.lives.ToString();
            labelcoins.Text = p.coins.ToString() + " ₿";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (p.coins >= p.items[0].price)
            {
                ++p[0];
                p.coins -= p.items[0].price;
                Reload();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (p.coins >= p.items[1].price)
            {
                ++p[1];
                p.coins -= p.items[1].price;
                Reload();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (p.coins >= p.items[2].price)
            {
                ++p[2];
                p.coins -= p.items[2].price;
                Reload();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (p.coins >= p.items[3].price)
            {
                ++p[3];
                p.coins -= p.items[3].price;
                Reload();
            }
        }

        private void buttonlife_Click(object sender, EventArgs e) { 
            if (p.coins >= 50)
            {
                ++p.lives;
                p.coins -= 50;
                Reload();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            menu.Show();
            Close();
        }
    }
}
