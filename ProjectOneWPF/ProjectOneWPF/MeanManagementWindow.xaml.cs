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
    /// Logica di interazione per ManagementMeanWindow.xaml
    /// </summary>
    public partial class MeanManagementWindow : Window
    {
        private AdminWindow aw;
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        public MeanManagementWindow(AdminWindow aw)
        {
            InitializeComponent();
           
            this.aw = aw;
        }

        private void ClearTextBoxes()
        {
            NameText.Clear();
            DateText.Clear();
            OHeightText.Clear();
            IDHText.Clear();
            IDRText.Clear();
            DescText.Clear();
        }

        private bool checkDate(string d)
        {
            string[] date = d.Split('/');
            return int.Parse(date[0]) <= 12 && int.Parse(date[1]) >= 1 && int.Parse(date[1]) <= 31
                && int.Parse(date[2]) <= 2050 ;
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {

            
            if (ChoisesComboBox.Text.Equals("Rocket"))
            {
                if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrWhiteSpace(DateText.Text)
                    || string.IsNullOrWhiteSpace(IDHText.Text))
                {
                    MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                    return;
                }
                if (DateText.IsEnabled && !checkDate(DateText.Text))
                {
                    MessageBox.Show("The date format is not correct", "Error", MessageBoxButton.OK);
                    return;
                }
                if (DateText.IsEnabled && Convert.ToDateTime(DateText.Text) > DateTime.Now)
                {
                    MessageBox.Show("Build date can not be higth than current date", "Error", MessageBoxButton.OK);
                    return;
                }
                ROCKET r=null;
                try
                {
                    r = new ROCKET
                    {
                        Roket_Name = NameText.Text,
                        Build_Date = Convert.ToDateTime(DateText.Text),
                        ID_Hangar = int.Parse(IDHText.Text)

                    };
                }
                catch
                {
                    MessageBox.Show("Build date incorrect", "Error", MessageBoxButton.OK);
                    return;
                }
                try
                {
                    db.ROCKETs.InsertOnSubmit(r);
                    db.SubmitChanges();
                }
                catch
                {
                    db.ROCKETs.DeleteOnSubmit(r);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);


                }

                ClearTextBoxes();


            }
            if (ChoisesComboBox.Text.Equals("Satellite"))
            {
                int? OrbitalHeigth = null;
                int? IDRocket = null;
                if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrWhiteSpace(DateText.Text) 
                    || string.IsNullOrWhiteSpace(IDHText.Text ) )
                {
                    MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                    return;
                }
                if (DateText.IsEnabled && !checkDate(DateText.Text))
                {
                    MessageBox.Show("The date format not correct", "Error", MessageBoxButton.OK);
                    return;
                }
                if (DateText.IsEnabled && Convert.ToDateTime(DateText.Text) > DateTime.Now)
                {
                    MessageBox.Show("Build date can not be higth than current date", "Error", MessageBoxButton.OK);
                    return;
                }
                if (OHeightText.Text != "" && IDRText.Text != "")
                {
                    OrbitalHeigth = int.Parse(OHeightText.Text);
                    IDRocket = int.Parse(IDRText.Text);
                }
                else if((OHeightText.Text != "" && IDRText.Text == "")
                    || (OHeightText.Text == "" && IDRText.Text != ""))
                {
                    MessageBox.Show("If you insert a enter a rocket you need to enter the orbital height and vice versa", "Error", MessageBoxButton.OK);
                    return;
                }
                

                SATELLITE s = new SATELLITE
                {
                    Satellite_Name = NameText.Text,
                    Build_Date = Convert.ToDateTime(DateText.Text),
                    ID_Hangar = int.Parse(IDHText.Text),
                    Orbital_Heigth = OrbitalHeigth,
                    ID_Rocket = IDRocket

                };
                try
                {
                    db.SATELLITEs.InsertOnSubmit(s);
                    db.SubmitChanges();
                }
                catch
                {
                    db.SATELLITEs.DeleteOnSubmit(s);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);


                }

                ClearTextBoxes();


            }
            if (ChoisesComboBox.Text.Equals("Spacecraft"))
            {
                if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrWhiteSpace(DescText.Text ))
                {
                    MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                    return;
                }
                SPACECRAFT s = new SPACECRAFT
                {
                    Spacecraft_Name = NameText.Text,
                    Spacecraft_Description = DescText.Text
                   

                };
                try
                {
                    db.SPACECRAFTs.InsertOnSubmit(s);
                    db.SubmitChanges();
                }
                catch
                {
                    db.SPACECRAFTs.DeleteOnSubmit(s);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);


                }

                ClearTextBoxes();


            }
            if (ChoisesComboBox.Text.Equals("Robot"))
            {
                int? IDRocket = null;
                if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrWhiteSpace(DescText.Text))
                {
                    MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                    return;
                }

                if (IDRText.Text != "")
                {
                    IDRocket = int.Parse(IDRText.Text);
                }

                ROBOT r = new ROBOT
                {
                    Robot_Name = NameText.Text,
                    Robot_Description = DescText.Text,
                    ID_Rocket = IDRocket
                };
                try
                {
                    db.ROBOTs.InsertOnSubmit(r);
                    db.SubmitChanges();
                }
                catch
                {
                    db.ROBOTs.DeleteOnSubmit(r);
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

     


        private void ChoisesComboBox_DropDownClosed_1(object sender, EventArgs e)
        {
            if(ChoisesComboBox.Text.Equals("Rocket"))
            {
                DescText.IsEnabled = false;
                DateText.IsEnabled = true;
                OHeightText.IsEnabled = false;
                IDHText.IsEnabled = true;
                IDRText.IsEnabled = false;

            }
            if (ChoisesComboBox.Text.Equals("Satellite"))
            {
                DescText.IsEnabled = false;
                DateText.IsEnabled = true;
                OHeightText.IsEnabled = true;
                IDHText.IsEnabled = true;
                IDRText.IsEnabled = true;


            }
            if (ChoisesComboBox.Text.Equals("Spacecraft"))
            {
                DescText.IsEnabled = true;
                DateText.IsEnabled = false;
                OHeightText.IsEnabled = false;
                IDHText.IsEnabled = false;
                IDRText.IsEnabled = false;



            }
            if (ChoisesComboBox.Text.Equals("Robot"))
            {
                DescText.IsEnabled = true;
                DateText.IsEnabled = false;
                OHeightText.IsEnabled = false;
                IDHText.IsEnabled = false;
                IDRText.IsEnabled = true;

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

        private void DateText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DateText.Text) || !IsDigitsOnly(DateText.Text) )
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void OHeightText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(OHeightText.Text) || !IsDigitsOnly(OHeightText.Text) )
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void IDHText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDHText.Text) || !IsDigitsOnly(IDHText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void IDRText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDRText.Text) || !IsDigitsOnly(IDRText.Text) )
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }
    }
}
