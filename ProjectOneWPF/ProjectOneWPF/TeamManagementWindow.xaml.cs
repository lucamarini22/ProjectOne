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
    /// Logica di interazione per WindowManagementTeam.xaml
    /// </summary>
    public partial class TeamManagementWindow : Window
    {
        AdminWindow aw;
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        public TeamManagementWindow(AdminWindow aw)
        {
            InitializeComponent();
            this.aw = aw;
        }

        private bool checkDate(string d)
        {
            string[] date = d.Split('/');
            return int.Parse(date[0]) <= 12 && int.Parse(date[1]) >= 1 && int.Parse(date[1]) <= 31
                && int.Parse(date[2]) <= 2050;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.aw.Show();
            this.Hide();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(IDELabel.Text) || string.IsNullOrWhiteSpace(IDTLabel.Text)
                  || string.IsNullOrWhiteSpace(DateLabel.Text))
            {
                MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                return;
            }

            if (!checkDate(DateLabel.Text))
            {
                MessageBox.Show("The date format is not correct", "Error", MessageBoxButton.OK);
                return;
            }

            if(Convert.ToDateTime(DateLabel.Text) > DateTime.Now)
                {
                MessageBox.Show("The date inserted is in the future", "Error", MessageBoxButton.OK);
                return;
            }



            var res = (from m in db.MEMBERSHIPs
                       where m.ID_Employee == Int32.Parse(IDELabel.Text) && m.ID_Team == Int32.Parse(IDTLabel.Text)
                       select m).Count();

            var res2 = (from m in db.MEMBERSHIPs
                        where m.ID_Employee == Int32.Parse(IDELabel.Text) && !m.Exit_Date.HasValue
                        select m).Count();

            var res3 =  from m in db.MEMBERSHIPs
                        join t in db.TEAMs on m.ID_Employee equals t.ID_Responsible
                        where t.ID_Team == Int32.Parse(IDTLabel.Text)
                        && !m.Exit_Date.HasValue
                        select m.Entry_Date;


            if (res != 0)
            {
                MessageBox.Show("The Employee had already joined the Team", "Error", MessageBoxButton.OK);
                IDTLabel.Clear();
                IDELabel.Clear();
                return;
            }
            if (res2 != 0)
            {
                MessageBox.Show("The Employee is already a member of a team", "Error", MessageBoxButton.OK);
                IDTLabel.Clear();
                IDELabel.Clear();
                return;

            }
            if (string.IsNullOrWhiteSpace(IDELabel.Text) || string.IsNullOrWhiteSpace(IDTLabel.Text)
                    || string.IsNullOrWhiteSpace(DateLabel.Text))
            {
                MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                return;
            }
            if (res3.First().Date > Convert.ToDateTime(DateLabel.Text)) {
                MessageBox.Show("The inserted date is earlier than the date of the creation of the team", "Error", MessageBoxButton.OK);
                return;
            }
            else
            {


                MEMBERSHIP m = new MEMBERSHIP
                {
                    ID_Employee = Int32.Parse(IDELabel.Text),
                    ID_Team = Int32.Parse(IDTLabel.Text),
                    Entry_Date = Convert.ToDateTime(DateLabel.Text)
                };

                try
                {
                    db.MEMBERSHIPs.InsertOnSubmit(m);
                    db.SubmitChanges();
                }
                catch
                {
                    db.MEMBERSHIPs.DeleteOnSubmit(m);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);

                }

                IDELabel.Clear();
                IDTLabel.Clear();
                DateLabel.Clear();
            }

        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameLabel.Text) || string.IsNullOrWhiteSpace(IDRLabel.Text)
                    || string.IsNullOrWhiteSpace(DateTeamLabel.Text))
            {
                MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                return;
            }
            if (!checkDate(DateTeamLabel.Text))
            {
                MessageBox.Show("The date format is not correct", "Error", MessageBoxButton.OK);
                return;
            }
            if (Convert.ToDateTime(DateTeamLabel.Text) > DateTime.Now)
            {
                MessageBox.Show("The date inserted is in the future", "Error", MessageBoxButton.OK);
                return;
            }
            var res = (from mm in db.MEMBERSHIPs
                        where mm.ID_Employee == Int32.Parse(IDRLabel.Text) && !mm.Exit_Date.HasValue
                        select mm).Count();
            if (res != 0)
            {
                MessageBox.Show("The Employee is already a member of a team", "Error", MessageBoxButton.OK);
                IDTLabel.Clear();
                IDELabel.Clear();
                return;

            }
           

            TEAM t = new TEAM
            {
                Team_Name = NameLabel.Text,
                ID_Responsible = Int32.Parse(IDRLabel.Text)
                
            };

            try
            {
                db.TEAMs.InsertOnSubmit(t);
                db.SubmitChanges();
            }
            catch
            {
                db.TEAMs.DeleteOnSubmit(t);
                MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);
                
            }

            var ID_Team = from team in db.TEAMs
                          where team.Team_Name == NameLabel.Text
                          select team.ID_Team;

            MEMBERSHIP m = new MEMBERSHIP
            {
                ID_Employee = int.Parse(IDRLabel.Text),
                ID_Team = ID_Team.First(),
                Entry_Date = Convert.ToDateTime(DateTeamLabel.Text)
            };

            try
            {
                db.MEMBERSHIPs.InsertOnSubmit(m);
                db.SubmitChanges();
            }
            catch
            {
                db.MEMBERSHIPs.DeleteOnSubmit(m);
                MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);

            }


            NameLabel.Clear();
            IDRLabel.Clear();
            DateTeamLabel.Clear();
        }

   

        private void IDRLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDRLabel.Text) || !IsDigitsOnly(IDRLabel.Text))
            {
                ExecManagementTeam.IsEnabled = false;
            }
            else
            {
                ExecManagementTeam.IsEnabled = true;
            }
        }

        private void IDELabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDELabel.Text) || !IsDigitsOnly(IDELabel.Text))
            {
                AddEmployeeButton.IsEnabled = false;
            }
            else
            {
                AddEmployeeButton.IsEnabled = true;
            }
        }

        private void IDTLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDTLabel.Text) || !IsDigitsOnly(IDTLabel.Text))
            {
                AddEmployeeButton.IsEnabled = false;
            }
            else
            {
                AddEmployeeButton.IsEnabled = true;
            }
        }

        private void DateLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DateLabel.Text))
            {
                AddEmployeeButton.IsEnabled = false;
            }
            else
            {
                AddEmployeeButton.IsEnabled = true;
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

       
    }
}
