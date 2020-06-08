using System;
using System.IO;

namespace OOP3
{
    public static class FileHandler
    {
        public static int Read(Player player)
        {
            string path = @"..\players\" + player.nick + ".txt";
            if (!File.Exists(path))
            {
                player.Death();
                Write(player);
            }
            using (StreamReader ppp = new StreamReader(path))
            {
                string g;
                g = ppp.ReadLine();
                if (player.pass != g)
                    return 0;
                g = ppp.ReadLine();
                player.lvl = int.Parse(g);
                g = ppp.ReadLine();
                player.lives = int.Parse(g);
                g = ppp.ReadLine();
                player.coins = int.Parse(g);
                g = ppp.ReadLine();
                for (int i = 0; i < 4; ++i)
                {
                    player[i] = int.Parse(g);
                    g = ppp.ReadLine();
                }
            }
            return 1;
        }
        public static void Write(Player player)
        {
            string path = @"..\players\" + player.nick + ".txt";
            using (StreamWriter ppp = new StreamWriter(path, false))
            {
                ppp.Write($"{player.pass}\n{player.lvl}\n{player.lives}\n{player.coins}\n{player[0]}\n{player[1]}\n{player[2]}\n{player[3]}");
            }
        }
    }
}