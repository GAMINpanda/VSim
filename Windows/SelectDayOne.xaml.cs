﻿using System;
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

            double width = Application.Current.MainWindow.Width; //get width and height
            double height = Application.Current.MainWindow.Height;

            string messageBoxText = "Confirm Virus Start at " + "(" + coords.X + "," + coords.Y + ")"; //messagebox to confirm
            string caption = "Confirm";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes) //if user confirms
            {

                double day1Xd = coords.X * (1280 / width); //select image is 1280 pixels wide, so convert relative to image
                double day1Yd = coords.Y * (640 / height); //same for height (640 px high)

                Console.WriteLine("X: " + day1Xd + " Y: " + day1Yd);

                int day1Xi = Convert.ToInt32(day1Xd);
                int day1Yi = Convert.ToInt32(day1Yd); //convert to int

                Console.WriteLine("X: " + day1Xi + " Y: " + day1Yi);

                Bitmap SelectPixBitmap = new Bitmap("/Data+Images/SelectPixel.png");

                System.Drawing.Color PixelCol = SelectPixBitmap.GetPixel(day1Xi, day1Yi); //get colour of selected pixel

                if (PixelCol != System.Drawing.Color.Black) //if not land colour don't continue (change .Black)
                {

                    Main.Globals.Day1Infected = new int[] { day1Xi, day1Yi }; //set global Day1Infected to given value

                    Main.Globals.cpsv.Reset(); //resets SIR values

                    this.Hide();
                }
            }
        }

        private void OnReturnClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
