using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using LiveCharts.Geared;
using PointsClient.Helpers;

namespace PointsClient.Scrollable
{
    public class PointsViewModel : INotifyPropertyChanged
    {
        private PointsManager _manager;
        private Func<double, string> _formatter;
        private double _from;
        private double _to;
        private GearedValues<Point> _values;

        public PointsViewModel()
        {
            var pointsHandler = new PointsHandler(new PointsWebClient(), new PointsParser());

            _manager = new PointsManager(pointsHandler, new DialogService());

            UpdatePoints();

            Formatter = x => new DateTime((long) x).ToString("t");
        }

        public object Mapper { get; set; }

        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }
        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }
        public GearedValues<Point> Values
        {
            get { return _values; }
            set
            {
                _values = value;
                OnPropertyChanged("Values");
            }
        }
        public Func<double, string> Formatter
        {
            get { return _formatter; }
            set
            {
                _formatter = value;
                OnPropertyChanged("Formatter");
            }
        }

        public async void UpdatePoints()
        {
            await _manager.GetAllPoints();

            Values = _manager.Points.AsGearedValues().WithQuality(Quality.High);
            From = _manager.Points.First().Time.Ticks;
            To = _manager.Points.Last().Time.AddMinutes(20).Ticks;
        }

        public async void SavePoints()
        {
            await _manager.SavePoints(Values.ToList());
            await _manager.GetAllPoints();

            Values = _manager.Points.AsGearedValues().WithQuality(Quality.High);
            From = _manager.Points.First().Time.Ticks;
            To = _manager.Points.Last().Time.AddMinutes(20).Ticks;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
