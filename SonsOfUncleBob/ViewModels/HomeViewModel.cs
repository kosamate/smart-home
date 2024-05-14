﻿using System;
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
        public HomeViewModel(BarcelonaHomeModel homeModel, InformationViewModel informationViewModel, HistoryViewModel historyViewModel)
        {
            InformationViewModel = informationViewModel;
            HistoryViewModel = historyViewModel;
            foreach (RoomModel room in homeModel.Rooms)
            {
                var roomviewModel = new RoomViewModel(room);
                Rooms.Add(roomviewModel);
                DetailsViewModel.AddToRoomList(roomviewModel);
            }

            IsInformationPageActive = true;
        }

        private Page page { get; set; }

  
        public bool IsInformationPageActive
        {
            get { return page == Page.Information; }
            set
            {
                page = value ? Page.Information : Page.History;
                InformationViewModel.IsVisible = value;
                HistoryViewModel.IsVisible = !value;
                Notify();
            }
        }

        public InformationViewModel InformationViewModel { get; init; }
        public HistoryViewModel HistoryViewModel { get; init; }

        public List<RoomViewModel> Rooms { get; init; } = new();

        public RoomViewModel Kitchen { get => Rooms.Where(r => r.Name == "Kitchen").First(); }
        public RoomViewModel LivingRoom { get => Rooms.Where(r => r.Name == "Living Room").First(); }
        public RoomViewModel BedRoom { get => Rooms.Where(r => r.Name == "Bedroom").First(); }
        public RoomViewModel BathRoom { get => Rooms.Where(r => r.Name == "Bathroom").First(); }
    }

    public enum Page { Information, History }


}
