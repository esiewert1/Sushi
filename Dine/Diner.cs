using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FPoint = Edonokaitenzushi.Convey.ConveyorMap.FPoint;
using Edonokaitenzushi.SushiDomain;
using Edonokaitenzushi.Convey;

namespace Edonokaitenzushi.Dine
{
    public class Diner : IConveyorObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private const int SUSHI_STEPS = 20;

        private IConveyor _conveyor;
        private Random _random;
        private int _billInt;
        private System.Timers.Timer _eatingTimer;

        private double _x;
        private double _y;
        private double _sushiX;
        private double _sushiY;
        private double _moneyX = 0;
        private double _moneyY = 0;

        private double _sushiWidth;
        private double _sushiHeight;

        private Sushi? _currentlyEating;

        private FPoint? _sushiStart = null;
        private FPoint? _sushiEnd = null;
        private int _sushiStep;

        public Diner(IConveyor conveyor)
        {
            Debug.WriteLine("New Diner");
            _random = new Random();
            _conveyor = conveyor;
            _conveyor.AddListener(this);

            _eatingTimer = new System.Timers.Timer(10000);
            _eatingTimer.AutoReset = false;
            _eatingTimer.Enabled = true;

            BitmapImage = null;
        }

        public virtual bool DoRemove { get; protected set; }

        public virtual double X
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged("X");
                }
            }
        }

        public virtual double Y
        {
            get { return _y; }
            set
            {
                if (_x != value)
                {
                    _y = value;
                    OnPropertyChanged("Y");
                }
            }
        }

        public virtual double SushiX
        {
            get { return _sushiX; }
            set
            {
                if (_sushiX != value)
                {
                    _sushiX = value;
                    OnPropertyChanged("SushiX");
                }
            }
        }

        public virtual double SushiY
        {
            get { return _sushiY; }
            set
            {
                if (_sushiY != value)
                {
                    _sushiY = value;
                    OnPropertyChanged("SushiY");
                }
            }
        }

        public virtual double MoneyX
        {
            get { return _moneyX; }
            set
            {
                if (_moneyX != value)
                {
                    _moneyX = value;
                    OnPropertyChanged("MoneyX");
                }
            }
        }

        public virtual double MoneyY
        {
            get { return _moneyY; }
            set
            {
                if (_moneyY != value)
                {
                    _moneyY = value;
                    OnPropertyChanged("MoneyY");
                }
            }
        }

        public virtual int BillInt
        {
            get { return _billInt; }
            set
            {
                if (_billInt != value)
                {
                    _billInt = value;
                    OnPropertyChanged("BillInt");
                    OnPropertyChanged("BillString");
                }
            }
        }

        public virtual string BillString
        {
            get { return BillInt.ToString() + "円"; }
        }

        public virtual double SushiWidth
        {
            get { return _sushiWidth; }
            set
            {
                if (_sushiWidth != value)
                {
                    _sushiWidth = value;
                    OnPropertyChanged("SushiWidth");
                }
            }
        }

        public virtual double SushiHeight
        {
            get { return _sushiHeight; }
            set
            {
                if (_sushiHeight != value)
                {
                    _sushiHeight = value;
                    OnPropertyChanged("SushiHeight");
                }
            }
        }

        public virtual BitmapImage? BitmapImage
        {
            get;
            set;
        }

        public virtual ImageSource SushiImage
        {
            get
            {
                return _currentlyEating?.SushiImage;
            }
        }

        private int _seatNumber;

        public virtual int SeatNumber
        {
            get
            {
                return _seatNumber;
            }
            set
            {
                _seatNumber = value;
                Debug.WriteLine($"Seat {value}");
                if (value == 0)
                {
                    Console.WriteLine();
                }
            }
        }

        private bool EatingState
        {
            get
            {
                return _currentlyEating != null;
            }
        }

        public virtual void OnBeltMoved()
        {
            if (EatingState)
            {
                _sushiStep++;
                SushiX = _sushiStart.X + _sushiStep * (_sushiEnd.X - _sushiStart.X) / SUSHI_STEPS;
                SushiY = _sushiStart.Y + _sushiStep * (_sushiEnd.Y - _sushiStart.Y) / SUSHI_STEPS;

                OnPropertyChanged("SushiImage");

                if (_sushiStep >= SUSHI_STEPS)
                {
                    _currentlyEating = null;
                }
            }

            if (Math.Abs(_conveyor.FractionStep) < 0.000001)
            {
                if (!_eatingTimer.Enabled)
                {
                    Sushi sushi = _conveyor.TakeSushiAt(SeatNumber);

                    Debug.WriteLine("Take Sushi " + sushi.GetType().Name + " " + SeatNumber);

                    if (!(sushi is NullSushi))
                    {
                        if (SeatNumber == 0)
                        {
                            Console.WriteLine();
                        }

                        _sushiStep = 0;
                        _sushiStart = new FPoint(ConveyorMap.Markers[SeatNumber].X, ConveyorMap.Markers[SeatNumber].Y);
                        _sushiEnd = new FPoint(ConveyorMap.Markers[SeatNumber].DinerX, ConveyorMap.Markers[SeatNumber].DinerY);
                        SushiX = _sushiStart.X + _sushiStep * (_sushiEnd.X - _sushiStart.X) / SUSHI_STEPS;
                        SushiY = _sushiStart.Y + _sushiStep * (_sushiEnd.Y - _sushiStart.Y) / SUSHI_STEPS;
                        SushiWidth = ConveyorMap.ICON_SIZE;
                        SushiHeight = ConveyorMap.ICON_SIZE;
                        _currentlyEating = sushi;
                        _eatingTimer.Enabled = true;
                        BillInt += sushi.Price;

                        OnPropertyChanged("X");
                        OnPropertyChanged("Y");
                        OnPropertyChanged("SushiX");
                        OnPropertyChanged("SushiY");
                        OnPropertyChanged("BillString");
                        OnPropertyChanged("SushiImage");
                    }
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual void Cleanup()
        {
            DoRemove = true;
        }
    }
}
