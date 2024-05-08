using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonsOfUncleBob.ViewModels
{
    public abstract class DetailsViewModel : ObservableObject
    {
        public DetailsViewModel(HomeViewModel parent)
        {
            this.parent = parent;
            parent.PropertyChanged += ParentPropertyChanged;
        }

        protected virtual void ParentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (nameof(parent.SelectedRoom).Contains(e.PropertyName))
                Notify(nameof(Room));
        }

        private HomeViewModel parent;

        public RoomViewModel Room
        {
            get => parent.SelectedRoom;
            set
            {
                if (value != parent.SelectedRoom)
                {
                    parent.SelectedRoom = value;
                    Notify();
                }
            }
        }
        public List<RoomViewModel> RoomList { get => parent.Rooms; }

        private bool isVisible = false;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                Notify();
            }
        }


    }
}
