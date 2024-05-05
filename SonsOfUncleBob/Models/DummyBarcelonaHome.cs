using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonsOfUncleBob.Models
{
    public class DummyBarcelonaHome : BarcelonaHome
    {
        public DummyBarcelonaHome()
        {
            foreach(Room room in Rooms)
            {
                foreach (HomeSignal signal in room.Signals)
                    foreach(float f in new float[] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f})
                    signal.Values.Enqueue(f);
            }
        }
    }
}
