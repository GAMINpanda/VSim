using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace VSim
{
    /// <summary>
    /// Interaction logic for ChartStats.xaml
    /// </summary>
    public partial class ChartStats : Window
    {
        public ChartStats()
        {
            InitializeComponent();
            BuildChart();
        }

        public void BuildChart()
        {
            ChartMain.AxisX.Add(new LiveCharts.Wpf.Axis {
                Title = "Day",
            });

            ChartMain.AxisY.Add(new Axis
            {
                Title = "Population"
            });

            LineSeries Infected = new LineSeries { 
                    Title="Infected",
                    Values = new ChartValues<double> { 0, 2, 3, 5 }
            };

            SeriesCollection SIR = new SeriesCollection { 
                Infected
            };
        }

        private void OnBackToMainClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
