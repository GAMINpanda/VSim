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

namespace VSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSettingsClick(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow(); //Opens the settings window
            settings.Show();
        }
        private void OnPlayClick(object sender, RoutedEventArgs e)
        {
            //Will begin simulation
        }
        private void OnSpeedClick(object sender, RoutedEventArgs e)
        {
            //Will speed up the simulation
        }
        private void OnPauseClick(object sender, RoutedEventArgs e)
        {
            //Will pause the simulation
        }
        private void OnStopClick(object sender, RoutedEventArgs e)
        {
            //Will stop the simulation (and closes window)
            this.Close();
        }
    }
}
