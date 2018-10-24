using System.Windows;
using System;
using LiveCharts.Events;
using PointsClient.Scrollable;
using LiveCharts.Configurations;
using LiveCharts;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace PointsClient
{
    public partial class MainWindow : IDisposable
    {
        public MainWindow()
        {
            InitializeComponent();

            var mapper = Mappers.Xy<Point>()
                        .X((item) => item.Time.Ticks)
                        .Y((item) => item.Value);

            Charting.For<Point>(mapper);
        }

        private void Axis_OnRangeChanged(RangeChangedEventArgs eventargs)
        {
            var vm = (PointsViewModel)DataContext;

            var currentRange = eventargs.Range;

            if (currentRange < TimeSpan.TicksPerDay * 2)
            {
                vm.Formatter = x => new DateTime((long)x).ToString("t");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 60)
            {
                vm.Formatter = x => new DateTime((long)x).ToString("dd MMM yy");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 540)
            {
                vm.Formatter = x => new DateTime((long)x).ToString("MMM yy");
                return;
            }

            vm.Formatter = x => new DateTime((long)x).ToString("t");
        }

        public void Dispose()
        {
            var vm = (PointsViewModel)DataContext;
            vm.Values.Dispose();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PointsViewModel)DataContext;
            vm.UpdatePoints();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PointsViewModel)DataContext;
            vm.SavePoints();
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !new Regex(@"^[0-9]*(?:\.[0-9]*)?$").IsMatch(e.Text);
        }
    }
}

