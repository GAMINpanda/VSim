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
        Bitmap temperature = new Bitmap("ImagesLowRes/TemperatureMapVeryLowRes.jpg");
        Bitmap population = new Bitmap("ImagesLowRes/PopulationDensityVeryLowRes.jpg");
        Bitmap GDP = new Bitmap("ImagesLowRes/GDPperCapitaVeryLowRes.png");

        public DataMap()
        {
            Console.WriteLine("DataMaps initialised");
        }

        public double GetTemp(int x, int y) //get temperature from a coordinate (assume coordinates are valid)
        {
            //If quite red or quite blue then temperature is quite extreme, if green it is more normal
            //use larger of (red - green)/ 255*(blue + 1) or (blue - green)/ 255*(red + 1) to calculate temperature 'extremness'

            Color getPixel = temperature.GetPixel(x, y);
            int red = getPixel.R;
            int green = getPixel.G;
            int blue = getPixel.B;

            double redBias = (double)(red - green) / (double)(255 * (blue + 1));
            double blueBias = (double)(blue - green) / (double)(255 * (red + 1));

            if (redBias >= blueBias)
            {
                //return redBias;
                return (double)red / (double)255;
            }
            else
            {
                //return blueBias;
               return (double)blue / (double)255;
            }
        }

        public double GetPop(int x, int y) //get population density for a particular area
        {
            //the more red, the higher the population pretty much ==> white is lack of population however so need to compare to green and blue

            Color getPixel = population.GetPixel(x, y);
            int red = getPixel.R;
            int green = getPixel.G;
            int blue = getPixel.B;

            double average = (double)(green + blue) / (double)2; //average of green and blue (higher = lighter colour)

            double popDensity = Math.Abs((double)red - average) / (double)255;

            return popDensity; //return density (note population roughly density *100,000)
        }

       
        public double getGDP(int x, int y) //Gets GDP of a particular pixel
        {
            //not useful at the moment since the data Map is a different format to the other maps
            //darker blue = more gdp

            Color getPixel = GDP.GetPixel(x, y);
            int blue = getPixel.B;
            int red = getPixel.R;
            int green = getPixel.G;

            double average = (double)(green + red) / (double)2; //average of green and red (higher = ligher colour)

            double GDPamount = Math.Abs((double)blue - average)/ (double)255;

            //Console.WriteLine(GDPamount);

            return GDPamount;
        }    
    }
}
