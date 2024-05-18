using SonsOfUncleBob.Models.EventArguments;
using SonsOfUncleBob.ViewModels;
using Common.DTO;
using SonsOfUncleBob.Models;

namespace SonsOfUncleBob.Http
{
    //Singleton
    internal sealed class DataProvider
    {
        private static DataProvider instance = null;
        private static readonly object padlock = new object();

        //public static event NewMeasuredValuesDelegate NewMeasuredValues;
        public static event EventHandler<RoomListEventArgs> NewMeasuredValues;

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
            RoomViewModel.NewDesiredValues += UpdateDesiredValuesInServer;
            StartListening();
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

        private async void StartListening()
        {
            while (true)
            {
                UpdateRooms();
                await Task.Delay(5000);
            }
        }

        public async void UpdateDesiredValuesInServer(object sender, RoomEventArgs eventArgs)
        {
            RoomDTO roomDTO = new("", -100, -100, true);
            BathroomDTO bathroomDTO = new("", -100, -100, true, -100, -100);
            if (eventArgs.Room.Signals[0].DesiredValue != null)
            {
                if (eventArgs.Room.Signals.Count == 2)
                {
                    AdjustBathoomDTOFromRoomModel(eventArgs.Room, bathroomDTO);
                    await client.PutBathroom(bathroomDTO);
                }
                else
                {
                    AdjustRoomDTOFromRoomModel(eventArgs.Room, roomDTO);
                    await client.PutRoom(roomDTO);
                }
            }
        }

        public async void UpdateDesiredValuesToDefault()
        {
            await client.DeleteRooms();
        }

        private async void UpdateRooms()
        {
            var roomDTOs = await client.GetRoomsList();
            var bathroomDTO = await client.GetBathroom();
            UpdateRoomsValues(roomDTOs);
            UpdateBathroomValues(bathroomDTO);
            var eventArgs = new RoomListEventArgs();
            eventArgs.Rooms = rooms;
            NewMeasuredValues?.Invoke(this, eventArgs);
        }

        private void AdjustRoomDTOFromRoomModel(RoomModel room, RoomDTO roomDTO)
        {
            if (room.Signals[0].DesiredValue != null)
            {
                roomDTO.Name = room.Name;
                roomDTO.Temperature = room.Signals[0].CurrentValue;
                roomDTO.DesiredTemperature = (float)room.Signals[0].DesiredValue;
                roomDTO.Light = room.Light;
            }
        }

        private void AdjustBathoomDTOFromRoomModel(RoomModel bathroom, BathroomDTO bathroomDTO)
        {
            if (bathroom.Signals[0].DesiredValue != null && bathroom.Signals[1].DesiredValue != null)
            {
                bathroomDTO.Name = bathroom.Name;
                bathroomDTO.Temperature = bathroom.Signals[0].CurrentValue;
                bathroomDTO.DesiredTemperature = (float)bathroom.Signals[0].DesiredValue;
                bathroomDTO.Light = bathroom.Light;
                bathroomDTO.Humidity = bathroom.Signals[1].CurrentValue;
                bathroomDTO.DesiredHumidity = (float)bathroom.Signals[1].DesiredValue;
            }
        }


        private void UpdateRoomsValues(List<RoomDTO> roomDTOList)
        {
            if (roomDTOList != null)
            {
                foreach (RoomModel room in rooms)
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
            else
            {
                foreach (RoomModel room in rooms)
                {
                    room.Light = false;
                    foreach (SignalModel signal in room.Signals)
                    {
                        signal.CurrentValue = float.NaN;
                        signal.DesiredValue = float.NaN;
                    }
                }
            }
        }

        private void UpdateBathroomValues(BathroomDTO bathroomDTO)
        {
            if (bathroomDTO != null)
            {
                foreach (RoomModel room in rooms)
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
                            if (signal.Category == SignalModel.SignalCategory.Humidity)
                            {
                                signal.CurrentValue = (float)bathroomDTO.Humidity;
                                signal.DesiredValue = (float)bathroomDTO.DesiredHumidity;
                            }
                        }
                    }
            }
            else
            {
                foreach (RoomModel room in rooms)
                    if (room.Name == "Bathroom")
                    {
                        room.Light = false;
                        foreach (SignalModel signal in room.Signals)
                        {
                            if (signal.Category == SignalModel.SignalCategory.Temperature)
                            {
                                signal.CurrentValue = float.NaN;
                                signal.DesiredValue = float.NaN;
                            }
                            if (signal.Category == SignalModel.SignalCategory.Humidity)
                            {
                                signal.CurrentValue = float.NaN;
                                signal.DesiredValue = float.NaN;
                            }
                        }
                    }
            }
        }
    }
}
