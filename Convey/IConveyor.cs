using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edonokaitenzushi.SushiDomain;

namespace Edonokaitenzushi.Convey
{
    public interface IConveyor
    {
        ObservableCollection<ConveyorSpot>? ConveyorSpots { get; }

        int PositionOfHead { get; }

        double FractionStep { get; }

        //        Sushi PeekSushiAt(int index);

        Sushi TakeSushiAt(int index);

        bool PutSushiAt(int index, Sushi sushi);

        void AddListener(IConveyorObserver observer);

        void RemoveListener(IConveyorObserver observer);
    }
}
