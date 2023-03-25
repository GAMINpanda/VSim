using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSim.Code_Logic
{
    public class VirusClass
    {
        public string VirusName; //Defining attributes - public so can be accessed without getters and setters
        public int Lethality;
        public double RNumber;
        public int Infectivity;
        public double TemperatureResist;
        public int MutateChance;

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
    }
}
