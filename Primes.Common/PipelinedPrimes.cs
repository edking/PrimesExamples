using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Primes.Common
{
    public class PipelinedPrimes : ICalculatePrimes
    {
        private BlockingCollection<int> _candidates;
        private BlockingCollection<int> _primes;

        #region ICalculatePrimes Members

        public List<int> Execute(int minPrime, int maxPrime, int degree)
        {
            _primes = new BlockingCollection<int> {2, 3, 5, 7};
            _candidates = new BlockingCollection<int>();
            var subTasks = new Task[degree];

            for (int i = 0; i < degree; i++)
            {
                subTasks[i] = new Task(() => SubTask(_candidates, _primes));
                subTasks[i].Start();
            }

            for (int c = 8; c < maxPrime; c++) _candidates.Add(c);
            _candidates.CompleteAdding();

            Task.WaitAll(subTasks);

            return _primes.ToList();
        }

        public bool IsPrime(int candidate, int degree)
        {
            int mx = _primes.Max();
            if (mx >= Convert.ToInt32(Math.Sqrt(candidate)))
            {
                var t = Execute(2, candidate / 2, degree);
            }
            return CheckPrime(candidate);
        }

        #endregion

        private void SubTask(BlockingCollection<int> input, BlockingCollection<int> output)
        {
            try
            {
                foreach (
                    var c in
                        input.GetConsumingEnumerable()
                            .Where(CheckPrime))
                {
                    output.Add(c);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private bool CheckPrime(int val)
        {
            int s = Convert.ToInt32(Math.Sqrt(val));
            return _primes.Where(p => p <= s).All(p => (val%p != 0));
        }
    }
}