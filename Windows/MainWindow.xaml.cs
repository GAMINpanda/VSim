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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace VSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SelectDayOne selectWin = new SelectDayOne();
        public SettingsWindow settingsWin = new SettingsWindow(selectWin);
        public ChartStats chartWin = new ChartStats();
        public MainWindow()
        {
            InitializeComponent();

            selectWin.Show();
        }

        private void OnSettingsClick(object sender, RoutedEventArgs e)
        {
            Main.Globals.CheckPause = true; //pause when opening settings
            Console.WriteLine("Opening Settings");
            //initialise settings window
            settingsWin.Show();
        }
        private void OnPlayClick(object sender, RoutedEventArgs e)
        {
            Main.Globals.Speed = 1; //sets default running speed
            Console.WriteLine("Speed: "+Main.Globals.Speed);
            Main.Globals.CheckPause = false; //sets the pause watching variable to false ==> Program can now start
        }
        private void OnSpeedClick(object sender, RoutedEventArgs e)
        {
            Main.Globals.Speed = 0.5; //Running time halfs so simulation runs double as quick
            Console.WriteLine("Speed: "+Main.Globals.Speed);
        }
        private void OnPauseClick(object sender, RoutedEventArgs e)
        {
            Main.Globals.CheckPause = true; //sets pause watching variable to true ==> Program is paused
        }
        private void OnStopClick(object sender, RoutedEventArgs e)
        {
            //Will stop the simulation (and closes window)
            Console.WriteLine("Applicatin stopping");
            Main.Globals.CheckPause = true;
            Main.Globals.CheckStop = true;
            chartWin.Close();
            settingsWin.Close();
            selectWin.Close();
            this.Close();
            System.Windows.Forms.Application.Exit();
        }
        private void OnGraphClick(object sender, RoutedEventArgs e)
        {
            Main.Globals.CheckPause = true; //pause when opening graph
            Console.WriteLine("Opening Graph");
            //Opens graph with SIR values
            chartWin.Show();
        }

        public void ChangeDay()
        {
            //Change the value of 'Day' on the main window
            this.dayDisplay.Text = "Day: " + Main.Globals.Day;
            
        }
    }
}
