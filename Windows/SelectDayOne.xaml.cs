using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VSim
{
    /// <summary>
    /// Interaction logic for SelectDayOne.xaml
    /// </summary>
    public partial class SelectDayOne : Window
    {
        System.Windows.Point coords = new System.Windows.Point();
        string filePath = System.IO.Path.GetFullPath(@"..\..\ImagesLowRes\SelectPixelVeryLowRes.png");


        public SelectDayOne()
        {
            InitializeComponent();
        }

        private void GetMousePos(object sender, MouseEventArgs e) //Gets the position of the mouse relative to the WPF window
        {
            coords = Mouse.GetPosition(Application.Current.MainWindow); //makes use of inbuilt functions

            this.MousePos.Text = "(" + coords.X + "," + coords.Y + ")"; //update on screen
        }

        private void SelectDayInfect(object sender, MouseButtonEventArgs e) //called when the user clicks
        {
            coords = Mouse.GetPosition(Application.Current.MainWindow);

            string messageBoxText = "Confirm Virus Start at " + "(" + coords.X + "," + coords.Y + ")"; //messagebox to confirm
            string caption = "Confirm";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes) //if user confirms
            {
                Bitmap SelectPixBitmap = new Bitmap(filePath);

                double width = Application.Current.MainWindow.ActualWidth; //get width and height
                double height = Application.Current.MainWindow.ActualHeight;

                double day1Xd = coords.X * (SelectPixBitmap.Width / width); //so convert relative to image
                double day1Yd = coords.Y * (SelectPixBitmap.Height / height);

                Console.WriteLine("X: " + day1Xd + " Y: " + day1Yd);

                int day1Xi = Convert.ToInt32(day1Xd);
                int day1Yi = Convert.ToInt32(day1Yd); //convert to int

                Console.WriteLine("X: " + day1Xi + " Y: " + day1Yi);

                System.Drawing.Color PixelCol = SelectPixBitmap.GetPixel(day1Xi, day1Yi); //get colour of selected pixel ==> Issue solved using Actual Width/Height instead

                if (!(PixelCol.R == 134 && PixelCol.G == 247 && PixelCol.B == 221))//if not land colour don't continue
                {

                    Main.Globals.Day1Infected = new int[] { day1Xi, day1Yi }; //set global Day1Infected to given value

                    Main.Globals.Day1InfectedIsSelected = true;

                    Main.Globals.cpsv.Reset(); //resets SIR values

                    this.Hide();
                }
                else
                {
                    messageBoxText = "Cannot start in the ocean";
                    caption = "Confirm";
                    button = MessageBoxButton.OK;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
            }
        }

        private void OnReturnClick(object sender, RoutedEventArgs e)
        {
            if (Main.Globals.Day1InfectedIsSelected)
            {
                this.Hide();
            }
            else
            {
                string messageBoxText = "Please select a starting Pixel"; //Force user to select a starting pixel
                string caption = "Confirm";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }
    }
}
