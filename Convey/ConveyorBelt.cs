using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Edonokaitenzushi.SushiDomain;

namespace Edonokaitenzushi.Convey
{
    public class ConveyorBelt : IConveyor, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private System.Timers.Timer _timer;

        public const int MSEC_PER_STEP = 250;

        private ObservableCollection<ConveyorSpot>? _conveyorSpots = null;

        private AutoResetEvent _gate = new AutoResetEvent(true);

        private Random _random;

        public ObservableCollection<ConveyorSpot>? ConveyorSpots
        {
            get { return _conveyorSpots; }
            private set { _conveyorSpots = value; }
        }

        private List<IConveyorObserver> _listeners = new List<IConveyorObserver>();

        private List<IConveyorObserver> _newListeners = new List<IConveyorObserver>();

        public int PositionOfHead { get; set; }
        public double FractionStep { get; set; }

        public ConveyorBelt()
        {
            _conveyorSpots = new ObservableCollection<ConveyorSpot>();

            _timer = new System.Timers.Timer(MSEC_PER_STEP);
            // Hook up the Elapsed event for the timer. 
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;

            _random = new Random();

            InitializeSpots();

            _conveyorSpots[0].Position = 0;
            _conveyorSpots[0].SushiHere = new EbiSushi();

            _conveyorSpots[1].Position = 1;
            _conveyorSpots[1].SushiHere = new TakoSushi();

            _conveyorSpots[2].Position = 2;
            _conveyorSpots[2].SushiHere = new OtoroSushi();

            OnPropertyChanged("ConveyorSpots");
        }

        public void AddListener(IConveyorObserver observer)
        {
            if (_gate.WaitOne(1000))
            {
                _newListeners.Add(observer);
                _gate.Set();
            }
        }
        public void RemoveListener(IConveyorObserver observer)
        {
            //            _listeners.Remove(observer);
        }

        public int GetOffset()
        {
            return PositionOfHead;
        }

        public Sushi TakeSushiAt(int index)
        {
            if (_random.Next(3) == 0)
            {
                int goal = (40 - PositionOfHead + index) % ConveyorMap.Count;

                Debug.WriteLine($"TAKE {goal} {PositionOfHead} {index}");

                OnPropertyChanged("ConveyorSpots");

                Sushi ret = ConveyorSpots[goal].SushiHere;

                ConveyorSpots[goal].SushiHere = new NullSushi();

                return ret;
            }

            return new NullSushi();
        }

        public bool PutSushiAt(int index, Sushi sushi)
        {
            if (sushi == null)
            {
                return false;
            }

            int goal = (40 - PositionOfHead + index) % ConveyorMap.Count;

            ConveyorSpots[goal].SushiHere = sushi;
            OnPropertyChanged("ConveyorSpots");
            return true;
        }

        private void OnTimedEvent(object? source, ElapsedEventArgs e)
        {
            if (_gate.WaitOne(0))
            {
                try
                {
                    FractionStep += 0.2;
                    if (FractionStep >= 0.5)
                    {
                        FractionStep -= 1.0;
                        if (++PositionOfHead >= ConveyorMap.Count)
                            PositionOfHead = 0;
                    }

                    //                    Debug.WriteLine($"### {_listeners.Count}");

                    _listeners.AddRange(_newListeners);

                    _newListeners.Clear();

                    foreach (var spot in _listeners)
                    {
                        spot.OnBeltMoved();
                    }

                    for (int i = _listeners.Count - 1; i >= 0; i--)
                    {
                        if (_listeners[i].DoRemove)
                        {
                            _listeners.RemoveAt(i);
                        }
                    }
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

        private void InitializeSpots()
        {
            for (int i = 0; i < ConveyorMap.Count; i++)
            {
                _conveyorSpots.Add(new ConveyorSpot(this) { SushiHere = new NullSushi(), Position = i });
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
