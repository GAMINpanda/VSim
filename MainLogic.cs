using System;
using System.Diagnostics;
using VSim;

namespace Main
{
    class Main
    {
        public void MainProcedure()
        {
            //will be the main running function of the program

            Startup(); //will handle user selection on starting pixel and draw main window

            while (true)
            {
                //Cycle(Globals.Day); //once cycle has been written this will be called to carry out the simulation

                Globals.Day++; //day increased each cycle
                Trace.WriteLine("New day");

                //System.Threading.Thread.Sleep(Convert.ToInt32(1000 * Globals.Speed)); //waits depending on simulation speed

                int count = 0;

                while (Globals.CheckPause) //infinte loop if paused
                {
                    if (count == 0) { //debugging tool to check if paused
                        Trace.WriteLine("Is Paused");
                        count++;
                    }
                    if (Globals.CheckStop) { break; } //Ends the simulation
                }

            }
        }

        public void Startup()
        {
            //Begins program

            Trace.WriteLine("Startup loaded");

            //ReadMapData(1280, 640); //Will read mapdata from the data maps once written

            //StartSelectScreen(); //Once written will select pixel to start from

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