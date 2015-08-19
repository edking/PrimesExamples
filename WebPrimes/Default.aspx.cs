using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using WebPrimes.PrimesService;

namespace WebPrimes
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            var val = Convert.ToInt32(candidate.Text);
            var client = new PrimesServiceClient();
            var rv = client.IsPrime(val);
            result.Text = String.Format("{0} IS {1}PRIME.", val, (rv ? String.Empty : "NOT "));
        }

        protected void FindPrimesButton_Click(object sender, EventArgs e)
        {
            var lval = Convert.ToInt32(lowBound.Text);
            var hval = Convert.ToInt32(highBound.Text);

            if (lval < hval)
            {
                var prList = new List<int>();
                for (int i = lval; i <= hval; i++)
                {
                    var client = new PrimesServiceClient();
                    bool isPrime = client.IsPrime(i);
                    if (isPrime) prList.Add(i);
                    //                   client.Close();
                }

//                var client = new PrimesServiceClient();
//                prList.AddRange(client.GetPrimesBetween(lval,hval));                
                primesList.DataSource = prList.Select(p => new {value = p});
                primesList.DataBind();
            }
        }
    }
}