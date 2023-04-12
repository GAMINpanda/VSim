using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VSim
{
    public class DataMap
    {
        public string FilePath = System.IO.Path.GetFullPath(@"..\..\Data+Images");

        public DataMap()
        {
            Console.WriteLine("DataMaps initialised");
        }

        public double GetTemp(int x, int y) //get temperature from a coordinate (assume coordinates are valid)
        {
            //If quite red or quite blue then temperature is quite extreme, if green it is more normal
            //use larger of (red - green)/ 255*(blue + 1) or (blue - green)/ 255*(red + 1) to calculate temperature 'extremness'

            Bitmap temperature = new Bitmap(FilePath+"/TemperatureMap.jpg");

            Color getPixel = temperature.GetPixel(x, y);
            int red = getPixel.R;
            int green = getPixel.G;
            int blue = getPixel.B;

            double redBias = (double)(red - green) / (double)(255 * (blue + 1));
            double blueBias = (double)(blue - green) / (double)(255 * (red + 1));

            if (redBias >= blueBias)
            {
                return redBias;
            }
            else
            {
                return blueBias;
            }
        }

        public double GetPop(int x, int y) //get population density for a particular area
        {
            //the more red, the higher the population pretty much

            Bitmap population = new Bitmap(FilePath+"/PopulationDensity.jpg");

            Color getPixel = population.GetPixel(x, y);
            int red = getPixel.R;

            double popDensity = (double)red / (double)255;

            return popDensity; //return density (note population roughly density *100,000)
        }

       
        public double getGDP(int x, int y) //Gets GDP of a particular pixel
        {
            //not useful at the moment since the data Map is a different format to the other maps
            //More blue = more gdp 
            Bitmap GDP = new Bitmap(FilePath+"/GDPperCapita.png"); //need to edit to be same format as other two maps

            Color getPixel = GDP.GetPixel(x, y);
            int blue = getPixel.B;

            double GDPamount = (double)blue / (double)255;

            return GDPamount;
        }    
    }
}
