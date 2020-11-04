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
    /// Interaction logic for AstronautLogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
       
        DataBaseDataClassesDataContext db=new DataBaseDataClassesDataContext();
        MainWindow mw;
        string type;
        public LogWindow(string type,MainWindow mw)
        {
            InitializeComponent();
            LogText.Focus();
            LogButton.IsEnabled = false;
            this.mw = mw;
            this.type = type;
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.type.Equals(this.mw.AstronautButton.Content.ToString()))
            {
                var res = from a in db.ASTRONAUTs
                          where a.ID_Astronaut == int.Parse(LogText.Text)
                          select a.ID_Astronaut;
                //res.or
                if (res.Count() == 0)
                {
                    MessageBox.Show("Sorry but your ID doesn't exist in the DataBase");

                }
                else
                {
                    AstronautWindow asw = new AstronautWindow(int.Parse(LogText.Text), this.mw);
                    asw.Show();
                    this.mw.Hide();
                    this.Hide();
                }
            }
            else if (this.type.Equals(this.mw.EmployeeButton.Content.ToString())) {

                

                var res = from ep in db.EMPLOYEEs
                          join m in db.MEMBERSHIPs on ep.ID_Employee equals m.ID_Employee
                          join t in db.TEAMs on m.ID_Team equals t.ID_Team
                          select new
                          {
                              IDEmployee = ep.ID_Employee,
                              IDResponsible = t.ID_Responsible,
                              IDTeam = t.ID_Team,
                              ExitDate = m.Exit_Date
                          };



                
                var res2 = res.Where(l => l.IDEmployee == int.Parse(LogText.Text));

                if (res2.Count() == 0)
                {
                    MessageBox.Show("Sorry but your ID doesn't exist in the DataBase");
                }
                else if (res2.Where(l => l.IDEmployee == l.IDResponsible && !l.ExitDate.HasValue).Count() != 0 )
                { 
                    ResponsibleEmployeeWindow rew = new ResponsibleEmployeeWindow(res2.First().IDEmployee, res2.First().IDTeam,this.mw);
                    rew.Show();
                }else
                {
                    SimpleEmployeeWindow sew = new SimpleEmployeeWindow(int.Parse(LogText.Text), this.mw);
                    sew.Show();
                }
                this.Close();
            
                
            
            }



        }

        
        private void LogText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LogText.Text) || !IsDigitsOnly(LogText.Text))
            {
                LogButton.IsEnabled = false;
            }else
            {
                LogButton.IsEnabled = true;
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
