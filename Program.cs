using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Grafy_i_Sieci
{
    class Program
    {
        class Wegier
        {       
            //metody główne do wykonania algorytmu
            //odejmij od każdego wiersza jego minimum
            private static void Krok1(uint[,] macierz)
            {
                uint min = UInt32.MaxValue;
                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    
                    for (int j = 0; j < macierz.GetLength(1); j++)
                    {
                        if (macierz[i,j] < min)
                            min = macierz[i,j];
                    }
                    for (int j = 0; j < macierz.GetLength(1); j++)
                    macierz[i, j] -= min;

                    min = UInt32.MaxValue;
                }
            }

            //odejmij od każdej kolumny jej minimum
            private static void Krok2(uint[,] macierz)
            {
                uint min = UInt32.MaxValue;
                for (int j = 0; j < macierz.GetLength(0); j++)
                {

                    for (int i = 0; i < macierz.GetLength(1); i++)
                    {
                        if (macierz[i, j] < min)
                            min = macierz[i, j];
                    }
                    for (int i = 0; i < macierz.GetLength(1); i++)
                        macierz[i, j] -= min;

                    min = UInt32.MaxValue;
                }
            }

            //wyznaczenie zer niezależnych
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

            //rysowanie linii na podstawie zer niezależnych
            private static int[,] RysujLinie(uint[,] macierz, ref int liczba, out uint[,] zera)
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

            //zwiększenie liczby zer niezależnych
            private static void Krok4(uint[,] macierz, int[,] linie)
            {
                uint min = uint.MaxValue;
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

            //metoda zwracająca i wyświetlająca wynik
            private static int Policz(uint[,] macierz, uint[,] wynikowa)
            {
                int wynik = 0;

                string tekst = "Ostateczny wynik to: ";

                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz.GetLength(0); j++)
                    {
                        if (wynikowa[j, i] == 1)
                        {
                            wynik += (int)macierz[j, i];
                            tekst += macierz[j, i];
                            if (i == macierz.GetLength(0) - 1)
                                tekst += " = ";
                            else
                                tekst += " + ";
                        }
                    }
                }

                tekst += wynik;
                Console.WriteLine(tekst + ".");

                return wynik;
            }

            //wykonanie algorytmu, wyświetla tylko wynik
            private static int Wykonaj(uint[,] macierz_prawdziwa)
            {
                int counter = 0;
                uint[,] macierz = new uint[macierz_prawdziwa.GetLength(0), macierz_prawdziwa.GetLength(0)];
                for (int i = 0; i < macierz_prawdziwa.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz_prawdziwa.GetLength(0); j++)
                    {
                        macierz[i, j] = macierz_prawdziwa[i, j];
                    }
                }

                int liczba = 0;
                int[,] nowe = new int[macierz.GetLength(0), macierz.GetLength(0)];
                uint[,] zera = new uint[macierz.GetLength(0), macierz.GetLength(1)];

                Wegier.Krok1(macierz);
                int test = 0;
                int[,] pierwsze = RysujLinie(macierz, ref test, out zera);
                if (test < macierz.GetLength(0))
                {
                    Wegier.Krok2(macierz);
                    test = 0;
                    pierwsze = RysujLinie(macierz, ref test, out zera);
                    if (test < macierz.GetLength(0))
                    {
                        while (liczba < macierz.GetLength(0))
                        {
                            nowe = Wegier.RysujLinie(macierz, ref liczba, out zera);
                            Wegier.Krok4(macierz, nowe);

                            //ładny spinner
                            switch (counter % 4)
                            {
                                case 0: Console.Write("/"); break;
                                case 1: Console.Write("-"); break;
                                case 2: Console.Write("\\"); break;
                                case 3: Console.Write("|"); counter = -1; break;
                            }
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            counter++;

                        }
                    }
                }
                return Policz(macierz_prawdziwa, zera);

            }

            //wykonanie algorytmu, wyświetla wszystko krok po kroku
            private static int WykonajWyswietl(uint[,] macierz_wejsciowa)
            {
                uint[,] macierz = new uint[macierz_wejsciowa.GetLength(0), macierz_wejsciowa.GetLength(0)];
                for (int i = 0; i < macierz_wejsciowa.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz_wejsciowa.GetLength(0); j++)
                    {
                        macierz[i, j] = macierz_wejsciowa[i, j];
                    }
                }

                int liczba = 0;
                int[,] nowe = new int[macierz.GetLength(0), macierz.GetLength(0)];
                uint[,] zera = new uint[macierz.GetLength(0), macierz.GetLength(1)];
                int k = 1;

                Console.Write("Macierz wejściowa\n");
                Wegier.uWyswietlMacierz(macierz);
                Console.WriteLine();

                Wegier.Krok1(macierz);
                Console.WriteLine("Krok {0}: ", k++);
                uWyswietlMacierz(macierz);
                Console.WriteLine();
                zWyswietlMacierz(Wegier.ZeraNiezalezne(macierz));
                int test = 0;
                int[,] pierwsze = RysujLinie(macierz, ref test, out zera);
                Console.WriteLine("\nNaciśnij dowolny klawisz, aby przejść do kolejnego kroku.\n\n");
                Console.ReadKey();
                if (test < macierz.GetLength(0))
                {

                    Wegier.Krok2(macierz);
                    Console.WriteLine("Krok {0}: ", k++);
                    uWyswietlMacierz(macierz);
                    Console.WriteLine();
                    zWyswietlMacierz(Wegier.ZeraNiezalezne(macierz));
                    Console.WriteLine();
                    Console.WriteLine("Naciśnij dowolny klawisz, aby przejść do kolejnego kroku.\n\n");
                    Console.ReadKey();
                    test = 0;
                    pierwsze = RysujLinie(macierz, ref test, out zera);
                    if (test < macierz.GetLength(0))
                    {
                        while (liczba < macierz.GetLength(0))
                        {

                            nowe = Wegier.RysujLinie(macierz, ref liczba, out zera);
                            if (liczba < macierz.GetLength(0))
                            {
                                Wegier.Krok4(macierz, nowe);
                                Console.WriteLine("Krok {0}: ", k++);
                                lWyswietlMacierz(nowe);
                                Console.WriteLine();
                                uWyswietlMacierz(macierz);
                                Console.WriteLine();

                                zWyswietlMacierz(Wegier.ZeraNiezalezne(macierz));

                                Console.WriteLine();
                                Console.WriteLine("Naciśnij dowolny klawisz, aby przejść do kolejnego kroku.\n\n");
                                Console.ReadKey();
                            }

                        }
                    }
                }
                Console.WriteLine("Krok {0}: ", k);
                Console.WriteLine();
                wWyswietlMacierz(macierz_wejsciowa, ZeraNiezalezne(macierz));

                
                Console.WriteLine("\n");
                return Policz(macierz_wejsciowa, zera);

            }



            //metody pomocnicze, bez których nie pójdą powyższe
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



            //metody wyświetlające macierze
            //zera na czerwono
            private static void uWyswietlMacierz(uint[,] macierz)
            {
                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz.GetLength(1); j++)
                    {
                        if (macierz[i, j] == 0)
                            Console.ForegroundColor = ConsoleColor.Red;

                        else
                            Console.ForegroundColor = ConsoleColor.Gray;

                        if (macierz[i, j] < 10)
                            Console.Write(macierz[i, j] + "     ");

                        if (macierz[i, j] >= 10 && macierz[i, j] < 100)
                            Console.Write(macierz[i, j] + "    ");

                        if (macierz[i, j] >= 100 && macierz[i, j] < 1000)
                            Console.Write(macierz[i, j] + "   ");

                        if (macierz[i, j] >= 1000)
                            Console.Write(macierz[i, j] + "  ");

                        //if (macierz[i, j] >= 100)
                        //    Console.Write(macierz[i, j] + " ");

                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    Console.WriteLine();
                }
            }

            //macierz wynikowa - optymalne wartości na zielono
            private static void wWyswietlMacierz(uint[,] macierz, uint[,] zera)
            {
                for (int i = 0; i < macierz.GetLength(0); i++)
                {
                    for (int j = 0; j < macierz.GetLength(1); j++)
                    {
                        if (zera[i, j] == 1)
                            Console.ForegroundColor = ConsoleColor.Green;

                        else
                            Console.ForegroundColor = ConsoleColor.Gray;

                        if (macierz[i, j] < 10)
                            Console.Write(macierz[i, j] + "     ");

                        if (macierz[i, j] >= 10 && macierz[i, j] < 100)
                            Console.Write(macierz[i, j] + "    ");

                        if (macierz[i, j] >= 100 && macierz[i, j] < 1000)
                            Console.Write(macierz[i, j] + "   ");

                        if (macierz[i, j] >= 1000)
                            Console.Write(macierz[i, j] + "  ");

                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    Console.WriteLine();
                }
            }

            //macierz z zerami niezależnymi
            private static void zWyswietlMacierz(uint[,] macierz)
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

            //macierzy z liniami
            private static void lWyswietlMacierz(int[,] macierz)
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



            //metody do prezentacji działania
            private static void Dzialanie_Wpisz(int p, int rozmiar)
            {
                uint[,] macierz = new uint[rozmiar, rozmiar];
                uint wartosc;
                int chk = 0;

                for (int i = 0; i < rozmiar; i++)
                {
                    for (int j = 0; j < rozmiar; j++)
                    {
                        while (chk == 0)
                        {
                            Console.Write("Podaj wartość elementu [{0}, {1}]: ", i, j);
                            try
                            {
                                wartosc = Convert.ToUInt32(Console.ReadLine());
                                if (wartosc == 0)
                                    Console.WriteLine("Błędna wartość. Spróbuj jeszcze raz.");
                                else
                                {
                                    macierz[i, j] = wartosc;
                                    chk = 1;
                                }

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Błędna wartość. Spróbuj jeszcze raz.");
                            }
                        }
                        chk = 0;

                    }
                }

                if (p == 1)
                {
                    Wykonaj(macierz);
                }

                if (p == 2)
                {
                    WykonajWyswietl(macierz);
                }
            }

            private static void Dzialanie_Losowo(int p, int rozmiar)
            {
                uint[,] macierz = new uint[rozmiar, rozmiar];
                Random r = new Random();

                for (int i = 0; i < rozmiar; i++)
                {
                    for (int j = 0; j < rozmiar; j++)
                    {
                        macierz[i, j] = (uint)r.Next(1, 10000);
                    }
                }

                if (p == 1)
                {
                    Wykonaj(macierz);
                }

                if (p == 2)
                {
                    WykonajWyswietl(macierz);
                }
            }

            public static void Dzialanie()
            {
                Console.WriteLine("Witaj w programie, który wykorzystuje algorytm węgierski do optymalnego przyporządkowania n zadań - n pracownikom.");
                int wybor = 0;
                int wybor2 = 0;
                int rozmiar = 0;
                int chk = 0;

                do
                {
                    Console.WriteLine("Co chcesz zrobić?\n[1] Wpisz macierz.\n[2] Wygeneruj macierz losowo.\n[3] Wyjdź.");
                    while (chk != 1)
                    {
                        try
                        {
                            wybor = Convert.ToInt32(Console.ReadLine());
                            if (wybor > 0 && wybor < 4)
                                chk++;
                            else
                                Console.WriteLine("Błędny wybór! Spróbuj jeszcze raz.");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Błędny wybór! Spróbuj jeszcze raz.");
                        }

                    }
                    chk = 0;
                    if (wybor == 3) break;

                    Console.WriteLine("[1] Wyświetl tylko wynik.\n[2] Wyświetl krok po kroku.\n[3] Wyjdź.");
                    while (chk != 1)
                    {
                        try
                        {
                            wybor2 = Convert.ToInt32(Console.ReadLine());
                            if (wybor2 > 0 && wybor2 < 4)
                                chk++;
                            else
                                Console.WriteLine("Błędny wybór! Spróbuj jeszcze raz.");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Błędny wybór! Spróbuj jeszcze raz.");
                        }

                    }
                    chk = 0;
                    if (wybor2 == 3) break;

                    Console.WriteLine("Podaj rozmiar macierzy: ");
                    while (chk != 1)
                    {
                        try
                        {
                            rozmiar = Convert.ToInt32(Console.ReadLine());
                            if (wybor2 > 0)
                                chk++;
                            else
                                Console.WriteLine("Błędny rozmiar macierzy! Spróbuj jeszcze raz.");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Błędny rozmiar macierzy! Spróbuj jeszcze raz.");
                        }
                    }
                    chk = 0;

                    switch (wybor)
                    {
                        case 1: Dzialanie_Wpisz(wybor2, rozmiar); break;
                        case 2: Dzialanie_Losowo(wybor2, rozmiar); break;
                        case 3: break;
                        default: break;
                    }

                } while (wybor != 3);
            }



            //metody do testowania
            //public static int PobierzWynik(uint[,] macierz)
            //{
            //    WebClient client = new WebClient();
            //    string adres = "http://www.hungarianalgorithm.com/solve.php?c=";
            //    for (int i = 0; i < macierz.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < macierz.GetLength(0); j++)
            //        {
            //            adres += macierz[i, j];
            //            adres += "-";
            //        }
            //        adres += "-";
            //    }
            //    //Console.WriteLine(adres);
            //    string c = client.DownloadString(adres);
            //    string wynik = "";
            //    for (int i = c.Length - 1; i > 0; i--)
            //    {
            //        if (c[i] == ' ' && c[i - 1] == 's' && c[i - 2] == 'l' && c[i - 3] == 'a')
            //        {
            //            wynik += c[i + 1];
            //            wynik += c[i + 2];
            //            if (c[i + 3] != '.')
            //                wynik += c[i + 3];
            //            break;
            //        }
            //    }
            //    return Convert.ToInt32(wynik);
            //    //Console.WriteLine("Wynik z pięknej strony internetowej: " + wynik);
            //}

            //public static void Testuj(uint[,] macierz)
            //{
            //    int zneta = 0;
            //    Console.Clear();
            //    int wynik = 0;

            //    uint[,] kopia = new uint[10, 10];

            //    for (int i = 0; i < 10; i++)
            //    {
            //        for (int j = 0; j < 10; j++)
            //        {
            //            kopia[i, j] = macierz[i, j];
            //        }
            //    }

            //    wynik = WykonajWyswietl(macierz);

            //    zneta = PobierzWynik(macierz);

            //    if (wynik == zneta)
            //        Console.WriteLine("kurła, działa");

            //    if (wynik != zneta)
            //        Console.WriteLine("kurła, nie działa");
            //}

        }
        static void Main(string[] args)
        {
            Wegier.Dzialanie();
        }
    }
}