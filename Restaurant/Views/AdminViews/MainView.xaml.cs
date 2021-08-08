using Restaurant.ViewModels;
using System.Windows;

namespace Restaurant.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(AdminMainVM adminMainVM)
        {
            InitializeComponent();
            this.DataContext = adminMainVM;
        }
    }
}
