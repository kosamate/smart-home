using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using SonsOfUncleBob.Database;

namespace SonsOfUncleBob.ViewModels
{
    public class HistoryViewModel : DetailsViewModel
    {
        public HistoryViewModel(HomeViewModel parent) : base(parent)
        {
            SelectedSignal = Room.Signals.FirstOrDefault();
            SignalPlotModel = new PlotModel();
            UpdatePlotModel();
        }

        private HistoryModel historyModel = new();

        private DateTime startDate = DateTime.Now.AddDays(-1);
        public DateTime StartDate
        {
            get => startDate;
            set 
            {
                if (value != startDate)
                {
                    startDate = value;
                    Notify();
                    UpdatePlotModel();
                }
            }
        }
        private DateTime endDate = DateTime.Now;
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    Notify();
                    UpdatePlotModel();
                }
            }
        }

        public List<SignalViewModel> Signals { get => Room.Signals; }

        private SignalViewModel selectedSignal;
        public SignalViewModel SelectedSignal { 
            get => selectedSignal;
            set 
            { 
                if (value != selectedSignal)
                {
                    selectedSignal = value;
                    Notify();
                }
            }
        }

        public PlotModel SignalPlotModel { get; private set; }

        private void UpdatePlotModel()
        {
            SignalPlotModel.Title = Room.Name;
            SignalPlotModel.Series.Clear();

            LineSeries lineserie = new LineSeries
            {
                ItemsSource = GetDummyDataPoints(),
                DataFieldX = "Time",
                DataFieldY = SelectedSignal.Name,
                StrokeThickness = 2,
                MarkerSize = 0,
                LineStyle = LineStyle.Solid,
                Color = OxyColors.White,
                MarkerType = MarkerType.Circle,
            };

            SignalPlotModel.Series.Add(lineserie);
            Notify(nameof(SignalPlotModel));
        }

        private IEnumerable<DataPoint> GetDataPoints()
        {
            foreach (var point in historyModel.GetSignalHistory(Room.Name, SelectedSignal.Name, StartDate, EndDate))
            {
                double time = (point.Key - StartDate).TotalMinutes;
                yield return new DataPoint(time, point.Value);
            }
        }
        private IEnumerable<DataPoint> GetDummyDataPoints()
        {
            return new DataPoint[] { new DataPoint(1, 1), new DataPoint(2, 2), new DataPoint(3, 4), new DataPoint(4, 8) };
        }
    }
}
