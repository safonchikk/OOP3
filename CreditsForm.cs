using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace OOP3
{
    public partial class CreditsForm : Form
    {
        MenuForm menu;
        public CreditsForm(MenuForm m)
        {
            menu = m;
            m.Hide();
            InitializeComponent();
            M();
        }
        private void M()
        {
            CheckForIllegalCrossThreadCalls = false;
            new Thread(() =>
            {
                while (true)
                {
                    Shift(label1);
                    Shift(label2);
                    Shift(label3);
                    Shift(label4);
                    Shift(label5);
                    Shift(label6);
                    Shift(label7);
                    Thread.Sleep(40);
                }
            }).Start();
        }
        void Shift(Label l)
        {
            l.Location = new Point(l.Location.X, l.Location.Y - 3);
            if (l.Location.Y < -5)
                l.Location = new Point(l.Location.X, this.Height - 5);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            menu.Show();
            Close();
        }
    }
}
