using SonsOfUncleBob.ViewModels;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Maui;


namespace SonsOfUncleBob.Views;

public partial class Information : ContentView
{
	public Information()
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