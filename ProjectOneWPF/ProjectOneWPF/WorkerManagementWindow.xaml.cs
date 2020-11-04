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
    /// Logica di interazione per WorkerManagementWindow.xaml
    /// </summary>
    public partial class WorkerManagementWindow : Window
    {
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        AdminWindow aw;
        

        public WorkerManagementWindow(AdminWindow aw)
        {
            InitializeComponent();
            this.aw = aw;
        }

       
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.aw.Show();
            this.Hide();
        }

      

        private void InsertParticipationButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDALabel.Text) || string.IsNullOrWhiteSpace(IDMLabel.Text) )
            {
                MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                return;
            }
            var res = from r in db.RECONNAISSANCEs
                      join pa in db.PARTICIPATIONs on r.ID_Mission_R equals pa.ID_Mission_R
                      where pa.ID_Astronaut.Equals(int.Parse(IDALabel.Text)) && !r.End_Date.HasValue
                      select r.ID_Mission_R;

            if(res.Count() != 0)
            {
                MessageBox.Show("The astronaut is already in another mission", "Error", MessageBoxButton.OK);
                return;
            }
            PARTICIPATION p = new PARTICIPATION
            {
                ID_Astronaut = Int32.Parse(IDALabel.Text),
                ID_Mission_R = Int32.Parse(IDMLabel.Text)
                
            };
            try
            {
                db.PARTICIPATIONs.InsertOnSubmit(p);
                db.SubmitChanges();
            }
            catch
            {
                db.PARTICIPATIONs.DeleteOnSubmit(p);
                MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);
                
            }

            IDALabel.Clear();
            IDMLabel.Clear();

        }
        private bool checkDate(string d)
        {
            string[] date = d.Split('/');
            return int.Parse(date[0]) <= 12 && int.Parse(date[1]) >= 1 && int.Parse(date[1]) < 31
                && int.Parse(date[2]) < 2050;
        }
        private void InsertEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameLabel.Text) || string.IsNullOrWhiteSpace(SurnameLabel.Text)
                || string.IsNullOrWhiteSpace(CountryLabel.Text) || string.IsNullOrWhiteSpace(DateLabel.Text))
            {
                MessageBox.Show("Fill in the required fields", "Error", MessageBoxButton.OK);
                return;
            }
            if (!checkDate(DateLabel.Text))
            {
                MessageBox.Show("The date of birth format not correct", "Error", MessageBoxButton.OK);
                return;
            }
            if (ChoisesComboBox.Text.Equals("Employee"))
            {
                EMPLOYEE ep = new EMPLOYEE
                {
                    Employee_Name = NameLabel.Text,
                    Employee_Surname = SurnameLabel.Text,
                    Country_Of_Birth = CountryLabel.Text,
                    Date_Of_Birth = Convert.ToDateTime(DateLabel.Text)
                };
                try
                {
                    db.EMPLOYEEs.InsertOnSubmit(ep);
                    db.SubmitChanges();
                }
                catch
                {
                    db.EMPLOYEEs.DeleteOnSubmit(ep);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);


                }

                NameLabel.Clear();
                SurnameLabel.Clear();
                CountryLabel.Clear();
                DateLabel.Clear();

            }
            else if (ChoisesComboBox.Text.Equals("Astronaut"))
            {
                ASTRONAUT a = new ASTRONAUT
                {
                    Astronaut_Name = NameLabel.Text,
                    Astronaut_Surname = SurnameLabel.Text,
                    Country_Of_Birth = CountryLabel.Text,
                    Date_Of_Birth = Convert.ToDateTime(DateLabel.Text)
                };
                try
                {
                    db.ASTRONAUTs.InsertOnSubmit(a);
                    db.SubmitChanges();
                }
                catch
                {
                    db.ASTRONAUTs.DeleteOnSubmit(a);
                    MessageBox.Show("An Error has occurred", "Error", MessageBoxButton.OK);


                }

                NameLabel.Clear();
                SurnameLabel.Clear();
                CountryLabel.Clear();
                DateLabel.Clear();

            }


        }

        

        private void IDMLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDMLabel.Text) || !IsDigitsOnly(IDMLabel.Text))
            {
                InsertParticipationButton.IsEnabled = false;
            }
            else
            {
                InsertParticipationButton.IsEnabled = true;
            }
        }

        private void IDALabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IDALabel.Text) || !IsDigitsOnly(IDALabel.Text))
            {
                InsertParticipationButton.IsEnabled = false;
            }
            else
            {
                InsertParticipationButton.IsEnabled = true;
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
