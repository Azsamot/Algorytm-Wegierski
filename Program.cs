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
            if (!CzyKwadratowa(macierz))
            {
                Console.WriteLine("Macierz nie jest kwadratowa!");
                return macierz;
            }
            OdejmijOdWierszaJegoMinimum(macierz);
            if (Sprawdz1(macierz))
                return macierz;
            OdejmijOdKolumnyJejMinimum(macierz);
            if (Sprawdz1(macierz))
                return macierz;
            Krok4(macierz);
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

        private static void OdejmijOdWierszaJegoMinimum(uint[,] macierz)
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

        private static void OdejmijOdKolumnyJejMinimum(uint[,] macierz)
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
        private static bool Sprawdz1(uint[,] macierz)
        {
            bool test = false;
            for (int j = 0; j < macierz.GetLength(0); j++)
            {
                test = false;
                for (int i = 0; i < macierz.GetLength(1); i++)
                {
                    if (macierz[i, j] == 0)
                        test = true;
                }
                if (test == false)
                    return test;
            }
            return test;
        }

        //tworzy macierz 0-1-2, liczba oznacza ilość przekreśleń na danym elemencie
        //nie czepiać się oznaczeń, pisane na szybko
        private static uint[,] MacierzPrzekreslen(uint[,] macierz, ref uint licznik)
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
                    licznik++;
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
                    licznik++;
                    for (int i = 0; i < macierz.GetLength(0); i++)
                    {
                        nowa[i, j]++;
                    }
                }
                cala = 0;
            }

            return nowa;
        }
        private static uint[,] Krok4(uint[,] macierz)
        {
            uint licznik = 0;
            uint[,] linie = MacierzPrzekreslen(macierz, ref licznik);
            if (licznik == macierz.GetLength(0))
                return macierz;
            uint min = 0;
            for (int i = 0; i < linie.GetLength(0); i++)
            {
                for (int j = 0; j < linie.GetLength(1); j++)
                {
                    if (linie[i, j] == 0)
                        min = Math.Min(min, linie[i, j]);
                }
            }
            for (int i = 0; i < linie.GetLength(0); i++)
            {
                for (int j = 0; j < linie.GetLength(1); j++)
                {
                    if (linie[i, j] == 0)
                        macierz[i, j] -= min;
                    else if (linie[i, j] == 2)
                        macierz[i, j] += min;
                }
            }
            return Krok4(macierz);
        }
        static void Main(string[] args)
        {
            uint[,] macierz = new uint[,] { { 82, 83, 69, 92 }, { 77, 37, 49, 92 }, { 11, 69, 5, 86 }, { 8, 9, 98, 23 } };
            macierz = Algorytm(macierz);
            for (int i = 0; i < macierz.GetLength(0); i++)
            {
                for (int j = 0; j < macierz.GetLength(1); j++)
                {
                    Console.Write(macierz[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
