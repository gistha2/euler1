﻿using System;
using System.Security.Cryptography;
using System.Numerics;
using System.Collections.Ge;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler_3___Largest_P_Factor
{
    class Program
    {




        /* public static bool isPrime (int number)
             {
                 int boundary = Convert.ToInt32(Math.Floor(Math.Sqrt(number)));
                 if (number == 1) return false;
                 if (number == 2) return true;

                 for (int i = 2; i <= 53; i++)
                 {
                     if (number % i == 0) return false;
                 }



                 return true;


             }
             */

        /* public static bool isPrime(int number)
          {
              int boundary = Convert.ToInt32(Math.Floor(Math.Sqrt(number)));

              if (number == 1) return false;
              if (number == 2) return true;

              for (int i = 2; i <= boundary; i++)
              {
                  if (number % i == 0) return false;
              }

              return true;
          }
        */



        public static bool IsProbablePrime(this BigInteger source, int certainty)
        {
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            // There is no built-in method for generating random BigInteger values.
            // Instead, random BigIntegers are constructed from randomly generated
            // byte arrays of the same length as the source.
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[source.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                do
                {
                    // This may raise an exception in Mono 2.10.8 and earlier.
                    // http://bugzilla.xamarin.com/show_bug.cgi?id=2761
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= source - 2);

                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }

        static void Main(string[] args)
        {


            List<int> PrimeFactors = new List<int>();
            long num;
            Console.WriteLine("Enter number to check the largest prime factor of");
            num = Convert.ToInt64(Console.ReadLine());


            for (int i = 1; i < num ; i++)
            {
                if (IsProbablePrime(i, 13195) == true)
                {
                    if (num % i == 0)
                    { PrimeFactors.Add(i); }
                }

            }
            int highestfactor = 1;


            foreach (int prime in PrimeFactors) 
            {
                if (prime>highestfactor)
                { highestfactor = prime; }

            }

            Console.WriteLine(highestfactor);
            Console.ReadLine();
               


        }
    }
}
