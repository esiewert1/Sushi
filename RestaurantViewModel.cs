using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Edonokaitenzushi.SushiDomain;
using Edonokaitenzushi.Convey;
using Edonokaitenzushi.Dine;

namespace Edonokaitenzushi
{
    public class RestaurantViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private IConveyor _conveyorBelt;

        private SushiChef _sushiChef;

        private DinerCollection _dinerCollection;

        public RestaurantViewModel()
        {
            _conveyorBelt = new ConveyorBelt();
            _sushiChef = new SushiChef(_conveyorBelt);
            _dinerCollection = new DinerCollection(_conveyorBelt);
        }

        public ObservableCollection<ConveyorSpot> ConveyorCollection
        {
            get
            {
                return _conveyorBelt.ConveyorSpots;
            }
        }

        public ObservableCollection<Diner> Diners
        {
            get
            {
                return _dinerCollection.DinerList;
            }
        }

        public ImageSource ChefHatImage
        {
            get
            {
                return new BitmapImage(new Uri("Images/chefHat.png", UriKind.Relative));
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
