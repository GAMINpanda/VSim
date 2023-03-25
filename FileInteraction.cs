using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using VSim.Code_Logic;
using Main;

namespace VSim
{
    class CurrentPixelStatusAndVirus //decision to store SIR data in a class ==> rather than .txt files
    {
        public List<int[]> SusceptiblePixels; //initialising attributes that other parts of the program can then use
        public List<int[]> InfectedPixels;
        public List<int[]> RecoveredPixels;
        public List<int[]> DeadPixels;

        public List<int[]> SIRValues;
        public VirusClass Virus;

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

        public void Reset() //reset simulation
        {
            SusceptiblePixels = new List<int[]>();
            InfectedPixels = new List<int[]>() { Main.Main.Globals.Day1Infected};
            RecoveredPixels = new List<int[]>();
            DeadPixels = new List<int[]>();
            SIRValues = new List<int[]>() { new int[] {1,0,0,0} }; //only 1 infected individual (will modify to add susceptible once data map gathering is written)
        }
    }

    class FileInteraction
    {
        public CurrentPixelStatusAndVirus LoadSaveFile() //Loads from Save.json
        {
            string FileName = "Save.json";
            using (StreamReader sr = new StreamReader(FileName))
            {
                string SaveJSONasString = sr.ReadToEnd(); //Read as raw string

                CurrentPixelStatusAndVirus cpsv = JsonSerializer.Deserialize<CurrentPixelStatusAndVirus>(SaveJSONasString); //deserialise into cpsv object

                return cpsv; //object can now be used in the simulation
            }
        }

        public void SaveToFile(CurrentPixelStatusAndVirus cpsv) //writes cpsv object to Save.json
        {
            string FileName = "Save.json";
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                string ConvertToJSON = JsonSerializer.Serialize(cpsv); //serialize object to JSON format

                sw.Write(FileName); //Write to save
            }
        }
    }
}
