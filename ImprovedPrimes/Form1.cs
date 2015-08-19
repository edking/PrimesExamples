using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImprovedPrimes
{
    public partial class Form1 : Form
    {
        private Task _mainTask;
        private List<int> _primes = new List<int> {2, 3, 5, 7};
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _results.Items.Clear();

            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            _mainTask = new Task(MainTask, _token);
            _mainTask.Start();
            statusStrip1.Items[0].Text = "Scan started.";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_token.CanBeCanceled) _tokenSource.Cancel();
            statusStrip1.Items[0].Text = "Scan cancelled.";
        }

        private void MainTask()
        {
            DoTheWork(new Primes.Common.ImprovedPrimes());
        }

        private void DoTheWork(Primes.Common.ICalculatePrimes calculator)
        {
            var sw = new Stopwatch();

            int max = Convert.ToInt32(bxHowMany.Text);
            int threadCount = Convert.ToInt32(bxThreads.Text);

            sw.Start();
            var p = new Primes.Common.Primes(calculator, threadCount);
            _primes = p.GetPrimes(2, max);
            sw.Stop();

            ShowPrimes();
            statusStrip1.Invoke(
                (MethodInvoker)
                    (() =>
                    {
                        statusStrip1.Items[0].Text = String.Format("Found {0:N0} primes in {1:N0} s.", _primes.Count,
                            sw.ElapsedMilliseconds / 1000);
                    }));
        }

        private void ShowPrimes()
        {
            var disp = _primes.ToList();
            disp.Sort();

            var res = disp.Select(x => new ListViewItem(x.ToString())).ToArray();

            _results.Invoke((MethodInvoker) (() => { _results.Items.AddRange(res); }));
        }
    }
}