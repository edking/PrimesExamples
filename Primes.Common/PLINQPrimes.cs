using System;
using System.Collections.Generic;
using System.Linq;

namespace Primes.Common
{
    public class PLINQPrimes : ICalculatePrimes
    {
        #region ICalculatePrimes Members

        public List<int> Execute(int minPrime, int maxPrime, int degree)
        {
            return Enumerable.Range(8, maxPrime).AsParallel()
                .WithDegreeOfParallelism(degree)
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Where(x => Enumerable.Range(2, (int) Math.Floor(Math.Sqrt(x)))
                    .All(y => x%y != 0)).Concat(new[] {2, 3, 5, 7}.AsParallel())
                .Where(n => n <= maxPrime && n >= minPrime).OrderBy(x => x).ToList();
        }

        public bool IsPrime(int candidate, int degree)
        {
            return Enumerable.Range(2, (int)Math.Floor(Math.Sqrt(candidate)))
        .AsParallel()
        .WithDegreeOfParallelism(degree)
        .All(p => (candidate % p != 0));
        }

        #endregion
    }
}