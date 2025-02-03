using Microcharts;
using SkiaSharp;
using SmartHome.Database;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace SmartHome.ViewModels
{
    public class HistoryViewModel : DetailsViewModel
    {
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
                Notify(nameof(Signals));
                SelectedSignal = Signals?.First();
                UpdateChart();
            }
            base.Notify(propertyName);
        }


        private HistoryModel historyModel;

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

        private KeyValuePair<string, int> selectedInterval  = new KeyValuePair<string, int>("1 minute", -1);
        public KeyValuePair<string, int> SelectedInterval { get => selectedInterval; set {
                if(value.Key != selectedInterval.Key)
                {
                    selectedInterval = value;
                    Notify();
                    UpdateChart();
                }
            } }

         public List<KeyValuePair<string, int>> Intervals { get; set; } = new List<KeyValuePair<string, int>>()
         { 
            new("1 minute", -1),
            new("5 minutes", -5),
            new("30 minutes", -30),
            new("1 hour", -1 * 60),

         };

        public List<KeyValuePair<DateTime, float>> ChartData { get; private set; } = [];

        private void UpdateChart()
        {

            ChartData = GetDataPoints().ToList();
            Notify(nameof(ChartData));
        }

        private IEnumerable<KeyValuePair<DateTime, float>> GetDataPoints()
        {
            if (SelectedRoom == null || SelectedSignal == null)
                return [];
            return historyModel.GetSignalHistory(SelectedRoom.Name, SelectedSignal.Name, DateTime.Now.AddMinutes(SelectedInterval.Value), DateTime.Now);
        }

    }
}
