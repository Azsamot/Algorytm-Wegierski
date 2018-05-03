using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafy_i_Sieci
{
    class Program
    {
        static uint[,] Algorytm(uint[,] macierz)
        {
            if(!CzyKwadratowa(macierz))
            {
                Console.WriteLine("Macierz nie jest kwadratowa!");
                return macierz;
            }
            OdejmijOdWierszaJegoMinimum(macierz);
            if(Sprawdz1(macierz))
                return macierz;
            
        }
        private static bool CzyKwadratowa(uint[,] macierz)
        {
            return macierz.GetLength(0) == macierz.GetLength(1);
        }
        
        private static uint[] MinimumWKazdymWierszu(uint[,] macierz)
        {
            uint[] min_wiersz = new uint[macierz.GetLength(0)];
            uint min;

            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                min = macierz[i, 0];

                for (int j = 0; j < macierz.GetLength(1); j++)
                {
                    min = Math.Min(min, macierz[i, j]);
                }

                min_wiersz[i] = min;
            }

            return min_wiersz;
        }
        
        private static uint[] MinimumWKazdejKolumnie(uint[,] macierz)
        {
            uint[] min_kolumna = new uint[macierz.GetLength(0)];
            uint min;

            for (int j = 0; j < macierz.GetLength(0); j++)
            {
                min = macierz[0, j];

                for (int i = 0; i < macierz.GetLength(1); i++)
                {
                    min = Math.Min(min, macierz[i, j]);
                }

                min_kolumna[j] = min;
            }

            return min_kolumna;
        }
        
        static void OdejmijOdWierszaJegoMinimum(uint[,] macierz)
        {
            uint[] min_wiersz = MinimumWKazdymWierszu(macierz);
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                for (int j = 0; j < macierz.GetLength(1); j++)
                {
                    macierz[i, j] -= min_wiersz[i];
                }
            }
        }

        static void OdejmijOdKolumnyJejMinimum(uint[,] macierz)
        {
            uint[] min_kolumna = MinimumWKazdejKolumnie(macierz);
            for (int j = 0; j < macierz.GetLength(0); j++)
            {
                for (int i = 0; i < macierz.GetLength(1); i++)
                {
                    macierz[j, i] -= min_kolumna[i];
                }
            }
        }
        static bool Sprawdz1(uint[,] macierz)
        {
            bool test;
            for(int j = 0; j < macierz.GetLength(0); j++)
            {
                test = false;
                for(int i = 0; i < macierz.GetLength(1); i++)
                {
                    if(macierz[i, j] == 0)
                        test = true;
                }
                if(test==false)
                    return test;
            }
            return test;
        }
        static void Main(string[] args)
        {
            uint[,] macierz = new uint[,] { { 3, 3, 3 }, { 2, 2, 2 }, { 1, 1, 1 } };
        }
    }
}
