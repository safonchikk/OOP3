using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace OOP3
{
    public partial class GameForm : Form
    {
        Player player;
        MenuForm menu;
        public Field field;
        MessageForm mes;
        public GameForm(Player p, MenuForm m)
        {
            InitializeComponent();
            menu = m;
            player = p;
            mes = new MessageForm(this);
        }
        private void Say(string lab, string but)
        {
            mes.Say(lab, but);
            mes.Show();
            Hide();
        }
        private void GameForm_Load(object sender, EventArgs e)
        {
            FieldBuild(player.lvl);
            Stats();
        }
        private void Stats()
        {
            labelinfo.Text = $"{field.AdjMines()} mines around\n" +
                $"Level {player.lvl}\n" +
                $"{player.coinsforlvl} ₿\n" +
                $"{player.lives} ♡\n" +
                $"{player.keys} 🔑\n";
            for (int i = 0; i < 4; ++i)
                labelinfo.Text += player.items[i].ToString() + '\n';
        }
        
        private void FieldBuild(int keyneed)
        {
            field = new Field(keyneed, player, panel1);
            field.Draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menu.Show();
            Close();
        }
        private Dot GetDir(KeyEventArgs k)
        {
            Dot d = new Dot(0, 0);
            switch (k.KeyCode)
            {
                case Keys.W:
                    d.y = -1;
                    break;
                case Keys.A:
                    d.x = -1;
                    break;
                case Keys.S:
                    d.y = 1;
                    break;
                case Keys.D:
                    d.x = 1;
                    break;
            }
            return d;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            int i, j;
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.A:
                case Keys.S:
                case Keys.D:
                    Dot d = GetDir(e);
                    i = d.x;
                    j = d.y;
                    if (ModifierKeys == Keys.None)
                    {
                        field[player.y, player.x].Update();
                        player.Move(i, j);
                        goto case 0;
                    }
                    if (ModifierKeys == Keys.Shift)
                    {
                        field[player.y, player.x].Update();
                        player.Move(2 * i, 2 * j);
                        goto case 0;
                    }
                    if (ModifierKeys == Keys.Control)
                    {
                        if (player[0] < 1)
                            break;
                        if (player.y + j > 0 && player.y + j < 14 && player.x + i > 0 && player.x + i < 14)
                        {
                            field[player.y + 2 * j, player.x + 2 * i].Update();
                            --player[0];
                        }
                    }
                    break;  
                case Keys.Enter:
                    int q = field.TryEscape();
                    if (q == 0)
                    {
                        mes.par = menu;
                        Say("You've done it, great job", "Yeeees!");
                        ++player.lvl;
                        Close();
                    }
                    if (q > 0)
                        Say((q > 1) ? $"{q} more locks" : "Just one more key", "Okay :(");
                    break;
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                    int k = Int32.Parse(e.KeyCode.ToString().Substring(6, 1));
                    i = (k+2) % 3 - 1;
                    j = (k-1) / 3 - 1;
                    if (field[player.y - j, player.x + i].cond == 'i')
                        field[player.y - j, player.x + i].cond = 'f';
                    else if (field[player.y - j, player.x + i].cond == 'f')
                        field[player.y - j, player.x + i].cond = 'i';
                    field[player.y - j, player.x + i].Display();
                    break;
                case Keys.Z:
                    if (player[2] < 1)
                        break;
                    --player[2];
                    Dot dot = field.keys.Light();
                    PictureBox pic = new PictureBox();
                    pic.Location = new Point(64 * dot.y, 64 * dot.x);
                    pic.Parent = panel1;
                    pic.Width = 64;
                    pic.Height = 64;
                    pic.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\textures\10.jpg");
                    pic.BringToFront();
                    CheckForIllegalCrossThreadCalls = false;
                    new Thread(() =>
                    {
                        Thread.Sleep(1000);
                        pic.Visible = false;
                    }).Start();
                    break;
                case Keys.X:
                    if (player[3] < 1)
                        break;
                    --player[3];
                    dot = field.doors.Light();
                    pic = new PictureBox();
                    pic.Location = new Point(64 * dot.y, 64 * dot.x);
                    pic.Parent = panel1;
                    pic.Width = 192;
                    pic.Height = 192;
                    pic.Image = Image.FromFile(Directory.GetCurrentDirectory() + @"\textures\10.jpg");
                    pic.BringToFront();
                    CheckForIllegalCrossThreadCalls = false;
                    new Thread(() =>
                    {
                        Thread.Sleep(1000);
                        pic.Visible = false;
                    }).Start();
                    break;
                case Keys.L:
                    if (field[player.y, player.x].content == 'D' && field[player.y, player.x].cond == 'v' && player[1] > 0)
                    {
                        --player[1];
                        FormLock f = new FormLock(this);
                        f.Show();
                        Hide();
                    }
                    break;
                case 0:
                        int h = field[player.y, player.x].UpdateByPlayer();
                        if (h == 1)
                        {
                            if (--player.lives == 0)
                            {
                                Say("You don't have new body, so?", "Time travel");
                                player.Death();
                                menu.Show();
                                Close();
                            }
                            else
                                Say("Boom!\nMost of your body is now pretty much meat pieces", "Take new one");
                        }
                        if (h == 2)
                            ++player.coinsforlvl;
                        if (h == 3)
                        {
                            field[player.y, player.x].Update();
                            Say("Oh, here's a key", "Cool");
                            ++player.keys;
                            ++player.coinsforlvl;
                            field[player.y, player.x].content = ' ';
                        }
                        if (h == 4)
                        {
                            Say("Oh, here's a door", "Nice");
                            ++player.coinsforlvl;
                        }
                        if (h == 5)
                        {
                            if (--player.lives == 0)
                            {
                                Say("There were spikes here, now you're dead", "Time travel");
                                player.Death();
                                menu.Show();
                                Close();
                            }
                            else
                                Say("These spikes are sharp and dangerous", "Ouch!");
                        }
                    break;
            }
            field.DrawPlayer();
            Stats();
        }
    }
}
