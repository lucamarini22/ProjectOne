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
    /// Logica di interazione per ManagementMissionWindow.xaml
    /// </summary>
    public partial class MissionManagementWindow : Window
    {
        AdminWindow aw;
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        public MissionManagementWindow(AdminWindow aw)
        {
            InitializeComponent();
            this.aw = aw;

        }

        private void ClearTextBoxes()
        {
            NameText.Clear();
            DescText.Clear();
            BeginDateText.Clear();
            EndDateText.Clear();
            IDLText.Clear();
            RobotText.Clear();
            IDTText.Clear();
            IDSSText.Clear();
            IDPText.Clear();
            IDMText.Clear();
            IDSSText.Clear();
            SatelliteText.Clear();
            SpaceCText.Clear();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? endDate = null;
            int? idRobot = null;
            int? idSpaceCraft = null;
            int? idPlanet = null;
            int? idMoon = null;
            int? idSpaceStation = null;
            if (ChoisesComboBox.Text.Equals("Reconnaissance"))
            {
                if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrWhiteSpace(BeginDateText.Text)
                    || string.IsNullOrWhiteSpace(IDLText.Text) || string.IsNullOrWhiteSpace(IDTText.Text) ||
                    string.IsNullOrWhiteSpace(DescText.Text))
                {
                    MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                    return;
                }
                if (!checkDate(BeginDateText.Text)) {
                    MessageBox.Show("The begin date format not correct", "Error", MessageBoxButton.OK);
                    return;
                }
                if (!EndDateText.Text.Equals(""))
                {
                    if (!checkDate(EndDateText.Text))
                    {
                        MessageBox.Show("The end date format not correct", "Error", MessageBoxButton.OK);
                        return;
                    }
                    if (!checkTwoDates(BeginDateText.Text, EndDateText.Text))
                    {
                        ClearTextBoxes();
                        MessageBox.Show("The end date inserted is before the begin date", "Error", MessageBoxButton.OK);
                        return;
                    }
                    endDate = Convert.ToDateTime(EndDateText.Text);
                }

                if ((RobotText.Text.Equals("") && SpaceCText.Text.Equals("")) 
                    || (!RobotText.Text.Equals("") && !SpaceCText.Text.Equals("")))
                {
                    MessageBox.Show("You have to insert or a SpaceCraft or a Robot in a Reconnaissance", "Error", MessageBoxButton.OK);                    
                    return;
                }else if (!RobotText.Text.Equals(""))
                {
                    idRobot = int.Parse(RobotText.Text);
                }else if(!SpaceCText.Text.Equals(""))
                {
                    idSpaceCraft = int.Parse(SpaceCText.Text);
                }

                if ((IDMText.Text.Equals("") && IDPText.Text.Equals(""))
                    || (!IDMText.Text.Equals("") && !IDPText.Text.Equals("")))
                {
                    MessageBox.Show("You have to insert or a Planet or a Moon in a Reconnaissance", "Error", MessageBoxButton.OK);
                    return;
                }
                else if (!IDMText.Text.Equals(""))
                {
                    idMoon = int.Parse(IDMText.Text);
                }
                else if (!IDPText.Text.Equals(""))
                {
                    idPlanet = int.Parse(IDPText.Text);
                }

                if (!IDSSText.Text.Equals(""))
                {
                    idSpaceStation = int.Parse(IDSSText.Text);
                }

                RECONNAISSANCE r = new RECONNAISSANCE
                {
                    Mission_R_Name = NameText.Text,
                    Mission_R_Description = DescText.Text,
                    Begin_Date = Convert.ToDateTime(BeginDateText.Text),
                    End_Date = endDate,
                    ID_Launch_Site = int.Parse(IDLText.Text),
                    ID_Robot = idRobot,
                    ID_Team = int.Parse(IDTText.Text),
                    ID_Space_Station = idSpaceStation,
                    ID_Planet = idPlanet,
                    ID_Moon = idMoon,
                    ID_Spacecraft = idSpaceCraft
                };
                try
                {
                    db.RECONNAISSANCEs.InsertOnSubmit(r);
                    db.SubmitChanges();
                }
                catch
                {
                    db.RECONNAISSANCEs.DeleteOnSubmit(r);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);


                }

                ClearTextBoxes();


            }
            if (ChoisesComboBox.Text.Equals("Deliver in Orbit"))                
            {
                if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrWhiteSpace(BeginDateText.Text) 
                    || string.IsNullOrWhiteSpace(IDLText.Text) || string.IsNullOrWhiteSpace(IDTText.Text)
                    || string.IsNullOrWhiteSpace(DescText.Text) || string.IsNullOrWhiteSpace(IDPText.Text))
                {
                    MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                    return;
                }

                if (!EndDateText.Text.Equals(""))
                {
                    if (!checkDate(EndDateText.Text))
                    {
                        MessageBox.Show("The end date format not correct", "Error", MessageBoxButton.OK);
                        return;
                    }
                    if (!checkTwoDates(BeginDateText.Text, EndDateText.Text))
                    {
                        ClearTextBoxes();
                        MessageBox.Show("The end date inserted is before the begin date", "Error", MessageBoxButton.OK);
                        return;
                    }
                    endDate = Convert.ToDateTime(EndDateText.Text);
                }
                if (!IDSSText.Text.Equals(""))
                {
                    idSpaceStation = int.Parse(IDSSText.Text);
                }
                DELIVER_IN_ORBIT d = new DELIVER_IN_ORBIT
                {
                    Mission_DO_Name = NameText.Text,
                    Mission_DO_Description = DescText.Text,
                    Begin_Date = Convert.ToDateTime(BeginDateText.Text),
                    End_Date = endDate,
                    ID_Launch_Site = int.Parse(IDLText.Text),
                    ID_Team = int.Parse(IDTText.Text),
                    ID_Space_Station = idSpaceStation,
                    ID_Planet = int.Parse(IDPText.Text),
                    ID_Satellite = int.Parse(SatelliteText.Text)


                };
                try
                {
                    db.DELIVER_IN_ORBITs.InsertOnSubmit(d);
                    db.SubmitChanges();
                }
                catch
                {
                    db.DELIVER_IN_ORBITs.DeleteOnSubmit(d);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);


                }

                ClearTextBoxes();


            }
            if (ChoisesComboBox.Text.Equals("Discover"))
            {
                if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrWhiteSpace(BeginDateText.Text)
                    || string.IsNullOrWhiteSpace(IDLText.Text) || string.IsNullOrWhiteSpace(IDTText.Text)
                    || string.IsNullOrWhiteSpace(DescText.Text))
                {
                    MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                    return;
                }
                if (!EndDateText.Text.Equals(""))
                {
                    if (!checkDate(EndDateText.Text))
                    {
                        MessageBox.Show("The end date format not correct", "Error", MessageBoxButton.OK);
                        return;
                    }
                    if (!checkTwoDates(BeginDateText.Text, EndDateText.Text))
                    {
                        ClearTextBoxes();
                        MessageBox.Show("The end date inserted is before the begin date", "Error", MessageBoxButton.OK);
                        return;
                    }
                    endDate = Convert.ToDateTime(EndDateText.Text);
                }
                if (!IDSSText.Text.Equals(""))
                {
                    idSpaceStation = int.Parse(IDSSText.Text);
                }
                DISCOVER d = new DISCOVER
                {
                    Mission_D_Name = NameText.Text,
                    Mission_D_Description = DescText.Text,
                    Begin_Date = Convert.ToDateTime(BeginDateText.Text),
                    End_Date = endDate,
                    ID_Launch_Site = int.Parse(IDLText.Text),
                    ID_Team = int.Parse(IDTText.Text),
                    ID_Space_Station = idSpaceStation,
                    ID_Satellite = int.Parse(SatelliteText.Text)

                };
                try
                {
                    db.DISCOVERs.InsertOnSubmit(d);
                    db.SubmitChanges();
                }
                catch
                {
                    db.DISCOVERs.DeleteOnSubmit(d);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);


                }

                ClearTextBoxes();

            }


        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            aw.Show();
        }

        private void ChoisesComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (ChoisesComboBox.Text.Equals("Reconnaissance"))
            {
                RobotText.IsEnabled = true;
                SpaceCText.IsEnabled = true;
                SatelliteText.IsEnabled = false;
                IDPText.IsEnabled = true;
                IDMText.IsEnabled = true;
            }
            if (ChoisesComboBox.Text.Equals("Deliver in Orbit"))
            {
                RobotText.IsEnabled = false;
                SpaceCText.IsEnabled = false;
                SatelliteText.IsEnabled = true;
                IDPText.IsEnabled = true;
                IDMText.IsEnabled = false;
            }
            if (ChoisesComboBox.Text.Equals("Discover"))
            {
                RobotText.IsEnabled = false;
                SpaceCText.IsEnabled = false;
                SatelliteText.IsEnabled = true;
                IDPText.IsEnabled = false;
                IDMText.IsEnabled = false;
            }

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

        private void BeginDateText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BeginDateText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void EndDateText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EndDateText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void IDLText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDLText.Text) || !IsDigitsOnly(IDLText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void IDTText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDTText.Text) || !IsDigitsOnly(IDTText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void IDSSText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDSSText.Text) || !IsDigitsOnly(IDSSText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void RobotText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RobotText.Text) || !IsDigitsOnly(RobotText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void SpaceCText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SpaceCText.Text) || !IsDigitsOnly(SpaceCText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void SatelliteText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SatelliteText.Text) || !IsDigitsOnly(SatelliteText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void IDPText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDPText.Text) || !IsDigitsOnly(IDPText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void IDMText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDMText.Text) || !IsDigitsOnly(IDMText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }

        }

        private bool checkDate(string d)
        {
            string[] date = d.Split('/');
            return int.Parse(date[0]) <= 12 && int.Parse(date[1]) >= 1 && int.Parse(date[1]) < 31
                && int.Parse(date[2]) < 2050;
        }

        private bool checkTwoDates(string BeginDate, string EndDate)
        {
            return Convert.ToDateTime(BeginDate) < Convert.ToDateTime(EndDate);
        }
    }
}
