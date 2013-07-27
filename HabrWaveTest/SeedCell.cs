using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabrWaveTest
{
    class SeedCell
    {
        private enum Direction { Main, Up, Down, Exausted }
        private Direction spawnDirection;
        private bool isNew = true;

        private int X;
        private int Y;

        private ConsoleColor waveColor;
        public int waveID { get; private set; }

        public static bool operator >(SeedCell item1, SeedCell item2)
        {
            return item1.waveID > item2.waveID;
        }
        public static bool operator <(SeedCell item1, SeedCell item2)
        {
            return item1.waveID <= item2.waveID;
        }

        public SeedCell(int x, int y)
        {
            X = x;
            Y = y;
            waveColor = ConsoleColor.Black;
            spawnDirection = Direction.Exausted;
            waveID = 0;
        }

        /*
        private SeedCell(int x, int y, ConsoleColor c, int ID, Direction d)
        {
            X = x;
            Y = y;
            color = c;
            spawnDirection = Direction.Main;
            waveID = ID;
            spawnDirection = d;
        }
         */

        private void Replace(ConsoleColor c, int ID, Direction d)
        {
            waveColor = c;
            spawnDirection = Direction.Main;
            waveID = ID;
            spawnDirection = d;
            isNew = true;
        }

        public void Start(ConsoleColor c, int ID)
        {
            waveColor = c;
            spawnDirection = Direction.Main;
            waveID = ID;
            isNew = true;
        }

        private void Spawn(SeedCell[,] board, Direction d)
        {
            int x = X;
            int y = Y;
            if (d == Direction.Main)
                x++;
            else if (d == Direction.Up)
                y--;
            else
                y++;

            try
            {
                if (this > board[x, y])
                    board[x, y].Replace(waveColor, waveID, d);
            }
            catch (IndexOutOfRangeException) { }
        }

        public void SpawnWave(SeedCell[,] board)
        {
            if (!isNew && spawnDirection != Direction.Exausted)
            {
                switch (spawnDirection)
                {
                    case Direction.Exausted:
                        return;
                    case Direction.Main:
                        Spawn(board, Direction.Main);
                        Spawn(board, Direction.Up);
                        Spawn(board, Direction.Down);
                        break;
                    default:
                        Spawn(board, this.spawnDirection);
                        break;
                }
                spawnDirection = Direction.Exausted;
            }
        }

        public void Draw()
        {
            if (waveID != 0)
            {
                Console.SetCursorPosition(X, Y);
                Console.BackgroundColor = waveColor;
                Console.Write(" ");
                isNew = false;
            }
        }

    }
}
