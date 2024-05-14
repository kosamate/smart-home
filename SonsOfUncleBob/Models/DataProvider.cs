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
    internal class DataProvider
    {
        private BarcelonaHomeModel home = new();
        private Client client = new();
        
        public DataProvider()
        {
            startListening();
        }

        private async void startListening()
        {
            while (true)
            {
                var roomDTOs = await client.GetRoomsList();
                var bathroomDTO = await client.GetBathroom();
                updateRoomValues(roomDTOs);
                updateBathroomValues(bathroomDTO);
                string kitchenString = $"Kitchen:\n\tlight: {home.Rooms[0].Light}\n\ttemperature: {home.Rooms[0].Signals[0].CurrentValue}\n\t" +
                    $"desired temperature: {home.Rooms[0].Signals[0].DesiredValue}\n";
                string livingRoomString = $"Living room:\n\tlight: {home.Rooms[1].Light}\n\ttemperature: {home.Rooms[1].Signals[0].CurrentValue}\n\t" +
                    $"desired temperature: {home.Rooms[1].Signals[0].DesiredValue}\n";
                string bedroomString = $"Bedroom:\n\tlight: {home.Rooms[2].Light}\n\ttemperature: {home.Rooms[2].Signals[0].CurrentValue}\n\t" +
                    $"desired temperature: {home.Rooms[2].Signals[0].DesiredValue}\n";
                string bathroomString = $"Bathroom:\n\tlight: {home.Rooms[3].Light}\n\ttemperature: {home.Rooms[3].Signals[0].CurrentValue}\n\t" +
                    $"desired temperature: {home.Rooms[3].Signals[0].DesiredValue}\n\thumidity: {home.Rooms[3].Signals[1].CurrentValue}\n\t" +
                    $"desired humidity: {home.Rooms[3].Signals[1].DesiredValue}\n";
                Debug.WriteLine(kitchenString);
                Debug.WriteLine(livingRoomString);
                Debug.WriteLine(bedroomString);
                Debug.WriteLine(bathroomString);
                await Task.Delay(5000);
            }

        }

        private void updateRoomValues(List<RoomDTO> roomDTOList)
        {
            foreach (RoomModel room in home.Rooms)
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
            foreach (RoomModel room in home.Rooms)
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
    }
}
