using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Edonokaitenzushi.SushiDomain
{
    public abstract class Sushi
    {
        public virtual int Price { get; }

        public virtual ImageSource SushiImage
        {
            get
            {
                return new BitmapImage(new Uri("Images/conveyorspot.png", UriKind.Relative));
            }
        }
    }
}
