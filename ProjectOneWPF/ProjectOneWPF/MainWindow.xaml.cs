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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectOneWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow aw = new AdminWindow(this);
            aw.Show();
            this.Hide();
        }

        private void AstronautButton_Click(object sender, RoutedEventArgs e)
        {
            LogWindow lw = new LogWindow(this.AstronautButton.Content.ToString(),this);
            lw.ShowDialog();
            
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow uw = new UserWindow(this);
            uw.Show();
            this.Hide();
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            LogWindow lw = new LogWindow(this.EmployeeButton.Content.ToString(), this);
            lw.ShowDialog();
        }
    }
}
