﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// per settare i comuni in un form
//String[] comuni;
//String[] codCatastale;
//comuni = File.ReadAllLines("C:/Users/a.dumitru/Downloads/listacomuni.txt");
//           codCatastale = File.ReadAllLines("C:/Users/a.dumitru/Downloads/codCatastale.txt");


namespace CalcoloCodiceFiscale.CF_Classes
{
    static class CalcoloCF
    {
        public static void Calcolo(Utente u)
        {
            string surname = Cognome(u.Cognome);
            string name = Nome(u.Nome);
            string year = u.DataNascita.Year.ToString().Substring(2, 2);
            string month = Month(u.DataNascita.Month);
            string day = Day(u.DataNascita.Day, u.Sesso);
            string comune = Comune(u.Comune);

            string y = String.Join("", surname, name, year, month, day, comune);
            string controlChar = ControlChar(y);

            y = String.Join("", y, controlChar);

            Console.WriteLine(y);
        }

        private static string Cognome(string s) {
            string cognome = s.ToUpper();
            if (cognome.Length < 2)
            {
                return "err";
            }
            else if (cognome.Length < 3)
            {
                return (cognome += "x").ToUpper();
            }
            else
            {
                char[] strSplit = cognome.ToCharArray();
                char[] c = new char[3];
                int j = 0;
                for (int i = 0; i < strSplit.Length && j < 3; i++)
                {
                    if (strSplit[i] != 'A' && strSplit[i] != 'E' && strSplit[i] != 'I' && strSplit[i] != 'O' && strSplit[i] != 'U')
                    {
                        c[j] = strSplit[i];
                        j++;
                    }
                }
                string sC = new string(c);
                if (sC.Length < 3)
                {
                    for (int k = 0, l = sC.Length - 1; k < strSplit.Length || l > 3; k++, l++)
                    {
                        if (strSplit[k] == 'A' || strSplit[k] == 'E' || strSplit[k] == 'I' || strSplit[k] == 'O' || strSplit[k] == 'U')
                        {
                            c[l] = strSplit[k];
                        }
                    }
                    return sC = string.Join("", c);
                } else
                {
                    return sC;
                }
            }

        }

        private static string Nome(string s)
        {
            string nome = s.ToUpper();
            if (nome.Length < 2)
            {
                return "err";
            }
            else if (nome.Length < 3)
            {
                return (nome += "x").ToUpper();
            }
            else
            {
                char[] strSplit = nome.ToCharArray();
                char[] temp = new char[strSplit.Length];
                char[] c = new char[3];
                int j = 0;
                string sC;
                for (int i = 0; i < strSplit.Length; i++)
                {
                    if (strSplit[i] != 'A' && strSplit[i] != 'E' && strSplit[i] != 'I' && strSplit[i] != 'O' && strSplit[i] != 'U')
                    {
                        temp[i] = strSplit[i];
                        j++;
                    }
                }
                if (j < 3)
                {
                    for (int i = 0; i<j; i++)
                    {
                        c[i] = temp[i];
                    }
                    for (int k = 0, l = j; k < strSplit.Length || l > 3; k++, l++)
                    {
                        if (strSplit[k] == 'A' || strSplit[k] == 'E' || strSplit[k] == 'I' || strSplit[k] == 'O' || strSplit[k] == 'U')
                        {
                            c[l] = strSplit[k];
                        }
                    }
                    return sC = string.Join("", c);
                }
                else if (j == 3)
                {
                    for (int i = 0; i < j; i++)
                    {
                        c[i] = temp[i];
                    }
                    return sC = string.Join("", c);

                }
                else if (j > 3)
                {
                    c[0] = temp[0];
                    c[1] = temp[1];
                    c[2] = temp[3];
                    return sC = string.Join("", c);
                }

            }
        }

        private static string Month(int i)
        {
            Dictionary<int, string> d = new Dictionary<int, string>();
            d.Add(01, "A");
            d.Add(02, "B");
            d.Add(03, "C");
            d.Add(04, "D");
            d.Add(05, "E");
            d.Add(06, "H");
            d.Add(07, "L");
            d.Add(08, "M");
            d.Add(09, "P");
            d.Add(10, "R");
            d.Add(11, "S");
            d.Add(12, "T");
            return d.First(x => x.Key == i).Value;
        }

        private static string Day(int i, string s)
        {
            if (s.ToUpper() == "F")
            {
                int d = i + 40;
                return i.ToString();
            }
            else if (s.ToUpper() == "M"){
                return i.ToString();
            } else
            {
                return "err";
            }
        }

        private static string Comune(string c)
        {
            Dictionary<string, string> d = new Dictionary<string, string>()
            {
                { "Torino", "L219" }, {"Milano", "F205"}, {"Napoli", "F839" }, {"Potenza", "G942"}
            };
            try
            {
                return d.First(x => x.Key == c).Value;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private static string ControlChar(string s)
        {
            char[] x = s.ToUpper().ToCharArray();
            char[] pari = new char[8];
            char[] dispari = new char[8];
            int ip = 0, id = 0, cni=0;
            for (int i=0; i<x.Length; i++)
            {
                if (i % 2 == 0)
                {
                    pari[ip] = x[i];
                    ip++;
                } else
                {
                    dispari[id] = x[i];
                    id++;
                }
            }
            Dictionary<string, int> dDispari = new Dictionary<string, int>(){
                {"0", 1}, {"1", 0}, {"2", 5}, {"3", 7}, {"4", 9}, {"5", 13}, {"6", 15}, {"7", 17},
                {"8", 19}, {"9", 21},{"A", 1},{"B", 0},{"C", 5},{"D", 7},{"E", 9},{"F", 13},{"G", 15},
                {"H", 17},{"I", 19},{"J", 21},{"K", 2},{"L", 4},{"M", 18},{"N", 20},{"O", 11},{"P", 3},{"Q", 6},
                {"R", 8},{"S", 12},{"T", 14},{"U", 16},{"V", 10},{"W", 22},{"X", 25},{"Y", 24},{"Z", 23}
            };
            Dictionary<string, int> dPari = new Dictionary<string, int>(){
                {"0", 0}, {"1", 1}, {"2", 2}, {"3", 3}, {"4", 4}, {"5", 5}, {"6", 6}, {"7", 7},
                {"8", 8}, {"9", 9},{"A", 0},{"B", 1},{"C", 2},{"D", 3},{"E", 4},{"F", 5},{"G", 6},
                {"H", 7},{"I", 8},{"J", 9},{"K", 10},{"L", 11},{"M", 12},{"N", 13},{"O", 14},{"P", 15},{"Q", 16},
                {"R", 17},{"S", 18},{"T", 19},{"U", 20},{"V", 21},{"W", 22},{"X", 23},{"Y", 24},{"Z", 25}
            };
            Dictionary<int, string> dCni = new Dictionary<int, string>(){
                {0, "A"}, {1, "B"}, {2, "C"}, {3, "D"}, {4, "E"}, {5, "F"}, {6, "G"}, {7, "H"},
                {8, "I"}, {9, "J"},{10, "K"},{11, "L"},{12, "M"},{13, "N"},{14, "O"},{15, "P"},{16, "Q"},
                {17, "R"},{18, "S"},{19, "T"},{20, "U"},{21, "V"},{22, "W"},{23, "X"},{24, "Y"},{25, "Z" }
            };
            for (int i = 0; i<ip;i++)
            {
                cni += dPari.First(z => z.Key.Equals(pari[i].ToString())).Value;

            }
            for (int i = 0; i < id; i++)
            {
                cni += dDispari.First(z => z.Key.Equals(dispari[i].ToString())).Value;

            }
            cni %= 26;
            return dCni.First(z => z.Key == cni).Value;
        }

    }
}
