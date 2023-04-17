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

        public void Mutate()
        {
            Random random = new Random();
            int rand = random.Next(0, 100);
            int randomChange;

            if(rand < MutateChance) //virus mutates roughly MutateChance% of days
            {
                rand = random.Next(0, 4); //4 different items could mutuate
                randomChange = random.Next(0, 10); //some variation

                Main.Globals.Cure = Main.Globals.Cure - (randomChange * 0.01); //mutation halts progress

                if (Main.Globals.Cure < 0)
                {
                    Main.Globals.Cure = 0;
                }

                switch (rand)
                {
                    case 0://affects lethality
                        Lethality = Lethality + randomChange - 5;
                        break;
                    case 1: //affects rnumber
                        RNumber = RNumber + ((double)randomChange/(double)10) - 0.5;
                        break;
                    case 2://affects infectivity
                        Infectivity = Infectivity + randomChange - 5;
                        break;
                    case 3: //affects temperature resistance
                        TemperatureResist = TemperatureResist + ((double)randomChange / (double)20) - 0.25;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
