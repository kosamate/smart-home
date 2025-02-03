using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Models.EventArguments
{
    internal class RoomEventArgs : EventArgs
    {
        public RoomModel Room { get; set; }
    }
}
