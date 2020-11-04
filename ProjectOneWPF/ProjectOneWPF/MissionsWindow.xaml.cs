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
    /// Interaction logic for MissionsWindow.xaml
    /// </summary>
    public partial class MissionsWindow : Window
    {
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        UserWindow uw;
        public MissionsWindow(UserWindow uw)
        {
            var rrec = from rec in db.RECONNAISSANCEs
                      select new
                      {
                          Name = rec.Mission_R_Name,
                          BeginDate = rec.Begin_Date,
                          EndDate = rec.End_Date,
                          Description = rec.Mission_R_Description
                      };
            var rdel = from del in db.DELIVER_IN_ORBITs
                       select new
                       {
                           Name = del.Mission_DO_Name,
                           BeginDate = del.Begin_Date,
                           EndDate = del.End_Date,
                           Description = del.Mission_DO_Description
                       };
            var rdis = from dis in db.DISCOVERs
                       select new
                       {
                           Name = dis.Mission_D_Name,
                           BeginDate = dis.Begin_Date,
                           EndDate = dis.End_Date,
                           Description = dis.Mission_D_Description
                       };
            InitializeComponent();
            DisDataGrid.ItemsSource = rdis;
            DelDataGrid.ItemsSource = rdel;
            RecDataGrid.ItemsSource = rrec;
            this.uw = uw;
        }

        private void CompletedRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var rrec = from rec in db.RECONNAISSANCEs
                       where rec.End_Date != null
                       select new
                       {
                           Name = rec.Mission_R_Name,
                           BeginDate = rec.Begin_Date,
                           EndDate = rec.End_Date,
                           Description = rec.Mission_R_Description
                       };
            var rdel = from del in db.DELIVER_IN_ORBITs
                       where del.End_Date != null
                       select new
                       {
                           Name = del.Mission_DO_Name,
                           BeginDate = del.Begin_Date,
                           EndDate = del.End_Date,
                           Description = del.Mission_DO_Description
                       };
            var rdis = from dis in db.DISCOVERs
                       where dis.End_Date != null
                       select new
                       {
                           Name = dis.Mission_D_Name,
                           BeginDate = dis.Begin_Date,
                           EndDate = dis.End_Date,
                           Description = dis.Mission_D_Description
                       };

            DisDataGrid.ItemsSource = rdis;
            DelDataGrid.ItemsSource = rdel;
            RecDataGrid.ItemsSource = rrec;
        }

        private void AllRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var rrec = from rec in db.RECONNAISSANCEs
                       select new
                       {
                           Name = rec.Mission_R_Name,
                           BeginDate = rec.Begin_Date,
                           EndDate = rec.End_Date,
                           Description = rec.Mission_R_Description
                       };
            var rdel = from del in db.DELIVER_IN_ORBITs
                       select new
                       {
                           Name = del.Mission_DO_Name,
                           BeginDate = del.Begin_Date,
                           EndDate = del.End_Date,
                           Description = del.Mission_DO_Description
                       };
            var rdis = from dis in db.DISCOVERs
                       select new
                       {
                           Name = dis.Mission_D_Name,
                           BeginDate = dis.Begin_Date,
                           EndDate = dis.End_Date,
                           Description = dis.Mission_D_Description
                       };

            DisDataGrid.ItemsSource = rdis;
            DelDataGrid.ItemsSource = rdel;
            RecDataGrid.ItemsSource = rrec;
        }

        private void NotCompletedRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var rrec = from rec in db.RECONNAISSANCEs
                       where rec.End_Date == null
                       select new
                       {
                           Name = rec.Mission_R_Name,
                           BeginDate = rec.Begin_Date,
                           EndDate = rec.End_Date,
                           Description = rec.Mission_R_Description
                       };
            var rdel = from del in db.DELIVER_IN_ORBITs
                       where del.End_Date == null
                       select new
                       {
                           Name = del.Mission_DO_Name,
                           BeginDate = del.Begin_Date,
                           EndDate = del.End_Date,
                           Description = del.Mission_DO_Description
                       };
            var rdis = from dis in db.DISCOVERs
                       where dis.End_Date == null
                       select new
                       {
                           Name = dis.Mission_D_Name,
                           BeginDate = dis.Begin_Date,
                           EndDate = dis.End_Date,
                           Description = dis.Mission_D_Description
                       };

            DisDataGrid.ItemsSource = rdis;
            DelDataGrid.ItemsSource = rdel;
            RecDataGrid.ItemsSource = rrec;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.uw.Show();
            this.Close();
        }
    }
}
