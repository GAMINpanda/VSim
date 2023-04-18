using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VSim
{
    public class CycleFunctions
    {
        Bitmap SelectPixBitmap = new Bitmap(System.IO.Path.GetFullPath(@"..\..\ImagesLowREs\SelectPixelVeryLowRes.png"));
        DataMap Data = new DataMap();
        public void CheckSusceptiblePixels()//Find which pixels could possibly be infected
        {
            foreach (int[] coord in Main.Globals.cpsv.InfectedPixels) //all surrounding pixels to the infected pixel are susceptible
            {
                bool sufficient = false;
                foreach (int[] coordCombo in Main.Globals.cpsv.SIRValues)
                {
                    if (coord.SequenceEqual(new int[] { coordCombo[0], coordCombo[1] }))
                    {
                        if(((double)coordCombo[2] / (double)coordCombo[3]) > Main.Globals.Cure)
                        {
                            sufficient = true; //only infect if proportion of infected to susceptible population larger than cure development
                        }
                        break;
                    }
                }
                if (sufficient)
                {
                    Check(new int[] { coord[0] + 1, coord[1] + 1 });
                    Check(new int[] { coord[0] + 1, coord[1] });
                    Check(new int[] { coord[0], coord[1] + 1 });
                    Check(new int[] { coord[0] - 1, coord[1] - 1 });
                    Check(new int[] { coord[0] - 1, coord[1] });
                    Check(new int[] { coord[0], coord[1] - 1 });
                    Check(new int[] { coord[0] - 1, coord[1] + 1 });
                    Check(new int[] { coord[0] + 1, coord[1] - 1 });
                }
            }
        }

        public void CalcNewInfectedPixels()//Calculate which of the Susceptible Pixels become infected
        {
            int[] coord;

            //Console.WriteLine("sus count: "+ Main.Globals.cpsv.SusceptiblePixels.Count);
            for(int i = 0; i < Main.Globals.cpsv.SusceptiblePixels.Count; i++)
            {
                coord = Main.Globals.cpsv.SusceptiblePixels[i];

                double temp = Data.GetTemp(coord[0], coord[1]);
                double pop = Data.GetPop(coord[0], coord[1]);
                //double gdp = Data.getGDP(coord[0], coord[1]);
                double gdp = 0.5; //keep as a constant as data map wrong size atm

                double RawNumber = Math.Pow((pop / gdp), temp * Main.Globals.cpsv.Virus.RNumber); //Number represents susceptibility to infection

                //Console.WriteLine("RawNumber: " + RawNumber);

                double d = 2; //some damping factor to test impact on infection
                double d2 = 1; //some other damping factor

                int valueToBeat = Convert.ToInt32(Main.Globals.cpsv.Virus.Infectivity * d2); //Higher per infectivity

                //Console.WriteLine("ValueToBeat: " + valueToBeat);

                Random random = new Random();
                int ranIteration;

                for (int j = 0; j < Convert.ToInt32(RawNumber*d); j++) //more attempts the higher raw number
                {
                    ranIteration = random.Next(0, 100);

                    //Console.WriteLine("RandomIteration: " + ranIteration);

                    if (ranIteration <= valueToBeat) //if random less than valueToBeat then pixel is infected (higher valueToBeat means more chance of infectvity)
                    {
                        Main.Globals.cpsv.InfectedPixels.Add(coord); //Pixel now infected
                        Main.Globals.cpsv.SIRValues.Add(new int[] { coord[0], coord[1], 1, Convert.ToInt32(Data.GetPop(coord[0], coord[1]) * 1000000), 0, 0 }); //Add SIR to Infected Pixels
                        Main.Globals.cpsv.SusceptiblePixels.Remove(coord); //no longer susceptible
                        break;
                    }
                }
            }
        }

        public void CalcRecoveredPixels()//Calculate which of the currently infected pixels can be considered 'Recovered'
        {
            int PopulationTotalAfflicted; //not including susceptible

            List<int[]> valsInfect = Main.Globals.cpsv.InfectedPixels;

            List<int[]> tempList = new List<int[]>() { };

            foreach (int[] vals in Main.Globals.cpsv.SIRValues) //iterate through all SIR pixels
            {
                //Console.WriteLine("I:" + vals[2] + "S:" + vals[3]+ "R:" + vals[4] + "D:" + vals[5]);

                if (valsInfect.Count > 1)
                {
                    for (int i = 0; i < valsInfect.Count; i++)
                    {
                        if (valsInfect[i].SequenceEqual(new int[] { vals[0], vals[1] }))
                        {
                            PopulationTotalAfflicted = vals[2] + vals[4] + vals[5];

                            //Console.WriteLine(PopulationTotal);

                            if ((double)vals[4] > (double)PopulationTotalAfflicted * 0.5) //if majority recovered considered a 'recovered' pixel
                            {
                                //Console.WriteLine("Majority recovered");
                                tempList.Add(valsInfect[i]); //no longer 'infected' persay
                                Main.Globals.cpsv.RecoveredPixels.Add(valsInfect[i]); //now 'recovered'
                            }

                            if ((double)vals[5] > (double)PopulationTotalAfflicted * 0.5) //if majority dead considered a 'dead' pixel
                            {
                                //Console.WriteLine("Majority dead");
                                tempList.Add(valsInfect[i]);
                                Main.Globals.cpsv.DeadPixels.Add(valsInfect[i]); //now 'dead'
                            }
                        }
                    }

                    for (int i = 0; i < tempList.Count; i++) //remove at end to prevent invalid parameters
                    {
                        RemoveItem(tempList[i], valsInfect);
                    }
                }

            }
        }

        public double CalcNewSIR(MainWindow MainWin)//Calculate individual pixel statistics
        {
            VirusClass virus = Main.Globals.cpsv.Virus;
            long TotalSusceptible = 0; //will represent global sir stats (no individual area needs long but global may)
            long TotalInfected = 0;
            long TotalRecovered = 0;
            long TotalDead = 0;

            int[] vals;
            int sus; //local sir stats
            int newsus;
            int infect;
            int newinfect;
            int recover;
            int newrecover;
            int dead;
            int newdead;
            int total;
            int dif;

            double power;

            double d1 = 0.5; //some dampening factor
            double d2 = 0.1; //damping factor representing proportion of people who recover/die each cycle (~10%)

            double couldBeInfected;

            for (int i = 0; i < Main.Globals.cpsv.SIRValues.Count; i++)
            {
                vals = Main.Globals.cpsv.SIRValues[i];
                sus = vals[3];
                infect = vals[2];
                recover = vals[4];
                dead = vals[5];
                total = sus + infect + recover + dead;

                if (!((recover > infect + sus) && ((double)infect/(double)total < 0.2))) //if recovered > infected + sus and infected less than 20% of population then don't bother calculating (more efficient)
                {

                    power = Math.Exp(-(virus.RNumber * ((virus.MutateChance * d1 * recover) + ((double)infect / (double)total))));

                    if (power < 1)
                    { //won't allow more to be infected than susceptible
                        couldBeInfected = sus * power; //this equation derived from SIR equation
                    }
                    else
                    {
                        couldBeInfected = sus;
                    }

                    newinfect = infect + Convert.ToInt32((double)virus.Infectivity / (double)100 * couldBeInfected); //calculate new values of infected, recovered, dead and susceptible in the pixel
                    newrecover = recover + Convert.ToInt32((double)newinfect * (((double)100 - (double)virus.Lethality) / (double)100) * d2);
                    newdead = dead + Convert.ToInt32((double)newinfect * ((double)virus.Lethality / (double)100) * d2);
                    newinfect = Convert.ToInt32((double)newinfect * (1 - d2)); //infected after recovered and dead are accounted for

                    newsus = total - (newinfect + newdead + newrecover);

                    Main.Globals.cpsv.SIRValues[i] = new int[] { vals[0], vals[1], newinfect, newsus, newrecover, newdead }; //stage changes in SIR
                }
                else //simpler calculation after a point
                {
                    newinfect = Convert.ToInt32(infect * 0.8); //slow decay of infection
                    dif = infect - newinfect;
                    newsus = sus;
                    newrecover = recover + Convert.ToInt32((double)dif * (((double)100 - (double)virus.Lethality) / (double)100) * d2);
                    newdead = Convert.ToInt32((double)dif * (1 - d2));

                    Main.Globals.cpsv.SIRValues[i] = new int[] { vals[0], vals[1], newinfect, newsus, newrecover, newdead }; //stage changes in SIR
                }

                TotalSusceptible = TotalSusceptible + newsus; //update global values
                TotalInfected = TotalInfected + newinfect;
                TotalRecovered = TotalRecovered + newrecover;
                TotalDead = TotalDead + newdead;
            }

            Console.WriteLine("TotalSus: " + TotalSusceptible);
            Console.WriteLine("TotalInfected: " + TotalInfected);
            Console.WriteLine("TotalRecovered: "+ TotalRecovered);
            Console.WriteLine("TotalDead: " + TotalDead);

            MainWin.Dispatcher.Invoke(() => { MainWin.chartWin.Update(TotalSusceptible, TotalInfected, TotalRecovered,TotalDead); }); //update graph

            return ((double)(TotalRecovered + TotalSusceptible)/(double)(TotalInfected + TotalSusceptible + TotalRecovered + TotalDead) * 0.2); //for use in cure development
        }

        public void UpdateCure(double TotalAble) //update cure progression
        {
            Main.Globals.Cure = Main.Globals.Cure + TotalAble * (1 / (double)(Main.Globals.cpsv.Virus.MutateChance + 1)); //higher mutation = harder to cure

            Console.WriteLine("Cure: " + Main.Globals.Cure);
        }

        public void Check(int[] coords)//check if pixel is susceptible/infected/recovered already or water
        {
            if (!(coords[0] >= 384 || coords[0] <= 0 || coords[1] >= 192 || coords[1] <= 0)) //make sure pixel is valid
            {
                System.Drawing.Color PixelCol = SelectPixBitmap.GetPixel(coords[0], coords[1]); //get colour of selected pixel

                if (!(CheckForItem(coords, Main.Globals.cpsv.SusceptiblePixels)) && !(CheckForItem(coords, Main.Globals.cpsv.InfectedPixels)) && !(CheckForItem(coords, Main.Globals.cpsv.RecoveredPixels)) && !(PixelCol.R == 134 && PixelCol.G == 247 && PixelCol.B == 221))
                {
                    Main.Globals.cpsv.SusceptiblePixels.Add(coords); //makes coordinate susceptible if not already
                }
            }
        }

        public bool CheckForItem(int[] coord, List<int[]> list) //need to check 'like' ==> .Contains checks for exact replicas
        {
            foreach (int[] valsInfect in list)
            {
                if (valsInfect.SequenceEqual(new int[] { coord[0], coord[1] }))
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveItem(int[] coord, List<int[]> list) //similar to check need to remove similar item
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].SequenceEqual(new int[] { coord[0], coord[1] }))
                {
                    list.Remove(list[i]);
                }
            }
        }
    }
}
