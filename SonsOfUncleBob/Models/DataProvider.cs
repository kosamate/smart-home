using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SonsOfUncleBob.Http;
using SonsOfUncleBob.Http.DTO;

namespace SonsOfUncleBob.Models
{
    //Singleton
    internal sealed class DataProvider
    {
        private static DataProvider instance = null;
        private static readonly object padlock = new object();

        public static event NewMeasuredValuesDelegate NewMeasuredValues;

        private Client client = new();
        private List<RoomModel> rooms = new();


        DataProvider()
        {
            foreach (string roomName in new string[] { "Kitchen", "Living Room", "Bedroom" })
                rooms.Add(
                    new RoomModel.RoomBuilder()
                    .SetName(roomName)
                    .AddSignal(new SignalModel("Temperature", "C°", SignalModel.SignalCategory.Temperature))
                    .Build()
                    );
            rooms.Add(
                new RoomModel.RoomBuilder()
                    .SetName("Bathroom")
                    .AddSignal(new SignalModel("Temperature", "C°", SignalModel.SignalCategory.Temperature))
                    .AddSignal(new SignalModel("Humidity", "%", SignalModel.SignalCategory.Humidity))
                    .Build()
                );
            startListening();
        }


        public static DataProvider Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DataProvider();
                    }
                    return instance;
                }
            }
        }

        private async void startListening()
        {
            while (true)
            {
                var roomDTOs = await client.GetRoomsList();
                var bathroomDTO = await client.GetBathroom();
                updateRoomsValues(roomDTOs);
                updateBathroomValues(bathroomDTO);
                NewMeasuredValues?.Invoke(this.rooms);
                await Task.Delay(5000);
            }
        }

        public async void updateDesiredValuesInServer(RoomModel room)
        {
            RoomDTO roomDTO = new("", - 100, -100, true);
            BathroomDTO bathroomDTO = new("", -100, -100, true, -100, -100);
            if (room.Signals.Count == 2)
            {
                adjustBathoomDTOFromRoomModel(room, bathroomDTO);
                await this.client.PutBathroom(bathroomDTO);
            }
            else
            {
                adjustRoomDTOFromRoomModel(room, bathroomDTO);
                await this.client.PutRoom(roomDTO);
            }

        }

        public async void updateDesiredValuesToDefault()
        {
            await this.client.DeleteRooms();
        }


        private void adjustRoomDTOFromRoomModel(RoomModel room, RoomDTO roomDTO)
        {
            roomDTO.Name = room.Name;
            roomDTO.Temperature = room.Signals[0].CurrentValue;
            roomDTO.DesiredTemperature = (float)room.Signals[0].DesiredValue;
            roomDTO.Light = room.Light;
        }

        private void adjustBathoomDTOFromRoomModel(RoomModel bathroom, BathroomDTO bathroomDTO)
        {
            bathroomDTO.Name = bathroom.Name;
            bathroomDTO.Temperature = bathroom.Signals[0].CurrentValue;
            bathroomDTO.DesiredTemperature = (float)bathroom.Signals[0].DesiredValue;
            bathroomDTO.Light = bathroom.Light;
            bathroomDTO.Humidity = bathroom.Signals[1].CurrentValue;
            bathroomDTO.DesiredHumidity = (float)bathroom.Signals[1].DesiredValue;
        }


        private void updateRoomsValues(List<RoomDTO> roomDTOList)
        {
            foreach (RoomModel room in this.rooms)
                foreach (RoomDTO roomDTO in roomDTOList)
                    if (room.Name == roomDTO.Name) 
                    {
                        room.Light = roomDTO.Light;
                        foreach (SignalModel signal in room.Signals)
                        {
                            signal.CurrentValue = (float)roomDTO.Temperature;
                            signal.DesiredValue = (float)roomDTO.DesiredTemperature;
                        }
                    }
        }

        private void updateBathroomValues(BathroomDTO bathroomDTO)
        {
            foreach (RoomModel room in this.rooms)
                if (room.Name == bathroomDTO.Name)
                {
                    room.Light = bathroomDTO.Light;
                    foreach (SignalModel signal in room.Signals)
                    {
                        if (signal.Category == SignalModel.SignalCategory.Temperature)
                        {
                            signal.CurrentValue = (float)bathroomDTO.Temperature;
                            signal.DesiredValue = (float)bathroomDTO.DesiredTemperature;
                        }
                        if(signal.Category == SignalModel.SignalCategory.Humidity)
                        {
                            signal.CurrentValue = (float)bathroomDTO.Humidity;
                            signal.DesiredValue = (float)bathroomDTO.DesiredHumidity;
                        }
                    }
                }
        }

        public delegate void NewMeasuredValuesDelegate(List<RoomModel> roomlist);
    }
}
