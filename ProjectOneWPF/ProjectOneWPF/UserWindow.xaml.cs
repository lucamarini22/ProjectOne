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

namespace ProjectOneWPF
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        MainWindow mw;
        public UserWindow(MainWindow mw)
        {
            InitializeComponent();            
            this.mw = mw;

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.mw.Show();
            this.Hide();
        }

        private void LetsGoButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChoisesComboBox.Text.Equals("Free Search"))
            {
                FreeSearchWindow fsw = new FreeSearchWindow(this);
                fsw.Show();           
            }else if(ChoisesComboBox.Text.Equals("Solar System"))
            {
                SolarSystemWindow ssw = new SolarSystemWindow(this);
                ssw.Show();
            }else if (ChoisesComboBox.Text.Equals("Constellations"))
            {
                ConstellationWindow cw = new ConstellationWindow(this);
                cw.Show();
            }else if (ChoisesComboBox.Text.Equals("Moons Orbiting Around The Planet"))
            {
                MoonOrbitingWindow mow = new MoonOrbitingWindow(this);
                mow.Show();
            }
            else if (ChoisesComboBox.Text.Equals("Visualize Missions"))
            {
                MissionsWindow ms = new MissionsWindow(this);
                ms.Show();
            }
            this.Hide();
        }
    }
}
