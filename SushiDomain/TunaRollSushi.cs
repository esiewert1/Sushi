using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Edonokaitenzushi.SushiDomain
{
    public class TunaRollSushi : Sushi
    {
        public override ImageSource SushiImage
        {
            get
            {
                return new BitmapImage(new Uri("Images/tunarollsushi.png", UriKind.Relative));
            }
        }

        public override int Price { get { return 250; } }
    }
}
