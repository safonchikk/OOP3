using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP3
{
    public partial class LoginForm : Form
    {
        MessageForm mes;
        public LoginForm()
        {
            InitializeComponent();
            mes = new MessageForm(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Player player = new Player(login.Text, pass.Text);
            int i = FileHandler.Read(player);
            if (i == 0)
            {
                mes.Say("Liar!", "I'll try again");
                mes.Show();
                Hide();
                return;
            }
            MenuForm f = new MenuForm(player);
            f.Show();
            this.Hide();
        }
    }
}
