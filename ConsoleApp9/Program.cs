using System;
using System.Collections.Generic;
using System.Configuration;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    internal class Program
    {
        
        static volatile bool exit = false;
        static void Main(string[] args)
        {
            double alpha = Math.PI / 1800d;
            double radius = 5d;
            double fov = 2* Math.PI / 3;

            int wallsize = 3;
           
            Player player = new Player(6, 3 , Math.PI/3d);

            string[] map = { "*****#**********",
                             "****************",
                             "**#*************",
                             "****************",
                             "****************",
                             "****************",
                             "****************",
                             "################",
                             "****************",
                             "****************",
                             "****************",
                             "****************",
                             "****************",
                             "****************",
                             "****************",
                             "****************"
                            };
            

            Task.Factory.StartNew(() => {
                ConsoleKeyInfo key = Console.ReadKey();

                while (key.Key != ConsoleKey.Escape) 
                {
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.LeftArrow) player.rotation += Math.PI / 180d;
                    if (key.Key == ConsoleKey.RightArrow) player.rotation -= Math.PI / 180d;

                }
                exit = true;
            });


            while (!exit)
            {
                for (double i = player.rotation; i <= (fov + player.rotation); i += alpha)
                {
                    for (double j = 0; j <= radius; j+= 0.1d)
                    {
                        if (CheckWall(Math.Cos(i) * j, Math.Sin(i) * j, ref player, ref map)) WallDraw(i, j, wallsize, fov);
                    }
                }
                Thread.Sleep(TimeSpan.FromSeconds(1/60));
                Console.Clear();
            }
        }

        private static void WallDraw(double i, double j, int wall, double fov)
        {
            int wallsize = (int)Math.Round(Math.Tan(fov/2)*j * wall, 0);
            try
            {
                for (int k = 0; k < wallsize; k++)
                {

                    int left = (int)Math.Abs(Math.Round(Console.WindowWidth * i / fov, 0));
                    int top = (int)Math.Abs(Math.Round(Console.WindowHeight / 2d + wallsize / 2d - k, 0));
                    Console.SetCursorPosition( left, top);
                    Console.Write("#");
                }           
            }
            catch { }
        }

        static internal bool CheckWall(double x, double y,ref Player player, ref string[] map) 
        {
            
            int iY = (int)Math.Round(y, 0);
            int iX = (int)Math.Round(x, 0);
            try
            {
                return map[iY + player.y][iX + player.x] == '#';
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}

