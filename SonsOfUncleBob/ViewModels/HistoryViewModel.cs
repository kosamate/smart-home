using Microcharts;
using SkiaSharp;
using SonsOfUncleBob.Database;
using System.Runtime.CompilerServices;

namespace SonsOfUncleBob.ViewModels
{
    public class HistoryViewModel : DetailsViewModel
    {
        readonly SKColor POINT_COLOR = new SKColor(6, 57, 112);
        readonly SKColor BACKGROUND_COLOR = new SKColor(171, 219, 227);
        public HistoryViewModel(HistoryModel historyModel)
        {
            this.historyModel = historyModel;
            historyModel.PropertyChanged += HistoryModel_PropertyChanged;
            SelectedSignal = SelectedRoom?.Signals.FirstOrDefault();
        }

        private void HistoryModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "History")
                UpdateChart();
        }

        protected override void Notify([CallerMemberName] string propertyName = "")
        {
            if (propertyName == nameof(IsVisible) && IsVisible)
            {
                UpdateChart();
            }
            if (propertyName == nameof(SelectedRoom))
            {
                SelectedSignal = SelectedRoom.Signals.First();
                UpdateChart();
            }
            base.Notify(propertyName);
        }


        private HistoryModel historyModel;

        private DateTime startDate = DateTime.Now.AddHours(-1);
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    Notify();
                    UpdateChart();
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
                    UpdateChart();
                }
            }
        }

        public List<SignalViewModel>? Signals { get => SelectedRoom?.Signals; }

        private SignalViewModel? selectedSignal;
        public SignalViewModel? SelectedSignal
        {
            get => selectedSignal;
            set
            {
                if (value != selectedSignal)
                {
                    selectedSignal = value;
                    Notify();
                    UpdateChart();
                }
            }
        }

        public LineChart? Chart { get; private set; }

        private void UpdateChart()
        {
            var dataPoints = GetDataPoints();
            if (dataPoints.Length == 0)
                return;

            if (Chart == null)
                this.Chart = new LineChart
                {
                    Entries = dataPoints,
                    LineMode = LineMode.Straight,
                    PointSize = 4f,
                    BackgroundColor = BACKGROUND_COLOR,
                    IsAnimated = false,
                    LabelOrientation = Orientation.Horizontal,
                };
            else
                (Chart as LineChart).Entries = dataPoints;

            Notify(nameof(Chart));
        }

        private ChartEntry[] GetDataPoints()
        {
            List<ChartEntry> dataPoints = new List<ChartEntry>();
            if (SelectedRoom == null || SelectedSignal == null)
                return [];

            var points = historyModel.GetSignalHistory(SelectedRoom.Name, SelectedSignal.Name, StartDate, DateTime.Now).ToArray(); // TODO chnges this if have a final idea for HistoryView
            if (points == null || points.Length == 0)
                return [];

            for (int i = 0; i < points.Length; i++)
            {
                    dataPoints.Add(new ChartEntry(points[i].Value) { Color = POINT_COLOR });
            }

            return dataPoints.ToArray();
        }

    }
}
