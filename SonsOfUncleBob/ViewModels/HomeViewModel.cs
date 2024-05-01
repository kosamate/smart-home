using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SonsOfUncleBob.Models;

namespace SonsOfUncleBob.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        public Home homeModel = new DummyBarcelonaHome();
        private Page page { get; set; }

        public bool IsInformationPageActive {
            get { return page == Page.Information; }
            set {
                page = value ? Page.Information : Page.History;
                Notify();
            }
        }
        public HomeViewModel()
        {
            page = Page.Information;

            foreach (Room room in homeModel.Rooms)

                switch(room.Name)
                {
                    case "Kitchen": Kitchen = new RoomViewModel(room); break;
                    case "Living Room": LivingRoom = new RoomViewModel(room); break;
                    case "Bedroom": BedRoom = new RoomViewModel(room); break;
                    case "Bathroom": BathRoom = new RoomViewModel(room); break;
                }
        }


        public IEnumerable<RoomViewModel> Rooms
        {
            get
            {
                yield return Kitchen;
                yield return LivingRoom;
                yield return BedRoom;
                yield return BathRoom;
            }
        }

        public RoomViewModel Kitchen { get; init; }
        public RoomViewModel LivingRoom { get; init; }
        public RoomViewModel BedRoom { get; init; }
        public RoomViewModel BathRoom { get; init; }
    }

    public enum Page {Information, History}

}
