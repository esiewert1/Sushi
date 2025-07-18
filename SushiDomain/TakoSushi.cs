using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Edonokaitenzushi.SushiDomain
{
    public class TakoSushi : Sushi
    {

        public override ImageSource SushiImage
        {
            get
            {
                return new BitmapImage(new Uri("Images/takosushi.png", UriKind.Relative));
            }
        }

        public override int Price { get { return 250; } }
    }
}
