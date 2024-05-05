using Microsoft.Maui.Controls;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Maui;
using SonsOfUncleBob.ViewModels;



namespace SonsOfUncleBob.Views;

public partial class PlotView : ContentView
{
    public PlotModel Model { get; set; }
    public PlotView()
	{
		
		Model = new PlotModel { Title = "Example Plot" };
            var series = new LineSeries();
            series.Points.Add(new DataPoint(0, 0));
            series.Points.Add(new DataPoint(1, 1));
            series.Points.Add(new DataPoint(2, 4));
            series.Points.Add(new DataPoint(3, 9));
            Model.Series.Add(series);
	}
}