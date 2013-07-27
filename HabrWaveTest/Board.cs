using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabrWaveTest
{
    class Board
    {
        private int bLength;
        private int bWidth;
        private SeedCell[,] board;

        //private System.Windows.Forms.Timer timer;

        private int currID;
        private Random rnd;

        public Board(int l, int w)
        {
            bLength = l;
            bWidth = w;
            currID = 1;
            board = new SeedCell[l, w];
            for (int i = 0; i < l; i++)
                for (int j = 0; j < w; j++)
                    board[i, j] = new SeedCell(i, j);

            //timer = new System.Windows.Forms.Timer();
            //timer.Tick += timer_Tick;
            Console.CursorVisible = false;
            rnd = new Random();
        }

        //void timer_Tick(object sender, EventArgs e)
        private void timer_Tick()
        {
            foreach (var item in board)
            {
                item.SpawnWave(board);
            }

            foreach (var item in board)
            {
                item.Draw();
            }
            //Console.Clear();
        }

        private void Click()
        {
            Array values = Enum.GetValues(typeof(ConsoleColor));
            board[0, rnd.Next(0, bWidth)].Start((ConsoleColor)Enum.GetValues(typeof(ConsoleColor)).GetValue(rnd.Next(values.Length)), currID);
            currID++;
        }

        public void Start(int period)
        {
            /*
            if (timer.Enabled)
                timer.Stop();
            timer.Interval = period;
            timer.Start();
             */
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    if(Console.ReadKey(true).Key == ConsoleKey.Escape)
                        return;
                    Click();
                }
                timer_Tick();
                System.Threading.Thread.Sleep(period);
            }
        }        
    }
}
