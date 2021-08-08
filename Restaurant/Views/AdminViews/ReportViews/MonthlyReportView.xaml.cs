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

namespace Restaurant.Views.AdminViews.ReportViews
{
    /// <summary>
    /// Interaction logic for MonthlyReportView.xaml
    /// </summary>
    public partial class MonthlyReportView : Window
    {
        public MonthlyReportView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTag = ((ComboBoxItem)cbMonths.SelectedItem).Tag.ToString();
            (DataContext as MonthlyReportVM).MonthlyReportForMonth(selectedTag);
        }
    }
}
