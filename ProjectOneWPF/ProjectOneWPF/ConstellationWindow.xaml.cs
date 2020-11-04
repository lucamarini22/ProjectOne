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
    /// Interaction logic for ConstellationWindow.xaml
    /// </summary>
    public partial class ConstellationWindow : Window
    {
        UserWindow uw;
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        public ConstellationWindow(UserWindow uw)
        {
            InitializeComponent();
            this.uw = uw;
            var res = from c in db.CONSTELLATIONs
                      select new
                      {
                          ID = c.ID_Constellation,
                          Name = c.Constellation_Name
                      };
            DataGrid.ItemsSource = res;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.uw.Show();
            this.Hide();
        }
    }
}
