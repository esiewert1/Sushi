using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using FPoint = Edonokaitenzushi.Convey.ConveyorMap.FPoint;
using Edonokaitenzushi.SushiDomain;

namespace Edonokaitenzushi.Convey
{
    public class ConveyorSpot : IConveyorObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        public bool DoRemove { get; private set; }

        private IConveyor _conveyor;

        public ConveyorSpot(IConveyor conveyor)
        {
            _conveyor = conveyor;
            _conveyor.AddListener(this);
        }

        private Sushi? _sushi = null;

        public Sushi? SushiHere
        {
            get
            {
                return _sushi;
            }
            set
            {
                if (_sushi != value)
                {
                    if (value is NullSushi && _sushi is NullSushi)
                        Debug.WriteLine("To Null " + _sushi.GetType().Name);

                    _sushi = value;


                    OnPropertyChanged("SushiHere");
                }
            }
        }

        public int Position { get; set; }

        public int ConveyorPosition { get; set; }
        public double FractionStep { get; set; }


        public double X
        {
            get
            {
                int index = (ConveyorPosition + Position) % ConveyorMap.Markers.Count;
                if (FractionStep >= 0)
                {
                    return ConveyorMap.Markers[index].X + FractionStep * ConveyorMap.Markers[index].PostDX;
                }
                return ConveyorMap.Markers[index].X + FractionStep * ConveyorMap.Markers[index].PreDX;
            }
        }

        public double Y
        {
            get
            {
                int index = (ConveyorPosition + Position) % ConveyorMap.Markers.Count;
                if (FractionStep >= 0)
                {
                    return ConveyorMap.Markers[index].Y + FractionStep * ConveyorMap.Markers[index].PostDY;
                }
                return ConveyorMap.Markers[index].Y + FractionStep * ConveyorMap.Markers[index].PreDY;
            }
        }

        void IConveyorObserver.OnBeltMoved()
        {
            ConveyorPosition = _conveyor.PositionOfHead;
            FractionStep = _conveyor.FractionStep;
            OnPropertyChanged("X");
            OnPropertyChanged("Y");
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
