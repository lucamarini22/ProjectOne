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
    /// Logica di interazione per ResponsibleEmployeeWindow.xaml
    /// </summary>
    public partial class ResponsibleEmployeeWindow : Window
    {
        private int idempl;
        private int idteam;
        MainWindow mw;
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        public ResponsibleEmployeeWindow(int IDEmpl, int IDTeam, MainWindow mw)
        {


            InitializeComponent();
            this.idempl = IDEmpl;
            this.idteam = IDTeam;
            this.mw = mw;
            var res = from ep in db.EMPLOYEEs
                      where ep.ID_Employee.Equals(this.idempl)
                      select new
                      {
                          Surname = ep.Employee_Surname
                      };

            SurnameLabel.Content = "Welcome " + res.First().Surname;

            var res1 = from t in db.TEAMs
                       where t.ID_Team.Equals(this.idteam)
                       select new
                       {
                           TeamName = t.Team_Name
                       };
            TeamLabel.Content = "Team: " + res1.First().TeamName;
        }

        private void ViewSimulationButton_Click(object sender, RoutedEventArgs e)
        {
            var res = from s in db.SIMULATIONs
                      join t in db.TEAMs on s.ID_Team equals t.ID_Team
                      join m in db.MEMBERSHIPs on t.ID_Team equals m.ID_Team
                      join ep in db.EMPLOYEEs on m.ID_Employee equals ep.ID_Employee
                      join p in db.PLANETs on s.ID_Planet equals p.ID_Planet
                      where ep.ID_Employee.Equals(this.idempl)
                      select new
                      {
                          SimulationDate = s.Simulation_Date,
                          LabNumber = s.Lab_Number,
                          Description = s.Simulation_Description,
                          Team = t.Team_Name,
                          Planet = p.Planet_Name
                      };
            DataGridSimulation.ItemsSource = res;
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mw.Show();
        }

        
        private void MissionTypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (MissionTypeComboBox.Text.Equals("Reconnaissance"))
            {
                var res = from r in db.RECONNAISSANCEs                          
                          join s in db.SPACECRAFTs on r.ID_Spacecraft equals s.ID_Spacecraft
                          join plan in db.PLANETs on r.ID_Planet equals plan.ID_Planet into pJr
                          from plan in pJr.DefaultIfEmpty()
                          join t in db.TEAMs on r.ID_Team equals t.ID_Team
                          join memb in db.MEMBERSHIPs on t.ID_Team equals memb.ID_Team
                          join emp in db.EMPLOYEEs on memb.ID_Employee equals emp.ID_Employee
                          join l in db.LAUNCH_SITEs on r.ID_Launch_Site equals l.ID_Launch_Site
                          join m in db.MOONs on r.ID_Moon equals m.ID_Moon into mJr
                          from m in mJr.DefaultIfEmpty()
                          join ss in db.SPACE_STATIONs on r.ID_Space_Station equals ss.ID_Space_Station into ssJr
                          from ss in ssJr.DefaultIfEmpty()
                          where emp.ID_Employee.Equals(this.idempl)
                          select new
                          {
                              MissionName = r.Mission_R_Name,
                              Description = r.Mission_R_Description,
                              BeginDate = r.Begin_Date,
                              EndDate = r.End_Date,
                              LaunchSite = l.Launch_Site_Name,
                              Team = t.Team_Name,
                              SpaceStation = ss.Space_Station_Name,
                              Planet = plan.Planet_Name,
                              Moon = m.Moon_Name,
                              Spacecraft = s.Spacecraft_Name
                          };
                MissionDataGrid.ItemsSource = res;
            }
            else if(MissionTypeComboBox.Text.Equals("Deliver in orbit"))
            {
                var res = from d in db.DELIVER_IN_ORBITs
                          join plan in db.PLANETs on d.ID_Planet equals plan.ID_Planet 
                          join t in db.TEAMs on d.ID_Team equals t.ID_Team
                          join s in db.SATELLITEs on d.ID_Satellite equals s.ID_Satellite
                          join memb in db.MEMBERSHIPs on t.ID_Team equals memb.ID_Team
                          join emp in db.EMPLOYEEs on memb.ID_Employee equals emp.ID_Employee
                          join l in db.LAUNCH_SITEs on d.ID_Launch_Site equals l.ID_Launch_Site
                          join ss in db.SPACE_STATIONs on d.ID_Space_Station equals ss.ID_Space_Station into ssJd
                          from ss in ssJd.DefaultIfEmpty()
                          where emp.ID_Employee.Equals(this.idempl)
                          select new
                          {
                              MissionName = d.Mission_DO_Name,
                              Description = d.Mission_DO_Description,
                              BeginDate = d.Begin_Date,
                              EndDate = d.End_Date,
                              LaunchSite = l.Launch_Site_Name,
                              Team = t.Team_Name,
                              SpaceStation = ss.Space_Station_Name,
                              Planet = plan.Planet_Name,
                              Satellite = s.Satellite_Name
                          };
                MissionDataGrid.ItemsSource = res;
            }else if (MissionTypeComboBox.Text.Equals("Discover"))
            {
                var res = from d in db.DISCOVERs
                          join t in db.TEAMs on d.ID_Team equals t.ID_Team
                          join s in db.SATELLITEs on d.ID_Satellite equals s.ID_Satellite
                          join memb in db.MEMBERSHIPs on t.ID_Team equals memb.ID_Team
                          join emp in db.EMPLOYEEs on memb.ID_Employee equals emp.ID_Employee
                          join l in db.LAUNCH_SITEs on d.ID_Launch_Site equals l.ID_Launch_Site
                          join ss in db.SPACE_STATIONs on d.ID_Space_Station equals ss.ID_Space_Station into ssJd
                          from ss in ssJd.DefaultIfEmpty()
                          where emp.ID_Employee.Equals(this.idempl)
                          select new
                          {
                              MissionName = d.Mission_D_Name,
                              Description = d.Mission_D_Description,
                              BeginDate = d.Begin_Date,
                              EndDate = d.End_Date,
                              LaunchSite = l.Launch_Site_Name,
                              Team = t.Team_Name,
                              SpaceStation = ss.Space_Station_Name,
                              Satellite = s.Satellite_Name
                          };
                MissionDataGrid.ItemsSource = res;
            }
        }
    }
}
