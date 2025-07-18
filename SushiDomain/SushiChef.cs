using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Edonokaitenzushi.Convey;

namespace Edonokaitenzushi.SushiDomain
{
    public class SushiChef
    {
        private System.Timers.Timer _timer;

        private Random _random = new Random();

        private IConveyor? _belt = null;

        private AutoResetEvent _gate = new AutoResetEvent(true);


        public SushiChef(IConveyor belt)
        {
            _belt = belt;

            _timer = new System.Timers.Timer(3000);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += OnPrepareSushi;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnPrepareSushi(object source, ElapsedEventArgs e)
        {
            if (_gate.WaitOne(0))
            {
                try
                {
                    Sushi sushi = SushiFactory.CreateSushi();

                    _ = _belt.PutSushiAt(0, sushi);
                }
                catch (Exception ex) 
                { 
                    Debug.WriteLine(ex.ToString());
                }    
                finally
                {
                    _gate.Set();
                }
            }
        }
    }
}
