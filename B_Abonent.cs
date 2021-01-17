using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELGAmalConsole
{
    static class B_Abonent
    {
        static private long g;
        static private long p;
        static private long x;
        static private long y;
        static private long k;

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
            PrintLog($"Открытый ключ: (y,g,p) = ({y},{g},{p}).", true);
        }

        static public void PrintPrivateKeys()
        {
            PrintLog($"Закрытый ключ: х = {x}.", true);
        }

        static public void SendOpenKeys()
        {
            A_Abonent.KeyAbonentY = y;
            A_Abonent.KeyAbonentG = g;
            A_Abonent.KeyAbonentP = p;
        }

        static public void PrintLog(string str, bool endl)
        {
            if (endl)
                Console.WriteLine("| Абонент B | " + str);
            else
                Console.Write("| Абонент B | " + str);
        }

        static public bool isNormalE(long e)
        {
            return (ELGAmal.GCD(k, p - 1) == 1) ? true : false;
        }

        static public void SignatureGeneration()
        {
            do
            {
                A_Abonent.PrintLog("Введите k: ", false);
                k = Convert.ToInt64(Console.ReadLine());
            }
            while (!A_Abonent.isNormalE(k));
        }

        static public void verificationOfDigitalSignature(long a, long[] b, long[]NumberEncrypt)
        {
            long[] LeftPart = new long[b.Length];//Левая часть
            long[] RightPart = new long[b.Length];//Правая часть

            PrintLog("Левая часть: ",false);
            for (int i = 0; i < b.Length; i++)
            {
                RightPart[i] = (long)ELGAmal.reSquaring(KeyAbonentG, NumberEncrypt[i], KeyAbonentP);
                Console.Write(RightPart[i] + " ");
            }
            Console.WriteLine();

            PrintLog("Правая часть: ",false);
            for (int i = 0; i < b.Length; i++)
            {
                long temp = (long)(ELGAmal.reSquaring(KeyAbonentY, a, KeyAbonentP) * ELGAmal.reSquaring(a, b[i], KeyAbonentP)); 
                LeftPart[i] = ELGAmal.Mod(temp, KeyAbonentP);
                Console.Write(RightPart[i]+" ");
            }
            Console.WriteLine("\nПроверка показала идентичность ЭЦП и открытой подписи!");
        }
    }
}
