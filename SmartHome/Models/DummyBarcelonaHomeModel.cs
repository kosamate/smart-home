using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class DummyBarcelonaHomeModel : BarcelonaHomeModel
    {
        public DummyBarcelonaHomeModel()
        {
            foreach(RoomModel room in Rooms)
            {
                foreach (SignalModel signal in room.Signals)
                    signal.CurrentValue = 35f;
            }
        }
    }
}
