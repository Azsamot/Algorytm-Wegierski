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
            OdejmijOdKolumnyJejMinimum(macierz);
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
        
        //tworzy macierz 0-1-2, liczba oznacza ilość przekreśleń na danym elemencie
        //nie czepiać się oznaczeń, pisane na szybko
        static uint[,] MacierzPrzekreslen(uint[,] macierz)
        {
            //pusta macierz NxN wypełniona zerami
            uint[,] nowa = new uint[macierz.GetLength(0), macierz.GetLength(0)];
            for (int i = 0; i < nowa.GetLength(0); i++)
            {
                for (int j = 0; j < nowa.GetLength(0); j++)
                {
                    nowa[i, j] = 0;
                }
            }

            //zmienna odpowiada za to, czy przekreslić dany wiersz, czy nie
            int caly = 0;

            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                for (int j = 0; j < macierz.GetLength(0); j++)
                {
                    if (macierz[i, j] == 0)
                        caly++;
                }
                //jeżeli więcej niż jedno zero z wierszu ---> przekreślamy
                if (caly > 1)
                {
                    for (int j = 0; j < macierz.GetLength(0); j++)
                    {
                        nowa[i, j]++;
                    }
                }
                caly = 0;
            }

            //analogicznie z kolumnami
            int cala = 0;

            for (int j = 0; j < macierz.GetLength(0); j++)
            {
                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    if (macierz[i, j] == 0)
                        cala++;
                }

                if (cala > 1)
                {
                    for (int i = 0; i < macierz.GetLength(0); i++)
                    {
                        nowa[i, j]++;
                    }
                }
                cala = 0;
            }

            return nowa;
        }
        
        static void Main(string[] args)
        {
            uint[,] macierz = new uint[,] { { 3, 3, 3 }, { 2, 2, 2 }, { 1, 1, 1 } };
        }
    }
}
