using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Edonokaitenzushi.Convey;

namespace Edonokaitenzushi.Dine
{
    public class DinerFactory
    {
        private static string[] _filenames = new string[]
        {
            "Images/diner1.png",
            "Images/diner2.png",
            "Images/diner3.png",
            "Images/diner4.png",
            "Images/diner5.png",
            "Images/diner6.png",
            "Images/diner7.png",
            "Images/diner8.png",
            "Images/diner9.png",
            "Images/diner10.png"
        };

        private static readonly Random _random = new Random();

        public static Diner Create(IConveyor conveyor)
        {
            var diner = new Diner(conveyor);
            int index = _random.Next(_filenames.Length);
            diner.BitmapImage = new BitmapImage(new Uri(_filenames[index], UriKind.Relative));
            return diner;
        }
    }
}
