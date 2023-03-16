using System;
using System.Diagnostics;
using System.Collections.Generic;
using static Main.Main.Globals;

namespace Testing
{
    class Test
    {
        public static void TestItemInt(int[] Data, int ItemToTest)
        {
            foreach(var item in Data)
            {
                ItemToTest = item;
                Trace.WriteLine(ItemToTest);
            }
        }

        public static void TestItemDouble(double[] Data, double ItemToTest)
        {
            foreach (var item in Data)
            {
                ItemToTest = item;
                Trace.WriteLine(ItemToTest);
            }
        }

        public static void TestItemIntArray( List<int[]> Data, int[] ItemToTest)
        {
            foreach(var inner in Data)
            {
                int count = 0;
                foreach(var item in inner)
                {
                    ItemToTest[count] = item;
                    count++;
                }
                foreach(var item2 in ItemToTest) { Trace.WriteLine(item2); }
            }
        }

        public static void TestItemString(string[] Data, string ItemToTest)
        {
            foreach (var item in Data)
            {
                ItemToTest = item;
                Trace.WriteLine(ItemToTest);
            }
        }

        public static void TestItemBoolean(bool[] Data, bool ItemToTest)
        {
            foreach (var item in Data)
            {
                ItemToTest = item;
                Trace.WriteLine(ItemToTest);
            }
        }

        public static void TestDataMilestone1()
        {
            TestItemInt(new int[3] { 0, 5, "error" }, Day);

            TestItemIntArray(new List<int[]> { new int[] { 0, 0 }, new int[] { 5, 5 }, new int[] { 5, 5, 5 }, new int[] { "error", "error" } }, Day1Infected);

            TestItemDouble(new double[] { 0, 0.5, 100, "error" }, Cure);

            TestItemBoolean(new bool[] { true, false, "error"}, CheckPause);

            TestItemBoolean(new bool[] { true, false, "error" }, CheckStop);

            TestItemInt(new int[] {0,1,10,"error"}, Speed);
        }
    }
}