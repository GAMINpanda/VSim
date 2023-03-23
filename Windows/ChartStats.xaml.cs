using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace VSim
{
    /// <summary>
    /// Interaction logic for ChartStats.xaml
    /// </summary>
    public partial class ChartStats : Window
    {
        private Func<double, string> _yFormatter;

        public ChartStats()
        {
            InitializeComponent();

            //code adapted from LiveCharts tutorial at https://v0.lvcharts.com/App/examples/v1/Wpf/Stacked%20Area

            SeriesCollection = new SeriesCollection //Mock data for now, will add real data later on
            {
                new StackedAreaSeries //each 'StackedAreaSeries' defines a seperate line
                {
                    Title = "Susceptible",
                    Values = new ChartValues<DateTimePoint>
                    {
                        new DateTimePoint(new DateTime(1, 1, 1), .228),
                        new DateTimePoint(new DateTime(2, 1, 1), .285),
                        new DateTimePoint(new DateTime(3, 1, 1), .366),
                        new DateTimePoint(new DateTime(4, 1, 1), .478)
                    },
                    LineSmoothness = 0
                },
                new StackedAreaSeries
                {
                    Title = "Infected",
                    Values = new ChartValues<DateTimePoint>
                    {
                        new DateTimePoint(new DateTime(1, 1, 1), .339),
                        new DateTimePoint(new DateTime(2, 1, 1), .424),
                        new DateTimePoint(new DateTime(3, 1, 1), .519),
                        new DateTimePoint(new DateTime(4, 1, 1), .618)
                    },
                    LineSmoothness = 0
                },
                new StackedAreaSeries
                {
                    Title = "Recovered",
                    Values = new ChartValues<DateTimePoint>
                    {
                        new DateTimePoint(new DateTime(1, 1, 1), 1.395),
                        new DateTimePoint(new DateTime(2, 1, 1), 1.694),
                        new DateTimePoint(new DateTime(3, 1, 1), 2.128),
                        new DateTimePoint(new DateTime(4, 1, 1), 2.634)
                    },
                    LineSmoothness = 0
                },
                new StackedAreaSeries
                {
                    Title = "Dead",
                    Values = new ChartValues<DateTimePoint>
                    {
                        new DateTimePoint(new DateTime(1, 1, 1), .549),
                        new DateTimePoint(new DateTime(2, 1, 1), .605),
                        new DateTimePoint(new DateTime(3, 1, 1), .657),
                        new DateTimePoint(new DateTime(4, 1, 1), .694)
                    },
                    LineSmoothness = 0
                }
            };

            XFormatter = val => new DateTime((long)val).ToString("yyyy"); //Deals with labelling
            YFormatter = val => val.ToString("N") + " M";

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> XFormatter { get; set; }

        public Func<double, string> YFormatter
        {
            get { return _yFormatter; }
            set
            {
                _yFormatter = value;
                OnPropertyChanged();
            }
        }

        public StackMode StackMode { get; set; }

        private void ChangeStackModeOnClick(object sender, RoutedEventArgs e) //Changes to a percentage type graph instead
        {
            foreach (var series in SeriesCollection.Cast<StackedAreaSeries>())
            {
                series.StackMode = series.StackMode == StackMode.Percentage
                    ? StackMode.Values
                    : StackMode.Percentage;
            }

            if (((StackedAreaSeries)SeriesCollection[0]).StackMode == StackMode.Values)
            {
                YFormatter = val => val.ToString("N") + " M";
            }
            else
            {
                YFormatter = val => val.ToString("P");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnBackToMainClick(object sender, RoutedEventArgs e) //return to the Main Window
        {
            this.Hide();
        }
    }
    
}
