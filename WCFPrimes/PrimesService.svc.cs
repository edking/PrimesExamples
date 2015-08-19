using System;
using System.Collections.Generic;
using System.Linq;
using Primes.Common;
namespace WCFPrimes
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PrimesService" in code, svc and config file together.
    public class PrimesService : IPrimesService
    {
        public const int cDegreeParallelism = 4;
        private Primes.Common.Primes primes = new Primes.Common.Primes(new PLINQPrimes(), cDegreeParallelism);

        #region IPrimesService Members

        public bool IsPrime(int value)
        {
            return primes.IsPrime(value);
        }

        public List<int> GetPrimes(int value)
        {
            return primes.GetPrimes(2, value);
        }

        public List<int> GetPrimesBetween(int low, int high)
        {
            return primes.GetPrimes(low, high);
        }

        #endregion
    }
}