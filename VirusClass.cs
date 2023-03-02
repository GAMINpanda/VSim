using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSim.Code_Logic
{
    internal class VirusClass
    {
        public int VirusName; //Defining attributes - public so can be accessed without getters and setters
        public int Lethality;
        public double RNumber;
        public int Infectivity;
        public double TemperatureResist;
        public int MutateChance;

        VirusClass(int virusName, int lethality, double rNumber, int infectivity, double temperatureResist, int mutateChance)
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
