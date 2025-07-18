using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edonokaitenzushi.SushiDomain
{
    public class SushiFactory
    {
        private static Random _random = new Random();

        public static Sushi CreateSushi()
        {
            int index = _random.Next(7);

            Sushi sushi = new NullSushi();

            switch (index)
            {
                case 1: sushi = new OtoroSushi(); break;
                case 2: sushi = new TakoSushi(); break;
                case 3: sushi = new TamagoSushi(); break;
                case 4: sushi = new TunaRollSushi(); break;
                case 5: sushi = new UnagiSushi(); break;
                case 6: sushi = new UniSushi(); break;
                case 0:
                default: sushi = new EbiSushi(); break;
            }

            return sushi;
        }
    }
}
