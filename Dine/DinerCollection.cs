using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;
using Edonokaitenzushi.Convey;

namespace Edonokaitenzushi.Dine
{
    public class DinerCollection : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public const int MSEC_PER_STEP = 5000;
        private System.Timers.Timer _timer;

        private Random _random = new Random();

        private IConveyor _conveyor;

        private AutoResetEvent _gate = new AutoResetEvent(true);

        public ObservableCollection<Diner> DinerList { get; set; } = new ObservableCollection<Diner>();

        public DinerCollection(IConveyor conveyor)
        {
            _conveyor = conveyor;

            for (int i = 0; i < ConveyorMap.Count; i++)
            {
                DinerList.Add(new NullDiner(conveyor)
                { SeatNumber = i });
            }


            _timer = new System.Timers.Timer(MSEC_PER_STEP);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += OnAddDiner;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnAddDiner(object? source, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (_gate.WaitOne(0))
                {
                    try
                    {
                        int choice = _random.Next(ConveyorMap.Count - 1) + 1;

                        Diner diner = DinerList[choice];

                        if (diner is NullDiner)
                        {
                            var newDiner = DinerFactory.Create(_conveyor);
                            newDiner.SeatNumber = choice;
                            newDiner.X = ConveyorMap.Markers[choice].DinerX;
                            newDiner.Y = ConveyorMap.Markers[choice].DinerY;
                            newDiner.MoneyX = ConveyorMap.Markers[choice].DinerX;
                            newDiner.MoneyY = ConveyorMap.Markers[choice].DinerY;
                            DinerList[choice] = newDiner;
                        }
                        else
                        {
                            // diner exits
                            int seat = DinerList[choice].SeatNumber;
                            DinerList[choice].Cleanup();
                            DinerList[choice] = new NullDiner(_conveyor);
                            DinerList[choice].SeatNumber = seat;
                        }
                        OnPropertyChanged("DinerList");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        _gate.Set();
                    }
                }
            }));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
