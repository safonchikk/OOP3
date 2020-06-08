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
    public partial class FormLock : Form
    {
        string s;
        int c;
		GameForm game;
        public FormLock(GameForm g)
        {
            InitializeComponent();
            Gen();
			game = g;
            c = 10;
        }

        private void button_Click(object sender, EventArgs e)
        {
			int a = 0, b = 0;
			--c;
			string s1 = textBox.Text;
			label.Text += s1 + '\n';
			for (int i = 0; i < 4; ++i)
				if (s1.Length != 4 || s1.IndexOf(s1[i]) != i || s1[i] < '0' || s1[i] > '9')
				{
					label.Text += "Invalid\n";
                    if (c == 0)
                        End(0);
                    textBox.Text = "";
                    return;
				}
			for (int i = 0; i < 4; ++i)
				for (int j = 0; j < 4; ++j)
					if (s[i] == s1[j])
						if (i == j)
							++a;
						else
							++b;
			label.Text += $"{a}A{b}B\n";
            textBox.Text = "";
            if (a == 4)
                End(1);
            if (c == 0)
                End(0);
		}
		private void End(int n)
        {
            MessageForm mes = new MessageForm(game);
            if (n == 1)
            {
                --game.field.keyneed;
                mes.Say("Nailed it!", "Yessss");
                mes.Show();
            }
            else
            {
                mes.Say("Lockpick broke:(", "Noooo");
                mes.Show();
            }
            Close();
        }
        public void Gen()
        {
            Random rnd = new Random();
            s = "";
            for (int i = 0; i < 4; ++i)
            {
                string s1 = rnd.Next(0, 10).ToString();
                while (s.IndexOf(s1) != -1)
                    s1 = rnd.Next(0, 10).ToString();
                s += s1;
            }
        }
    }
}
