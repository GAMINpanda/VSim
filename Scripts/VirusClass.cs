using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSim
{
    public class VirusClass
    {
        public string VirusName { get; set; } //Defining attributes - public so can be accessed without getters and setters
        public int Lethality { get; set; }
        public double RNumber { get; set; }
        public int Infectivity { get; set; }
        public double TemperatureResist { get; set; }
        public int MutateChance { get; set; }

        public VirusClass()
        {
            VirusName = "null";
            Lethality = 0;
            RNumber = 0;
            Infectivity = 0;
            TemperatureResist = 0;
            MutateChance = 0;
        }

        public VirusClass(string virusName, int lethality, double rNumber, int infectivity, double temperatureResist, int mutateChance)
        {
            VirusName = virusName;
            Lethality = lethality;
            RNumber = rNumber;
            Infectivity = infectivity;
            TemperatureResist = temperatureResist;
            MutateChance = mutateChance;
        }

        public void OutputToConsole()
        {
            Console.WriteLine("VirusName: " + this.VirusName);
            Console.WriteLine("Lethality: " + this.Lethality);
            Console.WriteLine("RNumber: " + this.RNumber);
            Console.WriteLine("Infectivity: " + this.Infectivity);
            Console.WriteLine("TemperatureResist: " + this.TemperatureResist);
            Console.WriteLine("MutateChance: " + this.MutateChance);
        }
    }
}
