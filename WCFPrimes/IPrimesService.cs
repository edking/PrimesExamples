using System.Collections.Generic;
using System.ServiceModel;

namespace WCFPrimes
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPrimesService" in both code and config file together.
    [ServiceContract]
    public interface IPrimesService
    {
        [OperationContract]
        bool IsPrime(int value);

        [OperationContract]
        List<int> GetPrimes(int value);

        [OperationContract]
        List<int> GetPrimesBetween(int low, int high);
    }
}