using System;

namespace Main
{
    class Main
    {
        public void MainProcedure()
        {
            //will be the main running function of the program

            //Startup() //will handle user selection on starting pixel once writted

            while (true)
            {
                //Cycle(Globals.Day) //once cycle has been written this will be called to carry out the simulation

                Globals.Day++; //day increased each cycle

                System.Threading.Thread.Sleep(Convert.ToInt32(1000 * Globals.Speed)); //waits depending on simulation speed

                while (Globals.CheckPause) //infinte loop if paused
                {
                    if (Globals.CheckStop) { break; } //Ends the simulation
                }

            }
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