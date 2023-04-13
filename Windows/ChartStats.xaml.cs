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

        public StackedAreaSeries Susceptible = new StackedAreaSeries{ Title = "Susceptible", Values = new ChartValues<DateTimePoint>{}, LineSmoothness = 0 };
        public StackedAreaSeries Infected = new StackedAreaSeries { Title = "Infected", Values = new ChartValues<DateTimePoint> { }, LineSmoothness = 0 };
        public StackedAreaSeries Recovered = new StackedAreaSeries { Title = "Recovered", Values = new ChartValues<DateTimePoint> { }, LineSmoothness = 0 };
        public StackedAreaSeries Dead = new StackedAreaSeries { Title = "Dead", Values = new ChartValues<DateTimePoint> { }, LineSmoothness = 0 };

        public ChartStats()
        {
            InitializeComponent();

            //code adapted from LiveCharts tutorial at https://v0.lvcharts.com/App/examples/v1/Wpf/Stacked%20Area

            SeriesCollection = new SeriesCollection //Mock data for now, will add real data later on
            {
                Susceptible,
                Infected,
                Recovered,
                Dead
            };

            XFormatter = val => new DateTime((long)val).ToString("yyyy"); //Deals with labelling
            YFormatter = val => val.ToString("N") + " M";

            DataContext = this;
        }

        public void Update(long TotalSusceptible, long TotalInfected, long TotalRecovered, long TotalDead) //update the graph
        {
            Susceptible.Values.Add(new DateTimePoint(new DateTime(Main.Globals.Day, 1, 1), ((double)TotalSusceptible/(double)1000000))); //somehow unrepresentable --> Look at later
            Infected.Values.Add(new DateTimePoint(new DateTime(Main.Globals.Day, 1, 1), ((double)TotalInfected / (double)1000000)));
            Recovered.Values.Add(new DateTimePoint(new DateTime(Main.Globals.Day, 1, 1), ((double)TotalRecovered / (double)1000000)));
            Dead.Values.Add(new DateTimePoint(new DateTime(Main.Globals.Day, 1, 1), ((double)TotalDead / (double)1000000))); //add SIR totals in millions

            SeriesCollection = new SeriesCollection
            {
                Susceptible,
                Infected,
                Recovered,
                Dead
            };

            XFormatter = val => new DateTime((long)val).ToString("yyyy"); //Deals with labelling
            YFormatter = val => val.ToString("N") + "M";

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
            Console.WriteLine("Closing Graph");
            this.Hide();
        }
    }
    
}
