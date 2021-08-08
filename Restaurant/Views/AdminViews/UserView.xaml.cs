using Restaurant.ViewModels;
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

namespace Restaurant.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public UserView()
        {
            InitializeComponent();
        }

        private void cbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserVM user = DataContext as UserVM;
            //if (user is null || String.IsNullOrEmpty((sender as ComboBox).SelectedValue.ToString()))
            //    return;
            //user.GetRole(int.Parse((sender as ComboBox).SelectedValue.ToString()));
            //cbRole.SelectedItem = user.Role.Name;
        }
    }
}
