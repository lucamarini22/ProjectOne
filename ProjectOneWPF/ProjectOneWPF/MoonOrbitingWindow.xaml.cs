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
    /// Interaction logic for MoonOrbitingWindow.xaml
    /// </summary>
    public partial class MoonOrbitingWindow : Window
    {
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        UserWindow uw;
        public MoonOrbitingWindow(UserWindow uw)
        {
            var res = from m in db.MOONs
                      join p in db.PLANETs on m.ID_Planet equals p.ID_Planet
                      select p.Planet_Name;

           
            InitializeComponent();
            res.Distinct().ToList().ForEach(l => PlanetComboBox.Items.Add(l));
            
            this.uw = uw;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.uw.Show();
            this.Close();
        }

        private void PlanetComboBox_DropDownClosed(object sender, EventArgs e)
        {
            MoonLabel.Content = "Moons orbiting around " + PlanetComboBox.Text;
            if (PlanetComboBox.Text != "")
            {
                var res = from m in db.MOONs
                          join p in db.PLANETs on m.ID_Planet equals p.ID_Planet
                          where p.Planet_Name.Equals(PlanetComboBox.Text)
                          select new
                          {
                              Name = m.Moon_Name,
                              DistanceFromEarth = m.Distance_From_Earth_Mantissa.ToString() + " x 10^" + m.Distance_From_Earth_Exp.ToString() + " Km",
                              DimensionRadius = m.Radius.ToString() + " km",
                              Mass = m.Mass_Mantissa.ToString() + " 10^" + m.Mass_Exp.ToString() + " Kg"
                          };
                DataGrid.ItemsSource = res;
            }
        }

        
    }
}
