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
    /// Interaction logic for AstronautWindow.xaml
    /// </summary>
    /// 
    
    public partial class AstronautWindow : Window
    {
        private int id;
        MainWindow mw;
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        public AstronautWindow(int ID,MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
            this.id = ID;           
            var res = from a in db.ASTRONAUTs
                      where a.ID_Astronaut == ID
                      select new
                      {
                          Surname = a.Astronaut_Surname
                      };
            AstronautLabel.Content = "Welcome eng. " + res.First().Surname.ToString();

            var res2 = from r in db.RECONNAISSANCEs
                       join p in db.PARTICIPATIONs on r.ID_Mission_R equals p.ID_Mission_R
                       join s in db.SPACECRAFTs on r.ID_Spacecraft equals s.ID_Spacecraft
                       join plan in db.PLANETs on r.ID_Planet equals plan.ID_Planet into pJr
                       from plan in pJr.DefaultIfEmpty()
                       join t in db.TEAMs on r.ID_Team equals t.ID_Team
                       join l in db.LAUNCH_SITEs on r.ID_Launch_Site equals l.ID_Launch_Site
                       join m in db.MOONs on r.ID_Moon equals m.ID_Moon into mJr
                       from m in mJr.DefaultIfEmpty()
                       join ss in db.SPACE_STATIONs on r.ID_Space_Station equals ss.ID_Space_Station into ssJr
                       from ss in ssJr.DefaultIfEmpty()
                  select new
                  {
                      p.ID_Astronaut,
                      Name = r.Mission_R_Name,
                      Description = r.Mission_R_Description,
                      BeginDate = r.Begin_Date,
                      EndDate=r.End_Date,
                      LaunchSite=l.Launch_Site_Name,
                      Team = t.Team_Name,
                      SpaceStation=ss.Space_Station_Name,
                      Planet=plan.Planet_Name,
                      Moon=m.Moon_Name,
                      Spacecraft=s.Spacecraft_Name
                  };
            var res3 = res2.Where(l => l.ID_Astronaut.Equals(this.id));
            DataGridCompleted.ItemsSource = res3.Where(l => l.EndDate.HasValue);
            DataGridNotCompleted.ItemsSource = res3.Where(l => !l.EndDate.HasValue);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {            
            mw.Show();
            this.Close();
        }
    }
}
