﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using VSim;

namespace VSim
{
    public class CurrentPixelStatusAndVirus //decision to store SIR data in a class ==> rather than .txt files
    {
        public List<int[]> SusceptiblePixels { get; set; } //initialising attributes that other parts of the program can then use
        public List<int[]> InfectedPixels { get; set; }
        public List<int[]> RecoveredPixels { get; set; }
        public List<int[]> DeadPixels { get; set; }

        public List<int[]> SIRValues { get; set; }
        public VirusClass Virus { get; set; }

        public CurrentPixelStatusAndVirus(VirusClass VirusInp, List<int[]> InfectedInp, List<int[]> RecoveredInp, List<int[]> DeadInp, List<int[]> SIRInp)
        {
            Virus = VirusInp;
            SusceptiblePixels = new List<int[]>();
            InfectedPixels = InfectedInp;
            RecoveredPixels = RecoveredInp;
            DeadPixels = DeadInp;
            SIRValues = SIRInp;

            //SusceptiblePixels = CheckSusceptiblePixels(); //Unwritted as of yet but this is why storing all susceptible pixels is unecessary (can recalculate on reloading)
        }

        public CurrentPixelStatusAndVirus()//blank slate
        {
            Virus = new VirusClass();
            SusceptiblePixels = new List<int[]>();
            InfectedPixels = new List<int[]>();
            RecoveredPixels = new List<int[]>();
            DeadPixels = new List<int[]>();
            SIRValues = new List<int[]>();
        }

        public CurrentPixelStatusAndVirus(VirusClass virusInp) //if only input is virus class
        {
            Virus = virusInp;
            SusceptiblePixels = new List<int[]>();
            InfectedPixels = new List<int[]>();
            RecoveredPixels = new List<int[]>();
            DeadPixels = new List<int[]>();
            SIRValues = new List<int[]>();
        }

        public void Reset() //reset simulation
        {
            DataMap Data = new DataMap();
            SusceptiblePixels = new List<int[]>();
            InfectedPixels = new List<int[]>() { Main.Globals.Day1Infected};
            RecoveredPixels = new List<int[]>();
            DeadPixels = new List<int[]>();
            SIRValues = new List<int[]>() { new int[] { Main.Globals.Day1Infected[0], Main.Globals.Day1Infected[1], 1, Convert.ToInt32(Data.GetPop(Main.Globals.Day1Infected[0], Main.Globals.Day1Infected[1]) * 500000),0,0} }; //only 1 infected individual
            Console.WriteLine("Temperature:"+Data.GetTemp(Main.Globals.Day1Infected[0], Main.Globals.Day1Infected[1])); //Test
        }

        public void OutputToConsole()
        {
            this.Virus.OutputToConsole();

            Console.WriteLine("SusceptiblePixels: ");
            Loop(this.SusceptiblePixels);

            Console.WriteLine("InfectedPixels: ");
            Loop(this.InfectedPixels);

            Console.WriteLine("RecoveredPixels: ");
            Loop(this.RecoveredPixels);

            Console.WriteLine("DeadPixels: ");
            Loop(this.DeadPixels);

            Console.WriteLine("SIRValues:");
            foreach (int[] val in SIRValues)
            {
                Console.WriteLine("x: " + val[0]);
                Console.WriteLine("y: " + val[1]);
                Console.WriteLine("I: " + val[2]);
                Console.WriteLine("S: " + val[3]);
                Console.WriteLine("R: " + val[4]);
                Console.WriteLine("D: " + val[5]);
            }
        }

        public void Loop(List<int[]> inputItem)
        {
            foreach (int[] item in inputItem)
            {
                Console.WriteLine(item[0]);
                Console.WriteLine(item[1]);
            }
        }
    }

    public class FileInteraction
    {
        public string FilePath = System.IO.Path.GetFullPath(@"..\..\Save.json");

        public CurrentPixelStatusAndVirus LoadSaveFile() //Loads from Save.json
        {
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string SaveJSONasString = sr.ReadToEnd(); //Read as raw string

                CurrentPixelStatusAndVirus cpsv = JsonSerializer.Deserialize<CurrentPixelStatusAndVirus>(SaveJSONasString); //deserialise into cpsv object

                return cpsv; //object can now be used in the simulation
            }
        }

        public void SaveToFile(CurrentPixelStatusAndVirus cpsv) //writes cpsv object to Save.json
        {
            Console.WriteLine(FilePath);
            StreamWriter sw = new StreamWriter(FilePath);

            string ConvertToJSON = JsonSerializer.Serialize(cpsv); //serialize object to JSON format

            sw.Write(ConvertToJSON); //Write to save

            sw.Close();
        }
    }
}
