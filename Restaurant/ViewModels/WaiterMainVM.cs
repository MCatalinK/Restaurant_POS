using Restaurant.Helpers;
using Restaurant.Views.WaiterViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class WaiterMainVM : BaseVM
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
                    TicketMenuView ticketMenu = new TicketMenuView(new WaiterTicketsVM() { IdUser = user.Id });
                    ticketMenu.ShowDialog();
                    break;
                case "2":
                    TableView tableView = new TableView(new WaiterTableVM() { User = user});
                    tableView.ShowDialog();
                    break;
                case "3":
                    break;
                   
            }
        }
    }
}
