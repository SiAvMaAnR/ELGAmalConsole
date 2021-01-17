using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELGAmalConsole
{
    static class A_Abonent
    {
        static private long g;
        static private long p;
        static private long x;
        static private long y;
        static private long k;

        static public long a;
        static public long[] b;

        static public long KeyAbonentY;
        static public long KeyAbonentG;
        static public long KeyAbonentP;

        static public void EnterG()
        {
            do
            {
                PrintLog($"Введите g (g < {p}): ", false);
                g = Convert.ToInt32(Console.ReadLine());
            }
            while (!(ELGAmal.IsPrimeNumber(g) && (g < p)));
        }

        static public void EnterX()
        {
            do
            {
                PrintLog($"Введите x (x < {p}): ", false);
                x = Convert.ToInt32(Console.ReadLine());
            }
            while (!(ELGAmal.IsPrimeNumber(x) && (x < p)));
        }

        static public void SetP(int P)
        {
            p = P;
        }

        static public void CalculateY()
        {
            y = (long)ELGAmal.reSquaring(g, x, p);
        }

        static public void PrintOpenKeys()
        {
            PrintLog($"Открытый ключ: (y,g,p) = ({y},{g},{p}).",true);
        }
        
        static public void PrintPrivateKeys()
        {
            PrintLog($"Закрытый ключ: х = {x}.",true);
        }

        static public void SendOpenKeys()
        {
            B_Abonent.KeyAbonentY = y;
            B_Abonent.KeyAbonentG = g;
            B_Abonent.KeyAbonentP = p;
        }

        static public void PrintLog(string str, bool endl)
        {
            if (endl)
                Console.WriteLine("| Абонент A | " + str);
            else
                Console.Write("| Абонент A | " + str);
        }

        static public bool isNormalE(long e)
        {
            return (ELGAmal.GCD(k, p - 1) == 1) ? true : false;
        }

        static public void EnterK()
        {
            do
            {
                A_Abonent.PrintLog("Введите k: ", false);
                k = Convert.ToInt64(Console.ReadLine());
            }
            while (!A_Abonent.isNormalE(k));
        }

        static public void FindFirstPart()
        {
            a = (long)ELGAmal.reSquaring(g, k, p);
            PrintLog($"а = {a}", false);
        }
        
        static public void PrintB(long []b)
        {
            Console.Write("\tb[] = ");
            foreach (var item in b)
            {
                Console.Write(item+" ");
            }
            Console.WriteLine();
        }

        static public void FindSecondPart()
        {
            b = new long[Program.NumberEncrypt.Length];
            ELGAmal.extendedGCD(k, p - 1, out long t1, out long t2, out long d);
            for (int i = 0; i < b.Length; i++)
            {
                long firstPart = t1 + p - 1;
                long secondPart = ELGAmal.Mod(Program.NumberEncrypt[i]+1 - x * a, p - 1);
                //Console.WriteLine(firstPart + " " + secondPart);

                b[i] = ELGAmal.Mod(firstPart*secondPart,p-1);
                
                //Console.WriteLine(b[i]);
            }
            PrintB(b);
        }

        static public void SignatureGeneration()
        {
            EnterK();
            FindFirstPart();
            FindSecondPart();
        }
    }
}
