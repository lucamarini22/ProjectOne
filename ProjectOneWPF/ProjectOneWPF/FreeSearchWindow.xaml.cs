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
    /// Interaction logic for FreeSearchWindow.xaml
    /// </summary>
    public partial class FreeSearchWindow : Window
    {
        DataBaseDataClassesDataContext db = new DataBaseDataClassesDataContext();
        UserWindow uw;
        decimal maxradius = decimal.MaxValue, minradius = decimal.MinValue;
        int maxexp = int.MaxValue, minexp = int.MinValue;
        bool viewall=false;
        public FreeSearchWindow(UserWindow uw)
        {
            InitializeComponent();
            this.uw = uw;
        }

        private void ViewAllCheckBox_Checked(object sender, RoutedEventArgs e)
        {
           
            this.viewall = !viewall;
            MinDistExpText.IsEnabled = !MinDistExpText.IsEnabled;
            MinRadiusText.IsEnabled = !MinRadiusText.IsEnabled;
            MaxDistExpText.IsEnabled = !MaxDistExpText.IsEnabled;
            MaxRadiusText.IsEnabled = !MaxRadiusText.IsEnabled;
            NameText.IsEnabled = !NameText.IsEnabled;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.uw.Show();
            this.Hide();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {


            if (viewall)
            {
                if (TypeComboBox.Text.Equals("Planet"))
                {
                    var res = from p in db.PLANETs
                              join g in db.GALAXies on p.ID_Galaxy equals g.ID_Galaxy
                              join s in db.STARs on p.ID_Star equals s.ID_Star
                              join dm in db.DISCOVERs on p.ID_Mission_D equals dm.ID_Mission_D into dmJp
                              from dm in dmJp.DefaultIfEmpty()
                              select new
                              {
                                  Name = p.Planet_Name,
                                  DistanceFromEarth = p.Distance_From_Earth_Mantissa.ToString() + " x 10^" + p.Distance_From_Earth_Exp.ToString() + " Km",
                                  DimensionRadius = p.Radius.ToString() + " km",
                                  Mass = p.Mass_Mantissa.ToString() + " 10^" + p.Mass_Exp.ToString() + " Kg",
                                  Galaxy = g.Galaxy_Name,
                                  OrbitingAround = s.Star_Name,
                                  DiscoverMission = dm.Mission_D_Name
                              };
                    DataGrid.ItemsSource = res;

                }
                else if (TypeComboBox.Text.Equals("Moon"))
                {
                    var res = from m in db.MOONs
                              join g in db.GALAXies on m.ID_Galaxy equals g.ID_Galaxy
                              join p in db.PLANETs on m.ID_Planet equals p.ID_Planet
                              join dm in db.DISCOVERs on m.ID_Mission_D equals dm.ID_Mission_D into dmJm
                              from dm in dmJm.DefaultIfEmpty()
                              select new
                              {
                                  Name = m.Moon_Name,
                                  DistanceFromEarth = m.Distance_From_Earth_Mantissa.ToString() + " x 10^" + m.Distance_From_Earth_Exp.ToString() + " Km",
                                  DimensionRadius = m.Radius.ToString() + " km",
                                  Mass = m.Mass_Mantissa.ToString() + " 10^" + m.Mass_Exp.ToString() + " Kg",
                                  Galaxy = g.Galaxy_Name,
                                  OrbitingAround = p.Planet_Name,
                                  DiscoverMission = dm.Mission_D_Name
                              };
                    DataGrid.ItemsSource = res;
                }
                else if (TypeComboBox.Text.Equals("Black Hole"))
                {
                    var res = from bh in db.BLACK_HOLEs
                              join g in db.GALAXies on bh.ID_Galaxy equals g.ID_Galaxy
                              join dm in db.DISCOVERs on bh.ID_Mission_D equals dm.ID_Mission_D into dmJbh
                              from dm in dmJbh.DefaultIfEmpty()
                              select new
                              {
                                  Name = bh.Black_Hole_Name,
                                  DistanceFromEarth = bh.Distance_From_Earth_Mantissa.ToString() + " x 10^" + bh.Distance_From_Earth_Exp.ToString() + " ly",
                                  DimensionRadius = bh.Radius.ToString() + " km",
                                  Mass = bh.Solar_Mass_Mantissa.ToString() + " 10^" + bh.Solar_Mass_Exp.ToString() + " sm",
                                  Galaxy = g.Galaxy_Name,
                                  DiscoverMission = dm.Mission_D_Name
                              };
                    DataGrid.ItemsSource = res;
                }
                else if (TypeComboBox.Text.Equals("Star"))
                {
                    var res = from s in db.STARs
                              join g in db.GALAXies on s.ID_Galaxy equals g.ID_Galaxy
                              join c in db.CONSTELLATIONs on s.ID_Constellation equals c.ID_Constellation into cJs
                              from c in cJs.DefaultIfEmpty()
                              join dm in db.DISCOVERs on s.ID_Mission_D equals dm.ID_Mission_D into dmJs
                              from dm in dmJs.DefaultIfEmpty()
                              select new
                              {
                                  Name = s.Star_Name,
                                  DistanceFromEarth = s.Distance_From_Earth_Mantissa.ToString() + " x 10^" + s.Distance_From_Earth_Exp.ToString() + " ly",
                                  DimensionRadius = s.Radius.ToString() + " km",
                                  Mass = s.Solar_Mass_Mantissa.ToString() + " 10^" + s.Solar_Mass_Exp.ToString() + " sm",
                                  Galaxy = g.Galaxy_Name,
                                  Constellation = c.Constellation_Name,
                                  DiscoverMission = dm.Mission_D_Name
                              };
                    DataGrid.ItemsSource = res;
                }

            }
            else
            {
                if (MinDistExpText.Text != "")
                {
                    this.minexp = int.Parse(MinDistExpText.Text);
                }
                if (MaxDistExpText.Text != "")
                {
                    this.maxexp = int.Parse(MaxDistExpText.Text);
                }
                if (MaxRadiusText.Text != "")
                {
                    this.maxradius = decimal.Parse(MaxRadiusText.Text);
                }
                if (MinRadiusText.Text != "")
                {
                    this.minradius = decimal.Parse(MinRadiusText.Text);
                }


                if (TypeComboBox.Text == "Planet")
                {

                    if (NameText.Text != "")
                    {

                        var res = from p in db.PLANETs
                                  join g in db.GALAXies on p.ID_Galaxy equals g.ID_Galaxy
                                  join s in db.STARs on p.ID_Star equals s.ID_Star
                                  join dm in db.DISCOVERs on p.ID_Mission_D equals dm.ID_Mission_D into dmJp
                                  from dm in dmJp.DefaultIfEmpty()
                                  where p.Distance_From_Earth_Exp >= this.minexp && p.Distance_From_Earth_Exp <= this.maxexp &&
                                        p.Radius >= this.minradius && p.Radius <= this.maxradius
                                        && p.Planet_Name.Equals(NameText.Text)
                                  select new
                                  {
                                      Name = p.Planet_Name,
                                      DistanceFromEarth = p.Distance_From_Earth_Mantissa.ToString() + " x 10^" + p.Distance_From_Earth_Exp.ToString() + " Km",
                                      DimensionRadius = p.Radius.ToString() + " km",
                                      Mass = p.Mass_Mantissa.ToString() + " 10^" + p.Mass_Exp.ToString() + " Kg",
                                      Galaxy = g.Galaxy_Name,
                                      OrbitingAround = s.Star_Name,
                                      DiscoverMission = dm.Mission_D_Name
                                  };
                        DataGrid.ItemsSource = res;
                    }
                    else
                    {
                        var res = from p in db.PLANETs
                                  join g in db.GALAXies on p.ID_Galaxy equals g.ID_Galaxy
                                  join s in db.STARs on p.ID_Star equals s.ID_Star
                                  join dm in db.DISCOVERs on p.ID_Mission_D equals dm.ID_Mission_D into dmJp
                                  from dm in dmJp.DefaultIfEmpty()
                                  where p.Distance_From_Earth_Exp >= this.minexp && p.Distance_From_Earth_Exp <= this.maxexp &&
                                        p.Radius >= this.minradius && p.Radius <= this.maxradius
                                  select new
                                  {
                                      Name = p.Planet_Name,
                                      DistanceFromEarth = p.Distance_From_Earth_Mantissa.ToString() + " x 10^" + p.Distance_From_Earth_Exp.ToString() + " Km",
                                      DimensionRadius = p.Radius.ToString() + " km",
                                      Mass = p.Mass_Mantissa.ToString() + " 10^" + p.Mass_Exp.ToString() + " Kg",
                                      Galaxy = g.Galaxy_Name,
                                      OrbitingAround = s.Star_Name,
                                      DiscoverMission = dm.Mission_D_Name
                                  };
                        DataGrid.ItemsSource = res;
                    }



                }
                else if (TypeComboBox.Text == "Moon")
                {
                    if (NameText.Text != "")
                    {
                        var res = from m in db.MOONs
                                  join g in db.GALAXies on m.ID_Galaxy equals g.ID_Galaxy
                                  join p in db.PLANETs on m.ID_Planet equals p.ID_Planet
                                  join dm in db.DISCOVERs on m.ID_Mission_D equals dm.ID_Mission_D into dmJm
                                  from dm in dmJm.DefaultIfEmpty()
                                  where m.Distance_From_Earth_Exp >= this.minexp && m.Distance_From_Earth_Exp <= this.maxexp &&
                                   m.Radius >= this.minradius && m.Radius <= this.maxradius
                                   && m.Moon_Name.Equals(NameText.Text)
                                  select new
                                  {
                                      Name = m.Moon_Name,
                                      DistanceFromEarth = m.Distance_From_Earth_Mantissa.ToString() + " x 10^" + m.Distance_From_Earth_Exp.ToString() + " Km",
                                      DimensionRadius = m.Radius.ToString() + " km",
                                      Mass = m.Mass_Mantissa.ToString() + " 10^" + m.Mass_Exp.ToString() + " Kg",
                                      Galaxy = g.Galaxy_Name,
                                      OrbitingAround = p.Planet_Name,
                                      DiscoverMission = dm.Mission_D_Name
                                  };
                        DataGrid.ItemsSource = res;
                    }
                    else
                    {
                        var res = from m in db.MOONs
                                  join g in db.GALAXies on m.ID_Galaxy equals g.ID_Galaxy
                                  join p in db.PLANETs on m.ID_Planet equals p.ID_Planet
                                  join dm in db.DISCOVERs on m.ID_Mission_D equals dm.ID_Mission_D into dmJm
                                  from dm in dmJm.DefaultIfEmpty()
                                  where m.Distance_From_Earth_Exp >= this.minexp && m.Distance_From_Earth_Exp <= this.maxexp &&
                                   m.Radius >= this.minradius && m.Radius <= this.maxradius
                                  select new
                                  {
                                      Name = m.Moon_Name,
                                      DistanceFromEarth = m.Distance_From_Earth_Mantissa.ToString() + " x 10^" + m.Distance_From_Earth_Exp.ToString() + " Km",
                                      DimensionRadius = m.Radius.ToString() + " km",
                                      Mass = m.Mass_Mantissa.ToString() + " 10^" + m.Mass_Exp.ToString() + " Kg",
                                      Galaxy = g.Galaxy_Name,
                                      OrbitingAround = p.Planet_Name,
                                      DiscoverMission = dm.Mission_D_Name
                                  };
                        DataGrid.ItemsSource = res;
                    }


                }else if (TypeComboBox.Text.Equals("Black Hole"))
                {
                    if(NameText.Text != "")
                    {
                        var res = from bh in db.BLACK_HOLEs
                                  join g in db.GALAXies on bh.ID_Galaxy equals g.ID_Galaxy
                                  join dm in db.DISCOVERs on bh.ID_Mission_D equals dm.ID_Mission_D into dmJbh
                                  from dm in dmJbh.DefaultIfEmpty()
                                  where bh.Distance_From_Earth_Exp >= this.minexp && bh.Distance_From_Earth_Exp <= this.maxexp &&
                                   bh.Radius >= this.minradius && bh.Radius <= this.maxradius &&
                                   bh.Black_Hole_Name.Equals(NameText.Text)
                                  select new
                                  {
                                      Name = bh.Black_Hole_Name,
                                      DistanceFromEarth = bh.Distance_From_Earth_Mantissa.ToString() + " x 10^" + bh.Distance_From_Earth_Exp.ToString() + " ly",
                                      DimensionRadius = bh.Radius.ToString() + " km",
                                      Mass = bh.Solar_Mass_Mantissa.ToString() + " 10^" + bh.Solar_Mass_Exp.ToString() + " sm",
                                      Galaxy = g.Galaxy_Name,
                                      DiscoverMission = dm.Mission_D_Name
                                  };
                        DataGrid.ItemsSource = res;
                    }
                    else
                    {
                        var res = from bh in db.BLACK_HOLEs
                                  join g in db.GALAXies on bh.ID_Galaxy equals g.ID_Galaxy
                                  join dm in db.DISCOVERs on bh.ID_Mission_D equals dm.ID_Mission_D into dmJbh
                                  from dm in dmJbh.DefaultIfEmpty()
                                  where bh.Distance_From_Earth_Exp >= this.minexp && bh.Distance_From_Earth_Exp <= this.maxexp &&
                                   bh.Radius >= this.minradius && bh.Radius <= this.maxradius
                                  select new
                                  {
                                      Name = bh.Black_Hole_Name,
                                      DistanceFromEarth = bh.Distance_From_Earth_Mantissa.ToString() + " x 10^" + bh.Distance_From_Earth_Exp.ToString() + " ly",
                                      DimensionRadius = bh.Radius.ToString() + " km",
                                      Mass = bh.Solar_Mass_Mantissa.ToString() + " 10^" + bh.Solar_Mass_Exp.ToString() + " sm",
                                      Galaxy = g.Galaxy_Name,
                                      DiscoverMission = dm.Mission_D_Name
                                  };
                        DataGrid.ItemsSource = res;
                    }
                }else if (TypeComboBox.Text.Equals("Star"))
                {
                    if(NameText.Text != "")
                    {
                        var res = from s in db.STARs
                                  join g in db.GALAXies on s.ID_Galaxy equals g.ID_Galaxy
                                  join c in db.CONSTELLATIONs on s.ID_Constellation equals c.ID_Constellation into cJs
                                  from c in cJs.DefaultIfEmpty()
                                  join dm in db.DISCOVERs on s.ID_Mission_D equals dm.ID_Mission_D into dmJs
                                  from dm in dmJs.DefaultIfEmpty()
                                  where s.Distance_From_Earth_Exp >= this.minexp && s.Distance_From_Earth_Exp <= this.maxexp &&
                                   s.Radius >= this.minradius && s.Radius <= this.maxradius
                                   && s.Star_Name.Equals(NameText)
                                  select new
                                  {
                                      Name = s.Star_Name,
                                      DistanceFromEarth = s.Distance_From_Earth_Mantissa.ToString() + " x 10^" + s.Distance_From_Earth_Exp.ToString() + " ly",
                                      DimensionRadius = s.Radius.ToString() + " km",
                                      Mass = s.Solar_Mass_Mantissa.ToString() + " 10^" + s.Solar_Mass_Exp.ToString() + " sm",
                                      Galaxy = g.Galaxy_Name,
                                      Constellation = c.Constellation_Name,
                                      DiscoverMission = dm.Mission_D_Name
                                  };
                        DataGrid.ItemsSource = res;
                    }
                    else
                    {
                        var res = from s in db.STARs
                                  join g in db.GALAXies on s.ID_Galaxy equals g.ID_Galaxy
                                  join c in db.CONSTELLATIONs on s.ID_Constellation equals c.ID_Constellation into cJs
                                  from c in cJs.DefaultIfEmpty()
                                  join dm in db.DISCOVERs on s.ID_Mission_D equals dm.ID_Mission_D into dmJs
                                  from dm in dmJs.DefaultIfEmpty()
                                  where s.Distance_From_Earth_Exp >= this.minexp && s.Distance_From_Earth_Exp <= this.maxexp &&
                                   s.Radius >= this.minradius && s.Radius <= this.maxradius
                                  select new
                                  {
                                      Name = s.Star_Name,
                                      DistanceFromEarth = s.Distance_From_Earth_Mantissa.ToString() + " x 10^" + s.Distance_From_Earth_Exp.ToString() + " ly",
                                      DimensionRadius = s.Radius.ToString() + " km",
                                      Mass = s.Solar_Mass_Mantissa.ToString() + " 10^" + s.Solar_Mass_Exp.ToString() + " sm",
                                      Galaxy = g.Galaxy_Name,
                                      Constellation = c.Constellation_Name,
                                      DiscoverMission = dm.Mission_D_Name
                                  };
                        DataGrid.ItemsSource = res;
                    }
                }
            }
            clean();
        }

        private bool oneIsEmpty(string s1, string s2)
        {
            return (s1 != "" && s2 == "") || (s2 != "" && s1 == "");
        }

       

        private void MinDistExpText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsDigitsOnly(MinDistExpText.Text) )
            {
                SearchButton.IsEnabled = false;
            }
            else
            {
                SearchButton.IsEnabled = true;
            }
        }

        private void MaxDistExpText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsDigitsOnly(MaxDistExpText.Text) )
            {
                SearchButton.IsEnabled = false;
            }
            else
            {
                SearchButton.IsEnabled = true;
            }
        }

        private void TypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            clean();
        }

        private void MinRadiusText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsDigitsOnly(MinRadiusText.Text) )
            {
                SearchButton.IsEnabled = false;
            }
            else
            {
                SearchButton.IsEnabled = true;
            }
        }

        private void MaxRadiusText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsDigitsOnly(MaxRadiusText.Text))
            {
                SearchButton.IsEnabled = false;
            }
            else
            {
                SearchButton.IsEnabled = true;
            }
        }

        private void clean()
        {
            MaxDistExpText.Clear();
            MaxRadiusText.Clear();
            MinRadiusText.Clear();
            NameText.Clear();
            MinDistExpText.Clear();
            
        }

       

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if ((c < '0' || c > '9' ) && c!='.')
                    return false;
            }

            return true;
        }
    }
}
