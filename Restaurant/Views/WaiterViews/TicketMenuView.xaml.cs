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

namespace Restaurant.Views.WaiterViews
{
    /// <summary>
    /// Interaction logic for TicketMenuView.xaml
    /// </summary>
    public partial class TicketMenuView : Window
    {
        public TicketMenuView(WaiterTicketsVM waiterTicketsVM)
        {
            InitializeComponent();
            this.DataContext = waiterTicketsVM;
        }

        private void cbDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as WaiterTicketsVM).Load(cbDay.SelectedItem);
        }
    }
}
