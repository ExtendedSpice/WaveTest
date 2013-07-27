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

        // Конструктор пустой черной клетки
        public SeedCell(int x, int y)
        {
            X = x;
            Y = y;
            waveColor = ConsoleColor.Black;
            spawnDirection = Direction.Exausted;
            waveID = 0;
        }

        // Гарантированное создание нового спавнера 
        public void Start(ConsoleColor c, int ID)
        {
            waveColor = c;
            spawnDirection = Direction.Main;
            waveID = ID;
            isNew = true;
        }

        // Распространение волны за один такт
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

        // Механизм распространения фронта в зависимости от направление d
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
                    board[x, y].Update(waveColor, waveID, d);
            }
            catch (IndexOutOfRangeException) { }
        }

        // Перекрашивание клетки
        private void Update(ConsoleColor c, int ID, Direction d)
        {
            waveColor = c;
            waveID = ID;
            spawnDirection = d;
            isNew = true;
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
