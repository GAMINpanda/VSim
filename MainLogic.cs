using System;

namespace Main
{
    class Main
    {
        public void MainProcedure()
        {
            //will be the main running function of the program

            //Startup()

            while (true)
            {
                //Cycle(Globals.Day)

                Globals.Day++;

                //Sleep(Globals.Speed)

                while (Globals.CheckPause)
                {
                    if (Globals.CheckStop) { break; }
                }

            }
        }
        public static class Globals {

            public static int Day = 0;

            public static int[] Day1Infected = new int[2];

            public static double Cure = 0;

            public static bool CheckPause = true;

            public static bool CheckStop = false;

            public static double Speed = 1;
        }
    }
}