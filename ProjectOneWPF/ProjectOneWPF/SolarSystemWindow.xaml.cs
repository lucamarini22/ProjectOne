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
    /// Interaction logic for SolarSystemWindow.xaml
    /// </summary>
    public partial class SolarSystemWindow : Window
    {
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        UserWindow uw;
        public SolarSystemWindow(UserWindow uw)
        {
            var res = from p in db.PLANETs
                      join s in db.STARs on p.ID_Star equals s.ID_Star
                      where s.Star_Name.Equals("Sun")
                      select new
                      {
                          Name = p.Planet_Name,
                          DistanceFromEarth = p.Distance_From_Earth_Mantissa.ToString() + " x 10^" + p.Distance_From_Earth_Exp.ToString() + " Km",
                          DimensionRadius = p.Radius.ToString() + " km",
                          Mass = p.Mass_Mantissa.ToString() + " 10^" + p.Mass_Exp.ToString() + " Kg"
                      };
            InitializeComponent();
            DataGrid.ItemsSource = res;
            this.uw = uw;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.uw.Show();
            this.Close();
        }
    }
}
