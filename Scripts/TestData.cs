using System;
using System.Diagnostics;
using System.Collections.Generic;
using static VSim.Main.Globals;

namespace Testing
{
    class Test
    {
        public static void TestItemInt(int[] Data, int ItemToTest, string Name)
        {
            Trace.WriteLine(Name);
            foreach(var item in Data)
            {
                ItemToTest = item;
                Trace.WriteLine("IntItem: "+ItemToTest);
            }
        }

        public static void TestItemDouble(double[] Data, double ItemToTest, string Name)
        {
            Trace.WriteLine(Name);
            foreach (var item in Data)
            {
                ItemToTest = item;
                Trace.WriteLine("DoubleItem: "+ItemToTest);
            }
        }

        public static void TestItemIntArray( List<int[]> Data, int[] ItemToTest, string Name)
        {
            Trace.WriteLine(Name);
            foreach(var inner in Data)
            {
                int count = 0;
                foreach(var item in inner)
                {
                    Trace.WriteLine("Count: " + count);
                    ItemToTest[count] = item;
                    count++;
                }
                foreach(var item2 in ItemToTest) { Trace.Write("IntArrayItem: "+item2+" "); }
                Trace.WriteLine("");
            }
        }

        public static void TestItemString(string[] Data, string ItemToTest, string Name)
        {
            Trace.WriteLine(Name);
            foreach (var item in Data)
            {
                ItemToTest = item;
                Trace.WriteLine("StringItem: "+ItemToTest);
            }
        }

        public static void TestItemBoolean(bool[] Data, bool ItemToTest, string Name)
        {
            Trace.WriteLine(Name);
            foreach (var item in Data)
            {
                ItemToTest = item;
                Trace.WriteLine("BoolItem: "+ItemToTest);
            }
        }

        public static void TestDataMilestone1() //testing milestone 1 data (global variables)
        {
            TestItemInt(new int[] { 0, 5}, Day, "Day");

            TestItemIntArray(new List<int[]> { new int[] { 0, 0 }, new int[] { 5, 5 }}, Day1Infected, "Day1Infected");

            TestItemDouble(new double[] { 0, 0.5, 100 }, Cure, "Cure");

            TestItemBoolean(new bool[] { true, false}, CheckPause, "CheckPause");

            TestItemBoolean(new bool[] { true, false}, CheckStop, "CheckStop");

            TestItemDouble(new double[] {0,1,0.5}, Speed, "Speed");
        }

        public static void TestDataMilestone3() //testing milestone 3 data to check saving works
        {
            cpsv.Virus.VirusName = "Covid";
            cpsv.Virus.Lethality = 75;
            cpsv.Virus.Infectivity = 90;
            cpsv.Virus.RNumber = 15;
            cpsv.Virus.TemperatureResist = 0.3;
            cpsv.Virus.MutateChance = 50;

            cpsv.InfectedPixels.Add(new int[] { 0, 0 });

            cpsv.RecoveredPixels.Add(new int[] { 0, 1 });

            cpsv.DeadPixels.Add(new int[] { 1, 0 } );

            cpsv.SusceptiblePixels.Add( new int[] { 1, 1 } );

            cpsv.SIRValues.Add(new int[] { 0, 0, 0, 1} );
        }
    }
}