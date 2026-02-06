using System;
using System.Collections.Generic;
using System.Text;

namespace GestionMemoire
{
    public class Lambda
    {
        public Func<int, int> Square = x => x * x;

        public void PrintSquare(int a)
        {
            Console.WriteLine(Square(a));
        }
    }
}
