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
        public SelectDayOne()
        {
            InitializeComponent();
        }

        public void GetMousePos()
        {
            Point coords = Mouse.GetPosition(Application.Current.MainWindow);
        }

        public void OnReturnClick()
        {
            this.Hide();
        }
    }
}
