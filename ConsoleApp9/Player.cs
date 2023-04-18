using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    internal class Player
    {
        public int x = 0;
        public int y = 0;
        public double rotation = 2 * Math.PI;
        public Player(int x, int y) { this.x = x; this.y = y; }
        public Player(int x, int y, double rotation) { this.x = x; this.y = y; this.rotation = rotation; }
        public Player() { }
    }
}
