using Restaurant.Helpers;
using Restaurant.Views.AdminViews;
using Restaurant.Views.AdminViews.ReportViews;
using System.Windows;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class AdminMainVM:BaseVM
    {
        private UserVM user;
        public UserVM User
        {
            get { return user; }
            set
            {
                user = value;
                NotifyPropertyChanged("User");
            }
        }
        
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

        public static readonly ICommand CloseCommand =
            new RelayCommand(o => ((Window)o).Close());

        public void OpenWindow(object obj)
        {
            string nr = obj as string;
            switch (nr)
            {
                case "1":
                    UserView userView = new UserView();
                    userView.ShowDialog();
                    break;
                case "2":
                    TableView tableView = new TableView();
                    tableView.ShowDialog();
                    break;
                case "3":
                    ItemView itemView = new ItemView();
                    itemView.ShowDialog();
                    break;
                case "4":
                    MainReportView reportView = new MainReportView();
                    reportView.ShowDialog();
                    break;
            }
        }
    }
}
