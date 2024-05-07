using SonsOfUncleBob.ViewModels;

namespace SonsOfUncleBob.Views;

public partial class History : ContentView
{
	public History()
	{
		InitializeComponent();
	}

    private string _selectedRoom;
    public string SelectedRoom
    {
        get { return _selectedRoom; }
        set
        {
            if (_selectedRoom != value)
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }
    }

}