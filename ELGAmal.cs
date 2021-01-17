using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ELGAmalConsole
{
    static class ELGAmal
    {
		static private string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		static private long[] numericConversion;

		static public bool IsPrimeNumber(long n)//Проверка на простоту
		{
			bool result = true;

			if (n > 1)
			{
				for (var i = 2u; i < n; i++)
				{
					if (n % i == 0)
					{
						result = false;
						break;
					}
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		static public BigInteger reSquaring(BigInteger m, BigInteger e, BigInteger n)//c=m^e(mod n)
		{
			BigInteger E = e;

			long i;
			for (i = 1; E != 1; i++)//Проверка количества элементов
				E = E / 2;

			BigInteger[] bynaryN = new BigInteger[i];

			E = e;
			for (long j = 0; j < i; j++)//Степень в бинарном представлении
			{
				bynaryN[j] = E % 2;
				E = E / 2;
			}

			BigInteger b = m % n;
			for (long j = 1; j < i; j++)//Возведение в степень
			{
				b = BigInteger.ModPow(b, 2, n);
				if (bynaryN[j] == 1)
					m = (m * b) % n;
			}
			return m;
		}

		static public long Mod(long a,long b)
		{
			if (a < 0)
			{
				a *= -1;
				a = a % b;
				a = b - a;
				a = a % b;
				return a;
			}
			else
			{
				return a % b;
			}
		}

		static public long[] TextToNumberEncrypt(string Text)//Текст в набор чисел
		{
			numericConversion = new long[Text.Length];
			for (int i = 0; i < Text.Length; i++)//Числовой вид сообщения
			{
				for (int j = 0; j < Alphabet.Length; j++)
				{
					if (Text[i] == Alphabet[j])
					{
						numericConversion[i] = j; break;
					}
				}
			}
			return numericConversion;
		}

		static public void PrintNumberEncrypt(long[]NumberEncrypt,bool Abonent)
		{
			if(Abonent)
				A_Abonent.PrintLog($"Текст в числовом представлении: ", false);
			else
				B_Abonent.PrintLog($"Текст в числовом представлении: ", false);
			foreach (var item in NumberEncrypt)
				Console.Write($"{item} ");
			Console.WriteLine();
		}

		static public long GCD(long A, long B)//Поиск НОД | Алгоритм Евклида
		{
			while (B != 0)
				(A, B) = (B, A % B);
			return A;
		}

		static public void extendedGCD(long a, long b, out long x, out long y, out long d)//Расширенный алгоритм Евклида
		{
			long q, r, x1, x2, y1, y2;


			if (b == 0)
			{
				d = a;
				x = 1;
				y = 0;
				return;
			}
			x2 = 1; y2 = 0;
			x1 = 0; y1 = 1;

			while (b > 0)
			{
				q = a / b; r = a - q * b;
				x = x2 - q * x1; y = y2 - q * y1;
				a = b; b = r;
				x2 = x1; x1 = x; y2 = y1; y1 = y;
			}
			d = a; x = x2; y = y2;
		}
	}
}
