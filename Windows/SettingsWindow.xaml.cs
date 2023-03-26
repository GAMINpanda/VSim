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
using VSim;

namespace VSim
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public VSim.FileInteraction file = new FileInteraction();
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            file.SaveToFile(Main.Globals.cpsv); //save global SIR values to JSON (untested)
        }

        private void OnLoadClick(object sender, RoutedEventArgs e)
        {
            Main.Globals.cpsv = file.LoadSaveFile(); //set global SIR values to whatever is loaded (untested)
        }

        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            Main.Globals.cpsv.Reset(); //resets SIR values (untested)

        }

        private void OnSaveVirusClick(object sender, RoutedEventArgs e)//Will save the virus parameters
        {
            //get value from each slider
            Main.Globals.cpsv.Virus.VirusName = this.name.Text;
            Main.Globals.cpsv.Virus.Lethality = Convert.ToInt32(this.lethality.Value);
            Main.Globals.cpsv.Virus.Infectivity = Convert.ToInt32(this.infectivity.Value);
            Main.Globals.cpsv.Virus.RNumber = this.rnumber.Value;
            Main.Globals.cpsv.Virus.TemperatureResist = this.tempresist.Value;
            Main.Globals.cpsv.Virus.MutateChance = Convert.ToInt32(this.mutateChance.Value);
        }

        private void OnReturnClick(object sender, RoutedEventArgs e)
        {
            this.Hide(); //Hides settings window
        }
    }
}
