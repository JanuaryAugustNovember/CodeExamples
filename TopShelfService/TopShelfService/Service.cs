using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TopShelfService
{
    public class Service
    {
        private static CancellationTokenSource _tokenSource;

        public void Start()
        {
            var tasks = new List<Task>();

            var taskOne = new Task(() => PollData(_tokenSource.Token, 15000), _tokenSource.Token, TaskCreationOptions.LongRunning);
            var taskTwo = new Task(() => PollData(_tokenSource.Token, 30000), _tokenSource.Token, TaskCreationOptions.LongRunning);

            tasks.Add(taskOne);
            tasks.Add(taskTwo);

            Task.Run(() => taskOne);

            Task.Factory.StartNew(() =>
            {
                PollData(_tokenSource.Token, 15000);
            });

            // await this?
            Task.WhenAll(tasks);
        }

        public void Stop()
        {
            _tokenSource.Cancel();
        }


        private void PollData(CancellationToken token, int delayInMs)
        {
            while (!token.IsCancellationRequested)
            {

            }
        }
    }
}
