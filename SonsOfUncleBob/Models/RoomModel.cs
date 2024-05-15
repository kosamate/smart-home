using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonsOfUncleBob.Models
{
    public class RoomModel
    {
        //Plus Point: Builder design pattern
        public class RoomBuilder
        {
            private RoomModel room = new();
            public RoomBuilder()
            {
                room.Signals = new();
            }

            public RoomBuilder SetName(string name)
            {
                room.Name = name;
                return this;
            }

            public RoomBuilder SetLight(bool light)
            {
                room.Light = light;
                return this;
            }

            public RoomBuilder AddSignal(SignalModel signal)
            {
                room.Signals.Add(signal);
                return this;
            }
            public RoomModel Build()
            {
                if (room.Name == null)
                    throw new ArgumentNullException("Name of the room is a required property.");
                
                return room;
            }


        }
        public string Name { get; private set; }
        public bool Light { get; set; }

        public List<SignalModel> Signals { get; set; }
        private RoomModel()
        {

        }


    }
}
