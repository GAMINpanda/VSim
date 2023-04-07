using System;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Controls;

namespace VSim
{
    public class ManagePixelOutput //Manages how infection is output to the screen
    {
        System.Drawing.Color Infected = System.Drawing.Color.Red; //getting colours related to pixel status

        System.Drawing.Color Recovered = System.Drawing.Color.Green;

        System.Drawing.Color Dead = System.Drawing.Color.Black;

        public Bitmap SelectPixBitmap = new Bitmap(System.IO.Path.GetFullPath(@"..\..\Data+Images\1280x640transChanging.png")); //Pixel Bitmap to use

        public void ColourPixel(int Xcor, int Ycor, char col)
        {
            switch (col) //Set colour based off infection status
            {
                case 'i':
                    SelectPixBitmap.SetPixel(Xcor, Ycor, Infected);
                    break;
                case 'r':
                    SelectPixBitmap.SetPixel(Xcor, Ycor, Recovered);
                    break;
                case 'd':
                    SelectPixBitmap.SetPixel(Xcor, Ycor, Dead);
                    break;
                default:
                    break;
            }
        }

        public BitmapImage ConvertBitmapImage() //convert bitmap to bitmapimage
        {
            // Create the image element.
            System.Windows.Controls.Image simpleImage = new System.Windows.Controls.Image();
            simpleImage.Width = 200;

            // Create source.
            BitmapImage bi = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi.BeginInit();
            bi.UriSource = new Uri(System.IO.Path.GetFullPath(@"..\..\Data+Images\1280x640transChanging.png"), UriKind.RelativeOrAbsolute);
            bi.EndInit();
            // Set the image source.
            simpleImage.Source = bi;

            return bi;
        }
    }
}