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
    /// Logica di interazione per CelestialBodyManagementWindow.xaml
    /// </summary>
    public partial class CelestialBodyManagementWindow : Window
    {
        AdminWindow aw;
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        private int? IDMission=null;
        private int? IDStar = null;
        private int? IDConstellation = null;
        public CelestialBodyManagementWindow(AdminWindow aw)
        {
            InitializeComponent();

            this.aw = aw;
        }

        private void ClearTextBoxes()
        {
            NameText.Clear();
            DMText.Clear();
            DEText.Clear();
            RadiusText.Clear();
            MassMantissaText.Clear();
            MassExpText.Clear();
            SolarMassMantissaText.Clear();
            SolarMassExpText.Clear();
            IDGText.Clear();
            IDMText.Clear();
            IDSText.Clear();
            IDPText.Clear();
            IDCText.Clear();
        }


        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChoisesComboBox.Text.Equals("Planet"))
            {
                if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrWhiteSpace(DMText.Text)
             || string.IsNullOrWhiteSpace(DEText.Text) || string.IsNullOrWhiteSpace(RadiusText.Text)
             || string.IsNullOrWhiteSpace(MassMantissaText.Text) ||
             string.IsNullOrWhiteSpace(MassExpText.Text)
             || string.IsNullOrWhiteSpace(IDGText.Text))
                 
                {
                    MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                    return;
                }
                var res = from plan in db.PLANETs
                          where plan.Planet_Name.Equals(NameText.Text)
                          select plan.ID_Planet;
                if (res.Count() != 0)
                {
                    MessageBox.Show("Planet name already exist", "Error", MessageBoxButton.OK);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(IDMText.Text))
                {
                    IDMission = int.Parse(IDMText.Text);
                }
                if (!string.IsNullOrWhiteSpace(IDSText.Text))
                {
                    IDStar = int.Parse(IDSText.Text);
                }
                PLANET p = new PLANET
                {
                    Planet_Name = NameText.Text,
                    Distance_From_Earth_Mantissa = decimal.Parse(DMText.Text),
                    Distance_From_Earth_Exp = decimal.Parse(DEText.Text),
                    Radius = decimal.Parse(RadiusText.Text),
                    Mass_Mantissa = decimal.Parse(MassMantissaText.Text),
                    Mass_Exp = int.Parse(MassExpText.Text),
                    ID_Galaxy = int.Parse(IDGText.Text),
                    ID_Mission_D = IDMission,
                    ID_Star = IDStar

                };


                try
                {
                    db.PLANETs.InsertOnSubmit(p);
                    db.SubmitChanges();
                }
                catch
                {
                    db.PLANETs.DeleteOnSubmit(p);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);


                }

                ClearTextBoxes();


            }
            if (ChoisesComboBox.Text.Equals("Moon"))
            {
                if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrWhiteSpace(DMText.Text)
                   || string.IsNullOrWhiteSpace(DEText.Text) || string.IsNullOrWhiteSpace(RadiusText.Text)
                   || string.IsNullOrWhiteSpace(MassMantissaText.Text) ||
                   string.IsNullOrWhiteSpace(MassExpText.Text)
                   || string.IsNullOrWhiteSpace(IDGText.Text) || string.IsNullOrWhiteSpace(IDPText.Text))
                {
                    MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                    return;
                }
                var res = from mo in db.MOONs
                          where mo.Moon_Name.Equals(NameText.Text)
                          select mo.ID_Moon;
                if (res.Count() != 0)
                {
                    MessageBox.Show("Moon name already exist", "Error", MessageBoxButton.OK);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(IDMText.Text))
                {
                    IDMission = int.Parse(IDMText.Text);
                }
                var res1 = from pl in db.PLANETs
                           where pl.ID_Planet.Equals(IDPText.Text)
                           select new
                           {
                               IDGalaxy = pl.ID_Galaxy
                           };
                if(res1.Count() == 0 || !res1.First().IDGalaxy.Equals(int.Parse(IDGText.Text)))
                {
                    MessageBox.Show("Planet and moon arent in the same galaxy", "Error", MessageBoxButton.OK);
                    return;
                }
                MOON m = new MOON
                {
                    Moon_Name = NameText.Text,
                    Distance_From_Earth_Mantissa = decimal.Parse(DMText.Text),
                    Distance_From_Earth_Exp = int.Parse(DEText.Text),
                    Radius = decimal.Parse(RadiusText.Text),
                    Mass_Mantissa = decimal.Parse(MassMantissaText.Text),
                    Mass_Exp = int.Parse(MassExpText.Text),
                    ID_Galaxy = int.Parse(IDGText.Text),
                    ID_Mission_D = IDMission,
                    ID_Planet = int.Parse(IDPText.Text)

                };
                try
                {
                    db.MOONs.InsertOnSubmit(m);
                    db.SubmitChanges();
                }
                catch
                {
                    db.MOONs.DeleteOnSubmit(m);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);


                }

                ClearTextBoxes();


            }
            if (ChoisesComboBox.Text.Equals("Star"))
            {
                if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrWhiteSpace(DMText.Text)
                   || string.IsNullOrWhiteSpace(DEText.Text) || string.IsNullOrWhiteSpace(RadiusText.Text)
                   || string.IsNullOrWhiteSpace(SolarMassMantissaText.Text) ||
                   string.IsNullOrWhiteSpace(SolarMassExpText.Text)
                   || string.IsNullOrWhiteSpace(IDGText.Text))
                {
                    MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                    return;
                }
                var res = from st in db.STARs
                          where st.Star_Name.Equals(NameText.Text)
                          select st.ID_Star;
                if (res.Count() != 0)
                {
                    MessageBox.Show("Star name already exist", "Error", MessageBoxButton.OK);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(IDMText.Text))
                {
                    IDMission = int.Parse(IDMText.Text);
                }
                if (!string.IsNullOrWhiteSpace(IDCText.Text))
                {
                    IDConstellation = int.Parse(IDCText.Text);
                }
                STAR s = new STAR
                {
                    Star_Name = NameText.Text,
                    Distance_From_Earth_Mantissa = decimal.Parse(DMText.Text),
                    Distance_From_Earth_Exp = int.Parse(DEText.Text),
                    Radius = decimal.Parse(RadiusText.Text),
                    Solar_Mass_Mantissa = decimal.Parse(MassMantissaText.Text),
                    Solar_Mass_Exp = int.Parse(MassExpText.Text),
                    ID_Galaxy = int.Parse(IDGText.Text),
                    ID_Mission_D = IDMission,
                    ID_Constellation =IDConstellation

                };
                try
                {
                    db.STARs.InsertOnSubmit(s);
                    db.SubmitChanges();
                }
                catch
                {
                    db.STARs.DeleteOnSubmit(s);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);


                }

                ClearTextBoxes();


            }
            if (ChoisesComboBox.Text.Equals("Black Hole"))
            {
                if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrWhiteSpace(DMText.Text) 
                    || string.IsNullOrWhiteSpace(DEText.Text) || string.IsNullOrWhiteSpace(RadiusText.Text)
                    || string.IsNullOrWhiteSpace(SolarMassMantissaText.Text) ||
                    string.IsNullOrWhiteSpace(SolarMassExpText.Text) || string.IsNullOrWhiteSpace(IDGText.Text))
                {
                    MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                    return;
                }
                var res = from bh in db.BLACK_HOLEs
                          where bh.Black_Hole_Name.Equals(NameText.Text)
                          select bh.ID_Black_Hole;
                if (res.Count() != 0)
                {
                    MessageBox.Show("Black Hole name already exist", "Error", MessageBoxButton.OK);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(IDMText.Text))
                {
                    IDMission = int.Parse(IDMText.Text);
                }
                BLACK_HOLE b = new BLACK_HOLE
                {
                    Black_Hole_Name = NameText.Text,
                    Distance_From_Earth_Mantissa = decimal.Parse(DMText.Text),
                    Distance_From_Earth_Exp = int.Parse(DEText.Text),
                    Radius = decimal.Parse(RadiusText.Text),
                    Solar_Mass_Mantissa = decimal.Parse(MassMantissaText.Text),
                    Solar_Mass_Exp = int.Parse(MassExpText.Text),
                    ID_Galaxy = int.Parse(IDGText.Text),
                    ID_Mission_D = IDMission,

                };
                try
                {
                    db.BLACK_HOLEs.InsertOnSubmit(b);
                    db.SubmitChanges();
                }
                catch
                {
                    db.BLACK_HOLEs.DeleteOnSubmit(b);
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
            if (ChoisesComboBox.Text.Equals("Moon"))
            {
                IDSText.IsEnabled = false;
                IDPText.IsEnabled = true;
                SolarMassExpText.IsEnabled = false;
                SolarMassMantissaText.IsEnabled = false;
                MassExpText.IsEnabled = true;
                MassMantissaText.IsEnabled = true;
                IDCText.IsEnabled = false;

            }
            if (ChoisesComboBox.Text.Equals("Planet"))
            {
                IDSText.IsEnabled = true;
                IDPText.IsEnabled = false;
                SolarMassExpText.IsEnabled = false;
                SolarMassMantissaText.IsEnabled = false;
                MassExpText.IsEnabled = true;
                MassMantissaText.IsEnabled = true;
                IDCText.IsEnabled = false;

            }
            if (ChoisesComboBox.Text.Equals("Star"))
            {
                IDSText.IsEnabled = false;
                IDPText.IsEnabled = false;
                SolarMassExpText.IsEnabled = true;
                SolarMassMantissaText.IsEnabled = true;
                MassExpText.IsEnabled = false;
                MassMantissaText.IsEnabled = false;
                IDCText.IsEnabled = true;

            }
            if (ChoisesComboBox.Text.Equals("Black Hole"))
            {
                IDSText.IsEnabled = false;
                IDPText.IsEnabled = false;
                SolarMassExpText.IsEnabled = true;
                SolarMassMantissaText.IsEnabled = true;
                MassExpText.IsEnabled = false;
                MassMantissaText.IsEnabled = false;
                IDCText.IsEnabled = false;

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

        private void RadiusText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RadiusText.Text) || !IsDigitsOnly(RadiusText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void IDGText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDGText.Text) || !IsDigitsOnly(IDGText.Text))
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

        private void IDSText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDSText.Text) || !IsDigitsOnly(IDSText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void IDCText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDCText.Text) || !IsDigitsOnly(IDCText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void DMText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DMText.Text) || !IsDigitsOnly(DMText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void DEText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DEText.Text) || !IsDigitsOnly(DEText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void MassMantissaText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MassExpText.Text) || !IsDigitsOnly(MassMantissaText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void MassExpText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MassExpText.Text) || !IsDigitsOnly(MassExpText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void SolarMassMantissaText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SolarMassMantissaText.Text) ||  !IsDigitsOnly(SolarMassMantissaText.Text))
            {
                InsertButton.IsEnabled = false;
            }
            else
            {
                InsertButton.IsEnabled = true;
            }
        }

        private void SolarMassExpText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SolarMassExpText.Text) || !IsDigitsOnly(SolarMassExpText.Text))
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
