using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Edonokaitenzushi.Convey.ConveyorSpot;

namespace Edonokaitenzushi.Convey
{
    public class ConveyorMap
    {
        public class Marker
        {
            public double X;
            public double Y;
            public double PreDX;
            public double PreDY;
            public double PostDX;
            public double PostDY;
            public double DinerX;
            public double DinerY;
        }

        public class FPoint
        {
            public double X;
            public double Y;
            public FPoint(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        public const double ICON_SIZE = 40;

        private static readonly ConveyorMap _conveyorMap;

        private List<Marker> _markers = new List<Marker>();


        static ConveyorMap()
        {
            _conveyorMap = new ConveyorMap();
            Initialize();
        }

        public static List<Marker> Markers
        {
            get => _conveyorMap._markers;
        }

        public static int Count
        {
            get
            {
                return _conveyorMap._markers.Count;
            }
        }

        private static void Initialize()
        {
            {
                for (int i = 0; i <= 4; i++)
                {
                    var marker = new Marker
                    {
                        X = ICON_SIZE * 11,
                        Y = ICON_SIZE * 5 - i * ICON_SIZE,
                    };
                    marker.DinerX = marker.X + ICON_SIZE;
                    marker.DinerY = marker.Y;
                    _conveyorMap._markers.Add(marker);
                }

                for (int i = 0; i <= 9; i++)
                {
                    var marker = new Marker
                    {
                        X = ICON_SIZE * 10 - i * ICON_SIZE,
                        Y = 0
                    };
                    marker.DinerX = marker.X;
                    marker.DinerY = marker.Y - ICON_SIZE;
                    _conveyorMap._markers.Add(marker);
                }

                for (int i = 0; i <= 9; i++)
                {
                    var marker = new Marker
                    {
                        X = 0,
                        Y = ICON_SIZE + ICON_SIZE * i
                    };
                    marker.DinerX = marker.X - ICON_SIZE;
                    marker.DinerY = marker.Y;
                    _conveyorMap._markers.Add(marker);
                }

                for (int i = 0; i <= 9; i++)
                {
                    var marker = new Marker
                    {
                        X = ICON_SIZE + i * ICON_SIZE,
                        Y = ICON_SIZE * 11
                    };
                    marker.DinerX = marker.X;
                    marker.DinerY = marker.Y + ICON_SIZE;
                    _conveyorMap._markers.Add(marker);
                }

                for (int i = 0; i <= 4; i++)
                {
                    var marker = new Marker
                    {
                        X = ICON_SIZE * 11,
                        Y = ICON_SIZE * 10 - i * ICON_SIZE
                    };
                    marker.DinerX = marker.X + ICON_SIZE;
                    marker.DinerY = marker.Y;
                    _conveyorMap._markers.Add(marker);
                }

                for (int i = 0; i < _conveyorMap._markers.Count; i++)
                {
                    _conveyorMap._markers[i].X += ICON_SIZE * 2;
                    _conveyorMap._markers[i].Y += ICON_SIZE * 2;
                    _conveyorMap._markers[i].DinerX += ICON_SIZE * 2;
                    _conveyorMap._markers[i].DinerY += ICON_SIZE * 2;
                }

                for (int i = 0; i < _conveyorMap._markers.Count; i++)
                {
                    _conveyorMap._markers[i].PostDX = _conveyorMap._markers[(i + 1) % Count].X - _conveyorMap._markers[i].X;
                    _conveyorMap._markers[i].PostDY = _conveyorMap._markers[(i + 1) % Count].Y - _conveyorMap._markers[i].Y;

                    _conveyorMap._markers[i].PreDX = _conveyorMap._markers[i].X - _conveyorMap._markers[(i + Count - 1) % Count].X;
                    _conveyorMap._markers[i].PreDY = _conveyorMap._markers[i].Y - _conveyorMap._markers[(i + Count - 1) % Count].Y;
                }
            }
        }
    }
}
