﻿using System;
using System.Net;

namespace Grafy_i_Sieci
{
    class Program
    {
        class Wegier
        {
            public static void WyswietlMacierz(int[,] macierz)
            {
                Console.WriteLine("\n----------");
                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz.GetLength(1); j++)
                    {
                        Console.Write(macierz[i, j] + " ");
                    }

                    Console.WriteLine();
                }
                Console.WriteLine("----------");
            }

            public static void uWyswietlMacierz(uint[,] macierz)
            {
                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz.GetLength(1); j++)
                    {
                        if (macierz[i, j] == 0)
                            Console.ForegroundColor = ConsoleColor.Red;

                        else
                            Console.ForegroundColor = ConsoleColor.Gray;

                        Console.Write(macierz[i, j] + " ");

                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    Console.WriteLine();
                }

            }

            public static void zWyswietlMacierz(uint[,] macierz)
            {
                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz.GetLength(1); j++)
                    {
                        if (macierz[i, j] == 1)
                            Console.ForegroundColor = ConsoleColor.Green;

                        else if (macierz[i, j] == 2)
                            Console.ForegroundColor = ConsoleColor.DarkBlue;

                        else
                            Console.ForegroundColor = ConsoleColor.Gray;

                        Console.Write(macierz[i, j] + " ");

                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    Console.WriteLine();
                }

            }

            public static void lWyswietlMacierz(int[,] macierz)
            {
                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz.GetLength(1); j++)
                    {
                        if (macierz[i, j] == 1)
                            Console.ForegroundColor = ConsoleColor.DarkYellow;

                        else if (macierz[i, j] == 2)
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;

                        else
                            Console.ForegroundColor = ConsoleColor.Gray;

                        Console.Write(macierz[i, j] + " ");

                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    Console.WriteLine();
                }

            }

            public static uint[] MinimumWKazdymWierszu(uint[,] macierz)
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

            public static uint[] MinimumWKazdejKolumnie(uint[,] macierz)
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

            public static void OdejmijOdWierszaJegoMinimum(uint[,] macierz)
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

            public static void OdejmijOdKolumnyJejMinimum(uint[,] macierz)
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

            static uint[] ZeraWWierszach(uint[,] macierz)
            {
                uint[] zera = new uint[macierz.GetLength(1)];

                for (int i = 0; i < macierz.GetLength(1); i++)
                {
                    for (int j = 0; j < macierz.GetLength(1); j++)
                    {
                        if (macierz[i, j] == 0)
                            zera[i]++;
                    }
                }

                return zera;
            }

            static uint[] ZeraWKolumnach(uint[,] macierz)
            {
                uint[] zera = new uint[macierz.GetLength(1)];

                for (int j = 0; j < macierz.GetLength(1); j++)
                {
                    for (int i = 0; i < macierz.GetLength(1); i++)
                    {
                        if (macierz[i, j] == 0)
                            zera[j]++;
                    }
                }

                return zera;
            }

            public static void Krok4(uint[,] macierz, int[,] linie)
            {
                uint min = 200;
                for (int i = 0; i < linie.GetLength(0); i++)
                {
                    for (int j = 0; j < linie.GetLength(1); j++)
                    {
                        if (linie[i, j] == 0)
                            min = Math.Min(min, macierz[i, j]);
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

            }

            public static bool Sprawdz1(uint[,] macierz)
            {
                bool test = false;
                uint[,] testowa = new uint[macierz.GetLength(0), macierz.GetLength(1)];
                for (int j = 0; j < macierz.GetLength(0); j++)
                {
                    test = false;
                    for (int i = 0; i < macierz.GetLength(1); i++)
                    {
                        if (testowa[i, j] != 1)
                        {
                            if (macierz[i, j] == 0)
                            {
                                test = true;
                                for (int k = j; k < macierz.GetLength(1); k++)
                                {
                                    testowa[i, k] = 1;
                                }
                            }
                        }
                    }
                    if (test == false)
                        return test;
                }
                return test;
            }

            public static bool Suma(uint[] wektor)
            {
                uint suma = 0;

                foreach (var item in wektor)
                {
                    suma += item;
                }

                return suma == 0;
            }

            public static int[,] Wynik(uint[,] macierz_prawdziwa, uint[,] zera)
            {
                uint[,] macierz = new uint[macierz_prawdziwa.GetLength(0), macierz_prawdziwa.GetLength(0)];
                for (int i = 0; i < macierz_prawdziwa.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz_prawdziwa.GetLength(0); j++)
                    {
                        macierz[i, j] = macierz_prawdziwa[i, j];
                    }
                }

                int[,] wynikowa = new int[macierz.GetLength(0), macierz.GetLength(0)];

                uint minzera = (uint)macierz.GetLength(0);
                uint[] wiersz = ZeraWWierszach(macierz);
                uint[] kolumna = ZeraWKolumnach(macierz);
                int kierunek = 0;
                int indeks = 0;
                int drugi = 0;

                do
                {
                    minzera = (uint)macierz.GetLength(0);
                    wiersz = ZeraWWierszach(macierz);
                    kolumna = ZeraWKolumnach(macierz);
                    for (int i = 0; i < wiersz.Length; i++)
                    {
                        if (wiersz[i] < minzera && wiersz[i] > 0)
                        {
                            minzera = wiersz[i];
                            kierunek = 1;
                            indeks = i;
                        }
                    }
                    for (int i = 0; i < kolumna.Length; i++)
                    {
                        if (kolumna[i] < minzera && kolumna[i] > 0)
                        {
                            minzera = kolumna[i];
                            kierunek = -1;
                            indeks = i;
                        }
                    }
                    //Console.WriteLine(minzera + "<--- minzera");

                    if (kierunek == 1 && minzera < (uint)macierz.GetLength(0))
                    {
                        for (int i = 0; i < wiersz.Length; i++)
                        {
                            if (macierz[indeks, i] == 0)
                            {
                                drugi = i;
                                wynikowa[indeks, i] = 1;
                                break;

                            }

                        }

                        for (int i = 0; i < wiersz.Length; i++)
                        {
                            macierz[indeks, i] = 666;
                            macierz[i, drugi] = 666;
                        }
                    }


                    if (kierunek == -1 && minzera < (uint)macierz.GetLength(0))
                    {
                        for (int i = 0; i < kolumna.Length; i++)
                        {
                            if (macierz[i, indeks] == 0)
                            {
                                drugi = i;
                                wynikowa[i, indeks] = 1;
                                break;
                            }

                        }

                        for (int i = 0; i < wiersz.Length; i++)
                        {
                            macierz[i, indeks] = 666;
                            macierz[drugi, i] = 666;
                        }
                    }
                    //uWyswietlMacierz(macierz);
                    //WyswietlMacierz(wynikowa);
                    //uWyswietlMacierz(macierz_prawdziwa);
                } while (minzera < (uint)macierz.GetLength(0));
                // WyswietlMacierz(wynikowa);


                //return wynik;

                //uWyswietlMacierz(macierz);
                return wynikowa;
            }

            public static int Policz(uint[,] macierz, uint[,] wynikowa)
            {
                int wynik = 0;



                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz.GetLength(0); j++)
                    {
                        if (wynikowa[i, j] == 1)
                            wynik += (int)macierz[i, j];
                    }
                }



                return wynik;
            }

            //public static int Wykonaj(uint[,] macierz_prawdziwa)
            //{
            //    uint[,] macierz = new uint[macierz_prawdziwa.GetLength(0), macierz_prawdziwa.GetLength(0)];
            //    for (int i = 0; i < macierz_prawdziwa.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < macierz_prawdziwa.GetLength(0); j++)
            //        {
            //            macierz[i, j] = macierz_prawdziwa[i, j];
            //        }
            //    }

            //    //Console.Write("Macierz wejściowa");
            //    //Wegier.uWyswietlMacierz(macierz);

            //    Wegier.OdejmijOdWierszaJegoMinimum(macierz);
            //    Wegier.OdejmijOdKolumnyJejMinimum(macierz);
            //    //Console.Write("Macierz po 1 kroku");
            //    //Wegier.uWyswietlMacierz(macierz);
            //    //WyswietlMacierz(RysujLinie(macierz, ref bla));

            //    int liczba = 0;

            //    int[,] nowe = new int[macierz.GetLength(0), macierz.GetLength(0)];
            //    int k = 0;

            //    do
            //    {
            //        liczba = 0;
            //        nowe = Wegier.RysujLinie(macierz, ref liczba);
            //        Wegier.Krok4(macierz, nowe);
            //        //Console.WriteLine(k);
            //        //uWyswietlMacierz(macierz);
            //        //WyswietlMacierz(nowe);
            //        k++;
            //    } while (liczba < macierz.GetLength(0));

            //    //Console.Write("Macierz końcowa");
            //    // Wegier.uWyswietlMacierz(macierz);

            //    //Console.WriteLine("Liczba iteracji: " + k);
            //    //Console.WriteLine("wynik");
            //    //int[,] wynikowa = Wynik(macierz);
            //    //Console.WriteLine("Macierz wynikowa (błąd)");
            //    //WyswietlMacierz(wynikowa);
            //    //return Policz(macierz_prawdziwa, wynikowa);
            //    //Console.WriteLine("nasz wynik: " + Policz(macierz_prawdziwa,wynikowa));
            //    //uWyswietlMacierz(macierz);
            //}

            public static int Wykonaj9(uint[,] macierz_prawdziwa)
            {
                uint[,] macierz = new uint[macierz_prawdziwa.GetLength(0), macierz_prawdziwa.GetLength(0)];
                for (int i = 0; i < macierz_prawdziwa.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz_prawdziwa.GetLength(0); j++)
                    {
                        macierz[i, j] = macierz_prawdziwa[i, j];
                    }
                }

                //Console.Write("Macierz wejściowa\n");
                //Wegier.uWyswietlMacierz(macierz);
                Console.WriteLine();

                Wegier.OdejmijOdWierszaJegoMinimum(macierz);
                Wegier.OdejmijOdKolumnyJejMinimum(macierz);
                Console.Write("Macierz po 1 kroku\n");
                Wegier.uWyswietlMacierz(macierz);
                Console.WriteLine();
                //int bla = 0;
                //WyswietlMacierz(RysujLinie(macierz, ref bla));

                int liczba = 0;

                int[,] nowe = new int[macierz.GetLength(0), macierz.GetLength(0)];
                uint[,] zera = new uint[macierz.GetLength(0), macierz.GetLength(1)];
                int k = 0;

                while (liczba < macierz.GetLength(0))
                {

                    nowe = Wegier.RysujLinie(macierz, ref liczba, out zera);
                    Wegier.Krok4(macierz, nowe);
                    Console.WriteLine(k + "-ty krok\n");
                    uWyswietlMacierz(macierz);
                    Console.WriteLine();

                    zWyswietlMacierz(Wegier.ZeraNiezalezne(macierz));
                    Console.WriteLine();
                    lWyswietlMacierz(nowe);
                    Console.WriteLine();
                    //Console.WriteLine(k);

                    k++;
                }
                //zWyswietlMacierz(ZeraNiezalezne(macierz));
                //WyswietlMacierz(nowe);

                //Console.Write("Macierz końcowa");
                // Wegier.uWyswietlMacierz(macierz);

                //Console.WriteLine("Liczba iteracji: " + k);
                //Console.WriteLine("wynik");
                //int[,] wynikowa = Wynik(macierz, zera);
                //Console.WriteLine("Macierz wynikowa (błąd)");
                //WyswietlMacierz(wynikowa);
                return Policz(macierz_prawdziwa, zera);
                //Console.WriteLine("nasz wynik: " + Policz(macierz_prawdziwa,wynikowa));
                //uWyswietlMacierz(macierz);
            }

            public static int PobierzWynik(uint[,] macierz)
            {
                WebClient client = new WebClient();
                string adres = "http://www.hungarianalgorithm.com/solve.php?c=";
                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz.GetLength(0); j++)
                    {
                        adres += macierz[i, j];
                        adres += "-";
                    }
                    adres += "-";
                }
                //Console.WriteLine(adres);
                string c = client.DownloadString(adres);
                string wynik = "";
                for (int i = c.Length - 1; i > 0; i--)
                {
                    if (c[i] == ' ' && c[i - 1] == 's' && c[i - 2] == 'l' && c[i - 3] == 'a')
                    {
                        wynik += c[i + 1];
                        wynik += c[i + 2];
                        if (c[i + 3] != '.')
                            wynik += c[i + 3];
                        break;
                    }
                }
                return Convert.ToInt32(wynik);
                //Console.WriteLine("Wynik z pięknej strony internetowej: " + wynik);
            }

            private static uint[,] ZeraNiezalezne(uint[,] macierz)
            {
                uint[,] zera = new uint[macierz.GetLength(0), macierz.GetLength(1)];
                uint[] zeraWiersze = IleZer(macierz, zera);
                while (Liczbazer(zeraWiersze) != 0)
                {
                    while (Jedynki(zeraWiersze) > 0)
                    {
                        JednoZero(macierz, zeraWiersze, ref zera);
                        zeraWiersze = IleZer(macierz, zera);
                    }
                    WiecejZer(macierz, ref zeraWiersze, ref zera);
                }
                return zera;
            }

            private static void WiecejZer(uint[,] macierz, ref uint[] zeraWiersze, ref uint[,] zera)
            {
                for (int i = 0; i < zeraWiersze.Length; i++)
                {
                    if (Jedynki(zeraWiersze) >= 1)
                        return;
                    if (zeraWiersze[i] >= 2)
                    {
                        int Zera = int.MaxValue;
                        int idx = 0;
                        for (int j = macierz.GetLength(1) - 1; j >= 0; j--)
                        {
                            
                            if (macierz[i, j] == 0 && zera[i, j] == 0)
                            {
                                int ilezer = 0;
                                for (int k = 0; k < macierz.GetLength(1); k++)
                                {
                                    if (k != i && macierz[k, j] == 0)
                                        ilezer++;
                                }
                                if (Zera > ilezer)
                                {
                                    Zera = ilezer;
                                    idx = j;
                                }

                            }
                        }
                    zera[i, idx] = 1;
                    WykreslZera(macierz, ref zera, i, idx);
                    }

                    zeraWiersze = IleZer(macierz, zera);
                }
            }

            private static void WykreslZera(uint[,] macierz, ref uint[,] zera, int r, int c)
            {
                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    if (macierz[r, i] == 0 && zera[r, i] == 0)
                    {
                        zera[r, i] = 2;
                    }
                    if (macierz[i, c] == 0 && zera[i, c] == 0)
                    {
                        zera[i, c] = 2;
                    }
                }
            }

            private static void JednoZero(uint[,] macierz, uint[] zeraWiersze, ref uint[,] zera)
            {
                for (int i = 0; i < zeraWiersze.Length; i++)
                {
                    if (zeraWiersze[i] == 1)
                    {
                        for (int j = 0; j < macierz.GetLength(0); j++)
                        {
                            if (macierz[i, j] == 0 && zera[i, j] == 0)
                            {
                                zera[i, j] = 1;
                                for (int k = 0; k < macierz.GetLength(0); k++)
                                {
                                    if (macierz[k, j] == 0 && zera[k, j] == 0)
                                    {
                                        zera[k, j] = 2;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            private static uint Liczbazer(uint[] zeraWiersze)
            {
                uint liczbazer = 0;
                for (int i = 0; i < zeraWiersze.Length; i++)
                {
                    liczbazer += zeraWiersze[i];
                }
                return liczbazer;
            }

            private static int Jedynki(uint[] zeraWiersze)
            {
                int jedynki = 0;
                for (int i = 0; i < zeraWiersze.Length; i++)
                {
                    if (zeraWiersze[i] == 1)
                        jedynki++;
                }
                return jedynki;
            }

            private static uint[] IleZer(uint[,] macierz, uint[,] zera)
            {
                uint[] liczbazer = new uint[macierz.GetLength(0)];
                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz.GetLength(1); j++)
                    {
                        if (macierz[i, j] == 0 && zera[i, j] == 0)
                            liczbazer[i]++;
                    }
                }
                return liczbazer;
            }

            public static int[,] RysujLinie(uint[,] macierz, ref int liczba, out uint[,] zera)
            {
                liczba = 0;
                int[,] linie = new int[macierz.GetLength(0), macierz.GetLength(1)];
                zera = ZeraNiezalezne(macierz);
                int[] wiersze_zaznaczone = new int[linie.GetLength(0)];
                int[] kolumny_zaznaczone = new int[linie.GetLength(0)];
                int licznik = int.MaxValue;
                for (int i = 0; i < wiersze_zaznaczone.Length; i++)
                {
                    wiersze_zaznaczone[i] = 1;
                }

                for (int i = 0; i < zera.GetLength(0); i++)
                {
                    for (int j = 0; j < zera.GetLength(0); j++)
                    {
                        if (zera[i, j] == 1)
                            wiersze_zaznaczone[i] = 0;

                    }
                }
                while (licznik != 0)
                {
                    licznik = 0;
                    for (int i = 0; i < zera.GetLength(0); i++)
                    {
                        for (int j = 0; j < zera.GetLength(0); j++)
                        {
                            if (wiersze_zaznaczone[i] == 1 && macierz[i, j] == 0 && kolumny_zaznaczone[j] == 0)
                            {
                                kolumny_zaznaczone[j] = 1;
                                licznik++;
                            }

                        }
                    }

                    for (int i = 0; i < zera.GetLength(0); i++)
                    {

                        for (int j = 0; j < zera.GetLength(0); j++)
                        {
                            if (kolumny_zaznaczone[j] == 1 && zera[i, j] == 1)
                                wiersze_zaznaczone[i] = 1;
                        }
                    }
                }

                for (int i = 0; i < zera.GetLength(0); i++)
                {
                    for (int j = 0; j < zera.GetLength(0); j++)
                    {
                        if (wiersze_zaznaczone[i] == 1 && macierz[i, j] == 0)
                            kolumny_zaznaczone[j] = 1;
                    }
                }

                for (int i = 0; i < linie.GetLength(0); i++)
                {
                    for (int j = 0; j < linie.GetLength(0); j++)
                    {
                        if (wiersze_zaznaczone[i] == 0)
                            linie[i, j]++;



                        if (kolumny_zaznaczone[j] == 1)
                            linie[i, j]++;

                    }


                }

                for (int i = 0; i < wiersze_zaznaczone.Length; i++)
                {
                    if (wiersze_zaznaczone[i] == 0)
                        liczba++;

                    if (kolumny_zaznaczone[i] == 1)
                        liczba++;
                }

                //WyswietlMacierz(linie);

                return linie;
            }

            //public static void TestujWys()
            //{
            //    Random r = new Random();
            //    Console.WriteLine("liczę");
            //    Stopwatch sw = new Stopwatch();
            //    int licznik = 0;
            //    int zneta = 0;
            //    Console.Write("Podaj liczbę testów: ");
            //    int s = Convert.ToInt32(Console.ReadLine());
            //    uint[,] losowa = new uint[10, 10];
            //    uint[,] losowa1 = new uint[10, 10];
            //    Console.Clear();
            //    int wynik = 0;
            //    sw.Start();

            //    for (int p = 0; p < s; p++)
            //    {
            //        for (int i = 0; i < 10; i++)
            //        {
            //            for (int j = 0; j < 10; j++)
            //            {
            //                losowa[i, j] = (uint)r.Next(1, 10);
            //                losowa1[i, j] = losowa[i, j];
            //            }
            //        }

            //        wynik = Wykonaj(losowa);

            //        zneta = PobierzWynik(losowa1);

            //        Console.Write(wynik + " ");
            //        Console.WriteLine(zneta);

            //        if (wynik == zneta)
            //            licznik++;



            //        // sw.Reset();
            //    }
            //    sw.Stop();
            //    Console.Write("skuteczność " + licznik / s * 100 + "% (" + licznik + "/" + s + ")\nSprawdzono w czasie: ");
            //    Console.Write(" " + Convert.ToDouble(sw.ElapsedMilliseconds) / 1000 + "s");
            //}

            //public static void Testuj()
            //{
            //    Random r = new Random();
            //    Stopwatch sw = new Stopwatch();
            //    int licznik = 0;
            //    int zneta = 0;
            //    Console.Write("Podaj liczbę testów: ");
            //    int s = Convert.ToInt32(Console.ReadLine());
            //    uint[,] losowa = new uint[10, 10];
            //    uint[,] losowa1 = new uint[10, 10];
            //    Console.Clear();
            //    int wynik = 0;
            //    sw.Start();

            //    for (int p = 0; p < s; p++)
            //    {
            //        for (int i = 0; i < 10; i++)
            //        {
            //            for (int j = 0; j < 10; j++)
            //            {
            //                losowa[i, j] = (uint)r.Next(1, 100);
            //                losowa1[i, j] = losowa[i, j];
            //            }
            //        }

            //        wynik = Wykonaj(losowa);

            //        zneta = PobierzWynik(losowa1);


            //        if (wynik == zneta)
            //            licznik++;

            //        if (wynik != zneta)
            //        // Console.WriteLine(p);
            //        {
            //            Console.WriteLine("wejściowa");
            //            uWyswietlMacierz(losowa);
            //            Wykonaj9(losowa);
            //            Console.WriteLine("Nasz wynik: "+wynik);
            //            Console.WriteLine("Wynik z neta: "+zneta);

            //        }


            //        // sw.Reset();
            //    }
            //    sw.Stop();
            //    Console.Write("skuteczność " + licznik / s * 100 + "% (" + licznik + "/" + s + ")\nSprawdzono w czasie: ");
            //    Console.Write(" " + Convert.ToDouble(sw.ElapsedMilliseconds) / 1000 + "s");
            //}

            public static void Testuj(uint[,] macierz)
            {
                int licznik = 0;
                int zneta = 0;
                Console.Clear();
                int wynik = 0;

                uint[,] kopia = new uint[10, 10];

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        kopia[i, j] = macierz[i, j];
                    }
                }



                wynik = Wykonaj9(macierz);

                zneta = PobierzWynik(macierz);


                if (wynik == zneta)
                    Console.WriteLine("kurła, działa");

                if (wynik != zneta)
                // Console.WriteLine(p);
                {
                    //Console.WriteLine("wejściowa");
                    //uWyswietlMacierz(macierz);
                    //Wykonaj9(macierz);
                    Console.WriteLine("kurła, nie działa");

                }
            }

        }
        static void Main(string[] args)
        {
            uint[,] matrix = new uint[,] { { 3, 3, 3 }, { 2, 2, 2 }, { 1, 1, 1 } };
            uint[,] mac2 = {     { 52, 13, 23, 1, 3, 92, 2 , 16},
                                 { 21, 33, 13, 5, 12, 32, 9 , 31},
                                 { 5, 3, 93, 65, 54, 57, 19 , 14},
                                 { 12, 31, 73, 86, 65, 95, 87 , 22},
                                 { 67, 43, 13, 43, 1, 32, 79 , 9},
                                 { 89, 65, 15, 54, 13, 12, 2 , 8},
                                 { 19, 13, 65, 15, 65, 9, 29 , 71},
                                 { 2, 3, 23, 5, 23, 86, 49 , 77} };
            uint[,] mac3 = new uint[,] { { 90, 75, 75, 80 }, { 35, 85, 55, 65 }, { 125, 95, 90, 105 }, { 45, 110, 95, 115 } };
            uint[,] mac4 = { { 82, 83, 69, 92 }, { 77, 37, 49, 92 }, { 11, 69, 5, 86 }, { 8, 9, 98, 23 } };
            uint[,] mac5 = { { 0, 7, 5, 0, 6 }, { 4, 0, 7, 5, 1 }, { 4, 0, 0, 5, 8 }, { 1, 5, 0, 8, 2 }, { 0, 4, 9, 2, 0 } };
            uint[,] ostatnia = {{70, 3, 96, 69, 81, 82, 41, 47, 17, 84},
                                {78, 42, 31, 37, 6, 45, 38, 92, 97, 22},
                                {54, 4, 24, 21, 82, 49, 43, 22, 41, 21},
                                {32, 36, 81, 14, 27, 94, 61, 66, 93, 35},
                                {25, 60, 11, 65, 18, 68, 22, 1, 73, 31},
                                {93, 15, 71, 71, 55, 67, 78, 76, 87, 32},
                                {40, 20, 6, 96, 53, 42, 61, 18, 10, 11},
                                {83, 72, 98, 62, 96, 93, 92, 59, 4, 14},
                                {27, 42, 49, 10, 27, 17, 64, 10, 59, 60},
                                {16, 85, 79, 61, 95, 6, 55, 15, 13, 75}};
            //uint[,] ostatnia = {{58, 27, 6, 20, 57, 34, 30, 76, 12, 53},
            //{78, 4, 43, 76, 51, 90, 83, 74, 62, 22},
            //{22, 41, 87, 51, 92, 26, 45, 89, 61, 58},
            //{29, 12, 62, 30, 36, 89, 56, 71, 65, 31},
            //{20, 97, 55, 18, 42, 12, 23, 98, 26, 89},
            //{77, 54, 85, 41, 42, 17, 40, 54, 28, 31},
            //{89, 41, 15, 53, 25, 67, 42, 14, 40, 62},
            //{35, 13, 9, 31, 3, 25, 86, 69, 10, 81},
            //{4, 47, 64, 72, 81, 74, 27, 21, 87, 20},
            //{49, 2, 43, 35, 42, 79, 82, 2, 93, 75}};

            //Wegier.Testuj(macierz); ---> test pojedyncznej, wyświetla wszystkie kroki
            //Wegier.Testuj();        ---> wybierasz liczbę testów randomowych macierzy, wyświetla błędne krok po kroku
            Wegier.Testuj(ostatnia);
            //Wegier.Testuj();
            Console.ReadKey();
        }
    }
}