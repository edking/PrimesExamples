using System.Collections.Generic;

namespace Primes.Common
{
    public class Primes
    {
        private readonly ICalculatePrimes _executor;
        public const int cDegreeParallelism = 4;

        public Primes(ICalculatePrimes exec, int degree = cDegreeParallelism)
        {
            DegreeParallelism = degree;
            _executor = exec;
        }

        public int DegreeParallelism { get; set; }
        public int MaxPrime { get; set; }
        public int MinPrime { get; set; }

        public List<int> GetPrimes(int minimum, int maximum)
        {
            MinPrime = minimum;
            MaxPrime = maximum;
            return _executor.Execute(MinPrime, MaxPrime, DegreeParallelism);
        }

        public bool IsPrime(int candidate)
        {
            return _executor.IsPrime(candidate, DegreeParallelism);
        }
    }
}