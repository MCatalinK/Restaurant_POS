using Restaurant.Helpers;
using Restaurant.Views.AdminViews.ReportViews;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    class AdminReportVM:BaseVM
    {
        private ICommand openWindowCommand;
        public ICommand OpenWindowCommand
        {
            get
            {
                if (openWindowCommand == null)
                {
                    openWindowCommand = new RelayCommand(OpenWindow);
                }
                return openWindowCommand;
            }
        }
        public void OpenWindow(object obj)
        {
            string nr = obj as string;
            switch (nr)
            {
                case "1":
                    DailyReportView dailyReport = new DailyReportView();
                    dailyReport.ShowDialog();
                    break;
                case "2":
                    MonthlyReportView monthlyReport = new MonthlyReportView();
                    monthlyReport.ShowDialog();
                    break;
                case "3":
                    ItemReportView itemReport = new ItemReportView();
                    itemReport.ShowDialog();
                    break;
            }
        }
    }
}
