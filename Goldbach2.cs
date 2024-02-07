using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goldbach2
{
    internal class Program
    {
        private static readonly ConcurrentQueue<int> Primes = new();
        private static void FindPrimes()
        {
            bool isprime;
            for (int i = 2; i <= int.MaxValue; i++)
            {
                isprime = true;
                if (i % 2 == 0)
                    isprime ^= true;
                else for (int j = 3; j <= (int)Math.Floor(Math.Sqrt(i) + 1) && isprime; j += 2)
                    {
                        if (i % j == 0)
                            isprime ^= true;
                    }
                if (isprime)
                    Primes.Enqueue(i);
            }
        }
        private static void TestGoldBachs()
        {
            bool isgb = false;
            for (int i = 4; i < int.MaxValue; i += 2, isgb = false)
                for (int j = 0; j <= int.MaxValue && !isgb; j++)
                    for (int k = j; k <= int.MaxValue && !isgb; k++)
                    {
                        int total = 0, count = 0;
                        while (true)
                        {
                            count = Primes.Count;
                            if (j < count && k < count)
                            {
                                total = Primes.ElementAt(j) + Primes.ElementAt(k);
                                break;
                            }
                            Console.WriteLine("New prime numbers are waiting to be discovered");
                        }

                        if (total > i)
                            break;
                        else if (total == i)
                        {
                            if(i % 1000 == 0)
                            Console.WriteLine($"{i} is a goldbach number{count}");
                            isgb = true;
                        }
                    }
        }
        private static void Main_(string[] args)
        {
            Primes.Enqueue(2);
            var taskgold = new Task(() => TestGoldBachs());
            var taskprime = new Task(() => FindPrimes());
            taskgold.Start();
            taskprime.Start();
            Task.WaitAll(taskgold, taskprime);
        }
    }
}
