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
    /// Logica di interazione per AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        string type;
        MainWindow mw;
      
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();

        public AdminWindow(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            this.type = ChoisesComboBox.Text;

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            mw.Show();
            this.Close();
        }


        private void ManageButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChoisesComboBox.Text.Equals("Workers"))
            {
                WorkerManagementWindow wmw = new WorkerManagementWindow(this);
                this.Hide();
                wmw.Show();
            } else if (ChoisesComboBox.Text.Equals("Teams"))
            {
                TeamManagementWindow tmw = new TeamManagementWindow(this);
                this.Hide();
                tmw.Show();
            } else if (ChoisesComboBox.Text.Equals("Missions"))
            {
                MissionManagementWindow mmw = new MissionManagementWindow(this);
                this.Hide();
                mmw.Show();
            } else if (ChoisesComboBox.Text.Equals("Celestial Bodies"))
            {
                CelestialBodyManagementWindow cmw = new CelestialBodyManagementWindow(this);
                this.Hide();
                cmw.Show();

            } else if (ChoisesComboBox.Text.Equals("Means"))
            {
                MeanManagementWindow memw = new MeanManagementWindow(this);
                this.Hide();
                memw.Show();
            } else if (ChoisesComboBox.Text.Equals("Places"))
            {
                PlaceManagementWindow pmw = new PlaceManagementWindow(this);
                this.Hide();
                pmw.Show();
            }
            else if (ChoisesComboBox.Text.Equals("Assign Rocket"))
            {
                AssignRocketWindow arw = new AssignRocketWindow(this);
                this.Hide();
                arw.Show();
            }

        }

        private void ChoisesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}



