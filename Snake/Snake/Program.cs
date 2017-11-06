using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Snake
{
    struct Position
    {
        public int row;
        public int col;
        public string a;

        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
            Random rand = new Random();     
            this.a = "";
        }

        public Position(int row, int col, string a)
        {
            this.row = row;
            this.col = col;           
            Random rand = new Random();
            this.a = a;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Position[] directions = new Position[]
            {
                new Position(0,1),     //right
                new Position(0,-1),    //left
                new Position(1,0),      //down
                new Position(-1,0),    //top
            };

            int direction = 0; //0

            Random randomNumberGenerator = new Random();
            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight;

            Position food = new Position(randomNumberGenerator.Next(0, Console.WindowHeight), randomNumberGenerator.Next(0, Console.WindowWidth));

            string eaten = "";

            Queue<Position> Snake = new Queue<Position>();
            List<string> snake = new List<string>();

            string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string a = Alphabet[randomNumberGenerator.Next(0, Alphabet.Length)];

            Snake.Enqueue(new Position(0, 0));
            snake.Add("_");

            while (true)
            {
                Console.Clear();

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        direction = 1;
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        direction = 0;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        direction = 3;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        direction = 2;
                    }
                }

                Position snakeHead = Snake.Last();
                Position nextDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);
                Snake.Enqueue(snakeNewHead);
               
                if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                {
                    snake.Remove("_");
                    Snake.Dequeue();
                    eaten = a;
                    snake.Add(eaten);
            
                    food = new Position(randomNumberGenerator.Next(0, Console.WindowHeight),randomNumberGenerator.Next(0, Console.WindowWidth));

                    a = Alphabet[randomNumberGenerator.Next(0, Alphabet.Length)];
                }
                else
                {
                    Snake.Dequeue();
                }
                
                foreach (Position position in Snake)
                {
                    Console.SetCursorPosition(position.col, position.row);
                    
                    foreach (var item in snake)
                    {
                        Console.Write(item);
                    }
                }
                       
                //HRANATA
                Console.SetCursorPosition(food.col, food.row);
                Console.Write(a);

                Thread.Sleep(100);
            }
        }
    }
}