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
    /// Logica di interazione per PlaceManagementWindow.xaml
    /// </summary>
    public partial class PlaceManagementWindow : Window
    {
        AdminWindow aw;
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();

        public PlaceManagementWindow(AdminWindow aw)
        {
            InitializeComponent();
            this.aw = aw;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            aw.Show();
        }

        private void InsertSpaceStationButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SSNameLabel.Text) || string.IsNullOrWhiteSpace(OrbitHLabel.Text))
            {
                MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                return;
            }
            SPACE_STATION s = new SPACE_STATION
            {
                Space_Station_Name = SSNameLabel.Text,
                Orbital_Heigth = Int32.Parse(OrbitHLabel.Text)
            };
            try
            {
                db.SPACE_STATIONs.InsertOnSubmit(s);
                db.SubmitChanges();
            }
            catch
            {
                db.SPACE_STATIONs.DeleteOnSubmit(s);
                MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);

            }

            SSNameLabel.Clear();
            OrbitHLabel.Clear();
        }

        private void InsertHangarButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HangarNameLabel.Text)
                || string.IsNullOrWhiteSpace(HangarCountryLabel.Text) || string.IsNullOrWhiteSpace(AddressLabel.Text))
            {
                MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                return;
            }
            HANGAR h = new HANGAR
            {
                Hangar_Name = HangarNameLabel.Text,
                Hangar_Country = HangarCountryLabel.Text,
                Hangar_Address = AddressLabel.Text
            };
            try
            {
                db.HANGARs.InsertOnSubmit(h);
                db.SubmitChanges();
            }
            catch
            {
                db.HANGARs.DeleteOnSubmit(h);
                MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);

            }

            HangarNameLabel.Clear();
            HangarCountryLabel.Clear();
            AddressLabel.Clear();
        }

        private void InsertLab_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LabNumLabel.Text))
            {
                MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                return;
            }
            LAB l = new LAB
            {
                Lab_Number = Int32.Parse(LabNumLabel.Text)
            };
            try
            {
                db.LABs.InsertOnSubmit(l);
                db.SubmitChanges();
            }
            catch
            {
                db.LABs.DeleteOnSubmit(l);
                MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);

            }

            LabNumLabel.Clear();
        }


        
        private void LabNumLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LabNumLabel.Text) || !IsDigitsOnly(LabNumLabel.Text))
            {
                InsertLabButton.IsEnabled = false;
            }
            else
            {
                InsertLabButton.IsEnabled = true;
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

        private void OrbitHLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(OrbitHLabel.Text) || !IsDigitsOnly(OrbitHLabel.Text))
            {
                InsertSpaceStationButton.IsEnabled = false;
            }
            else
            {
                InsertSpaceStationButton.IsEnabled = true;
            }
        }
    }
}
