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
    /// Interaction logic for AssignRocketWindow.xaml
    /// </summary>
    public partial class AssignRocketWindow : Window
    {
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        AdminWindow aw;
        public AssignRocketWindow(AdminWindow aw)
        {
            InitializeComponent();
            this.aw = aw;
            AssignButton.IsEnabled = false;
            IDSatelliteText.IsEnabled = false;
            IDRobotText.IsEnabled = false;
        }

        private void AssignButton_Click(object sender, RoutedEventArgs e)
        {
            var res = from r in db.ROCKETs
                      where r.ID_Rocket.Equals(int.Parse(IDRobotText.Text))
                      select new
                      {
                          IDRocket = r.ID_Rocket,
                          BuildDate = r.Build_Date
                      };
            if (res.Count() == 0)
            {
                MessageBox.Show("Rocket ID does not exist", "Error", MessageBoxButton.OK);
                return;
            }

            if (RobotRadioButton.IsChecked.Value)
            {
                var res2 = from ro in db.ROBOTs
                           where ro.ID_Robot.Equals(int.Parse(IDRobotText.Text))
                           select new
                           {
                               IDRobot = ro.ID_Robot,
                               IDRocket = ro.ID_Rocket
                           };
                if (res2.Count() == 0)
                {
                    MessageBox.Show("Robot ID does not exist", "Error", MessageBoxButton.OK);
                    return;
                }else if (res2.First().IDRocket.HasValue)
                {
                    MessageBox.Show("Robot is already assigned to a rocket", "Error", MessageBoxButton.OK);
                    return;
                }

                var queery = (from r in db.ROBOTs
                              where r.ID_Robot.Equals(int.Parse(IDRobotText.Text))
                              select r).First();
                queery.ID_Rocket = int.Parse(IDRocketText.Text);
                             
            }else if (SatelliteRadioButton.IsChecked.Value)
            {
                var res3 = from s in db.SATELLITEs
                           where s.ID_Satellite.Equals(int.Parse(IDSatelliteText.Text))
                           select new
                           {
                               IDSatellite = s.ID_Satellite,
                               IDRocket = s.ID_Rocket
                           };
                if (res3.Count() == 0)
                {
                    MessageBox.Show("Satellite ID does not exist", "Error", MessageBoxButton.OK);
                    return;
                }
                else if (res3.First().IDRocket.HasValue)
                {
                    MessageBox.Show("Satellite is already assigned to a rocket", "Error", MessageBoxButton.OK);
                    return;
                }
                var queery = (from s in db.SATELLITEs
                              where s.ID_Satellite.Equals(int.Parse(IDSatelliteText.Text))
                              select s).First();
                queery.ID_Rocket = int.Parse(IDRocketText.Text);
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.aw.Show();
            this.Close();
        }

        private void SatelliteRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            IDRobotText.Clear();
            AssignButton.IsEnabled = false;
            IDSatelliteText.IsEnabled = true;
            IDRobotText.IsEnabled = false;
        }

        private void RobotRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            AssignButton.IsEnabled = false;
            IDSatelliteText.Clear();
            IDSatelliteText.IsEnabled = false;
            IDRobotText.IsEnabled = true;
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void IDSatelliteText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDSatelliteText.Text) || !IsDigitsOnly(IDSatelliteText.Text) || IDRocketText.Equals(""))
            {
                AssignButton.IsEnabled = false;
            }
            else
            {
                AssignButton.IsEnabled = true;
            }
        }

        private void IDRobotText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDRobotText.Text) || !IsDigitsOnly(IDRobotText.Text) || IDRocketText.Equals(""))
            {
                AssignButton.IsEnabled = false;
            }
            else
            {

                AssignButton.IsEnabled = true;
            }
        }

        private void IDRocketText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDRobotText.Text) || !IsDigitsOnly(IDRobotText.Text) || (IDRobotText.Equals("") && IDSatelliteText.Text.Equals("")))
            {
                AssignButton.IsEnabled = false;
            }
            else
            {

                AssignButton.IsEnabled = true;
            }
        }
    }
}
