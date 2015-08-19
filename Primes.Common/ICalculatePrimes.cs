using System.Collections.Generic;

namespace Primes.Common
{
    public interface ICalculatePrimes
    {
        List<int> Execute(int minPrime, int maxPrime, int degree);
        bool IsPrime(int candidate, int degree);
    }
}