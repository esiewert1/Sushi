using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edonokaitenzushi.Convey
{
    public interface IConveyorObserver
    {
        void OnBeltMoved();

        bool DoRemove { get; }
    }
}
