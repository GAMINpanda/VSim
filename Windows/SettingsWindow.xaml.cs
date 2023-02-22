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

namespace VSim
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            //Will save current virus situation
        }

        private void OnLoadClick(object sender, RoutedEventArgs e)
        {
            //Will load a new simulation
        }

        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            //Will reset the simulation
        }

        private void OnSaveVirusClick(object sender, RoutedEventArgs e)
        {
            //Will save the virus parameters
        }

        private void OnReturnClick(object sender, RoutedEventArgs e)
        {
            this.Hide(); //Closes settings window
        }
    }
}
