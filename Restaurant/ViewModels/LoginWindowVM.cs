using Restaurant.Helpers;
using Restaurant.Models.Actions;
using Restaurant.Views.AdminViews;
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
    class LoginWindowVM
    {
        public LoginWindowVM()
        {
            UserRoleActions userAct=new UserRoleActions(null);
            TicketTypeActions ticketAct = new TicketTypeActions(null);

            userAct.SetRoles();
            ticketAct.SetTypes();

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

            UserVM user = obj as UserVM;
            user = user.GetUser(user);
            if (user is null) return;
            switch (user.IdRole)
            {
                case 1:
                    Views.AdminViews.MainView adminView = new Views.AdminViews.MainView(new AdminMainVM() { User = user });
                    adminView.ShowDialog();
                    break;
                case 2:
                    Views.WaiterViews.MainView waiterView = new Views.WaiterViews.MainView(new WaiterMainVM() { User = user });
                    waiterView.ShowDialog();
                    break;
            }
        }
    }
}
