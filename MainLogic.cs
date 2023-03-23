using System;
using System.Diagnostics;
using VSim;
using Testing;

namespace Main
{
    class Main
    {
        public void MainProcedure(MainWindow MainWin)
        {
            //will be the main running function of the program

            Startup(); //will handle user selection on starting pixel and draw main window

            /* Area for testing each milestone
            Test.TestDataMilestone1();
            */

            while (true)
            {
                Cycle(Globals.Day); //this will be called to carry out a single cycle of the simulation

                MainWin.Dispatcher.Invoke(() => { MainWin.ChangeDay(); });

                Globals.Day++; //day increased each cycle
                Trace.WriteLine("New day: "+ Globals.Day);

                System.Threading.Thread.Sleep(Convert.ToInt32(1000 * Globals.Speed)); //waits depending on simulation speed

                int count = 0;

                while (Globals.CheckPause) //infinte loop if paused
                {
                    if (count == 0) { //debugging tool to check if paused
                        Trace.WriteLine("Is Paused");
                        count++;
                    }
                    if (Globals.CheckStop) { break; } //Ends the simulation
                }
                if (count > 0) { Trace.WriteLine("Unpaused"); } //for debugging

            }
        }

        public void Startup()
        {
            //Begins program

            Trace.WriteLine("Startup loaded");

            //ReadMapData(1280, 640); //Will read mapdata from the data maps once written (might decrease resolution later)

            //StartSelectScreen(); //Once written will select pixel to start from

        }

        public void Cycle(int Day)
        {
            Trace.WriteLine("Cycle called: "+Day);

            if (Globals.Cure < 1) //virus can only spread whilst the cure is incomplete
            {;
                //CheckSusceptiblePixels(); //Gather Pixels that are susceptible to infection
                //CalcNewInfectedPixels(); //Dictate whether to infect or not
            }

            //CalcRecoveredPixels(); //Recovers regardless of the cure being created or not

            //int TotalAble = CalcNewSIR(); 
            //UpdateCure(TotalAble, 8000000000); //assume only those who are uninfected can work towards cure
            //VirusMutate()

            //UpdatePixelColour(); //Makes changes to MainWindow image
            //UpdateGraphData(Day, READ('SIRValues.csv')); //Update Graph ==> May slash depending on time
        }

        public static class Globals { //Defining all global variables to be used below

            public static int Day = 0;

            public static int[] Day1Infected = new int[2];

            public static double Cure = 0;

            public static bool CheckPause = true;

            public static bool CheckStop = false;

            public static double Speed = 1;
        }
    }
}