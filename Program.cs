using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafy_i_Sieci
{
    class Program
    {
        private static bool CzyKwadratowa(uint[,] macierz)
        {
            return macierz.GetLength(0) == macierz.GetLength(1);
        }
        
        static void Main(string[] args)
        {
            uint[,] macierz = new uint[,] { { 3, 3, 3 }, { 2, 2, 2 }, { 1, 1, 1 } };
        }
    }
}
