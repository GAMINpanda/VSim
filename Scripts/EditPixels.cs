using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Controls;


namespace VSim
{
    public class ManagePixelOutput //Manages how infection is output to the screen
    {
        //below adapted from https://stackoverflow.com/questions/23673733/how-to-clear-a-writeablebitmap
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern void RtlZeroMemory(IntPtr dst, int length);

        protected void ClearWriteableBitmap()
        {
            RtlZeroMemory(wbmp.BackBuffer, wbmp.PixelWidth * wbmp.PixelHeight * (wbmp.Format.BitsPerPixel / 8));

            wbmp.Dispatcher.Invoke(() =>
            {
                wbmp.Lock();
                wbmp.AddDirtyRect(new Int32Rect(0, 0, wbmp.PixelWidth, wbmp.PixelHeight));
                wbmp.Unlock();
            });
        }
        //above adapted from https://stackoverflow.com/questions/23673733/how-to-clear-a-writeablebitmap

        System.Drawing.Color Infected = System.Drawing.Color.Red; //getting colours related to pixel status

        System.Drawing.Color Recovered = System.Drawing.Color.Green;

        System.Drawing.Color Dead = System.Drawing.Color.Black;

        public WriteableBitmap wbmp = new WriteableBitmap(384, 192, 96, 96, PixelFormats.Bgra32, null); //1280x640 writeable bitmap (Can change)

        private readonly byte[] _blankImage = new byte[192 * 384 * 4];



        public void ColourPixel(int Xcor, int Ycor, char col)
        {
            //Console.WriteLine("Pixel " + col + " x:" + Xcor + " y:" + Ycor);

            switch (col) //Set colour based off infection status
            {
                case 'i':
                    ChangePixel(Xcor, Ycor, Infected.R, Infected.G, Infected.B);
                    break;
                case 'r':
                    ChangePixel(Xcor, Ycor, Recovered.R, Recovered.G, Recovered.B);
                    break;
                case 'd':
                    ChangePixel(Xcor, Ycor, Dead.R, Dead.G, Dead.B);
                    break;
                default:
                    break;
            }
        }

        public void ChangePixel(int x, int y, int r, int g, int b)
        {
            byte red = Convert.ToByte(r);
            byte green = Convert.ToByte(g);
            byte blue = Convert.ToByte(b);

            byte alpha = 255;

            byte[] colourdata = { blue, green, red, alpha }; //combine pixel colour data
            Int32Rect rect = new Int32Rect(x, y, 1, 1);
            int stride = wbmp.PixelWidth * wbmp.Format.BitsPerPixel / 8;
            wbmp.WritePixels(rect, colourdata, stride, 0); //write pixel into writeable bitmap
        }

        public void Clear()
        {
            ClearWriteableBitmap();
        }
        
    }
}