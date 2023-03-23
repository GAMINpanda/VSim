using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using Main;

namespace VSim
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            MainWindow main = new MainWindow(); //Launch MainWindow on startup
            main.Show();

            Trace.WriteLine("App Launched");

            Main.Main program = new Main.Main();
            Task.Factory.StartNew(() =>
            {
                program.MainProcedure(main);
            }); //Launches the MainProcedure from a seperate thread,allowing the UI thread to function normally
        }
    }
}
