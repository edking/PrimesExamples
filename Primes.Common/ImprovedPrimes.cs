using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Primes.Common
{
    public class ImprovedPrimes : ICalculatePrimes
    {
        private BlockingCollection<int> _primes = new BlockingCollection<int> { 2, 3, 5, 7 };

        #region ICalculatePrimes Members

        public List<int> Execute(int minPrime, int maxPrime, int degree)
        {
            var subTasks = new Task[degree];

            int mx = _primes.Max();
            if (mx < maxPrime)
            {
                int current = mx+1;

                for (int i = 0; i < degree; i++, current++)
                {
                    int c1 = current;
                    subTasks[i] = new Task(() => { if (CheckPrime(c1)) AddToPrimesList(c1); });
                    subTasks[i].Start();
                }

                while (current < maxPrime)
                {
                    int t = Task.WaitAny(subTasks);
                    int c = current;

                    subTasks[t].Dispose();
                    subTasks[t] = new Task(() => { if (CheckPrime(c)) AddToPrimesList(c); });
                    subTasks[t].Start();

                    current++;
                }
                Task.WaitAll(subTasks);
            }
            return _primes.ToList();
        }

        public bool IsPrime(int candidate, int degree)
        {
            int mx = _primes.Max();
            if (mx >= Convert.ToInt32(Math.Sqrt(candidate)))
            {
                var t = Execute(2, candidate/2, degree);
            }
            return CheckPrime(candidate);
        }

        #endregion

        private bool CheckPrime(int val)
        {
            int s = Convert.ToInt32(Math.Sqrt(val));
            return _primes.Where(p => p <= s).All(p => (val % p != 0));
        }


        private void AddToPrimesList(int val)
        {
            _primes.Add(val);
        }
    }
}