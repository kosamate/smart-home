using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.ViewModels
{
    public abstract class DetailsViewModel : ObservableObject
    {
        public DetailsViewModel()
        {
            viewModels.Add(this);
            roomList.CollectionChanged += RoomListChanged;
            
        }
        public RoomViewModel? SelectedRoom
        {
            get => selectedRoom;
            set
            {
                if (value != selectedRoom)
                {
                    if (selectedRoom != null)
                        selectedRoom.PropertyChanged -= PropertyViewModelsChanged;
                    selectedRoom = value;
                    if (selectedRoom != null)
                        selectedRoom.PropertyChanged += PropertyViewModelsChanged;
                    NotifyAll();
                    NotifyAll(nameof(IsRoomSelected));
                }
            }
        }

        public bool IsRoomSelected { get => (selectedRoom == null ? false : true); }
        public ObservableCollection<RoomViewModel> RoomList { get => roomList; } 
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                Notify();
            }
        }

        public static void AddToRoomList(RoomViewModel roomViewModel)
        {
            roomList.Add(roomViewModel);
        }

        private static List<DetailsViewModel> viewModels = new List<DetailsViewModel>();

        private static RoomViewModel? selectedRoom;

        private static ObservableCollection<RoomViewModel> roomList = new ObservableCollection<RoomViewModel>();

        private bool isVisible = false;
        private void RoomListChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyAll(nameof(RoomList));
        }
        private void NotifyAll([CallerMemberName] string propertyName = "")
        {
            foreach (var viewModel in viewModels)
                viewModel.Notify(propertyName);
        }

        public void PropertyViewModelsChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Notify(e.PropertyName);
        }

    }
}
