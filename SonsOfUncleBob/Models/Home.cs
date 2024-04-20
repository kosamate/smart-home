using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonsOfUncleBob.Models
{
    //Plus Point: Dependency Inversion with this base class
    internal abstract class Home
    {
        public abstract List<Room> Rooms { get; }
    }
}
