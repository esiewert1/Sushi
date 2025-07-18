using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Edonokaitenzushi.Convey.ConveyorMap;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Edonokaitenzushi.Convey;

namespace Edonokaitenzushi.Dine
{
    public class NullDiner : Diner
    {
        public NullDiner(IConveyor conveyor) : base(conveyor)
        {
        }

        public override double X
        {
            get; set;
        }

        public override double Y
        {
            get; set;
        }

        public override double SushiX
        {
            get; set;
        }

        public override double SushiY
        {
            get; set;
        }

        public override BitmapImage? BitmapImage
        {
            get;
            set;
        }

        public override ImageSource SushiImage
        {
            get;
        }

        public override int SeatNumber
        {
            get; set;
        }

        public override void OnBeltMoved()
        {
        }

        protected override void OnPropertyChanged([CallerMemberName] string name = "")
        {
        }

        public override void Cleanup()
        {
            DoRemove = true;
        }

        public override string BillString
        {
            get { return string.Empty; }
        }

    }
}
