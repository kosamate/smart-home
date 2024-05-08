using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonsOfUncleBob.Models
{
    //Plus Point: Dependency Inversion with this base class
    public abstract class HomeModel
    {
        public abstract List<RoomModel> Rooms { get; }
    }
}
