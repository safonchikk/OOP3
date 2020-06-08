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
    public partial class MessageForm : Form
    {
        public Form par;
        public MessageForm(Form f)
        {
            InitializeComponent();
            par = f;
        }
        public void Say(string lab, string but)
        {
            label.Text = lab;
            button.Text = but;
        }
        private void button_Click(object sender, EventArgs e)
        {
            par.Show();
            Hide();
        }
    }
}
