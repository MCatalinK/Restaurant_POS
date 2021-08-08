using Restaurant.Helpers;
using Restaurant.Models.Actions;
using Restaurant.Views.WaiterViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class WaiterTableVM : BaseVM
    {
        TableActions tableAct;
        TicketActions ticketAct;
        public WaiterTableVM()
        {
            tableAct = new TableActions(this);
            ticketAct = new TicketActions(null);
        }

        #region Data Members

        private UserVM user;
        private ObservableCollection<TableVM> tables;

        public UserVM User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                NotifyPropertyChanged("User");
            }
        }
       
        public ObservableCollection<TableVM> Tables
        {
            get
            {
                tables = tableAct.AllTablesForWaiter(User.Id);
                return tables;
            }
            set
            {
                tables = value;
                NotifyPropertyChanged("Tables");
            }
        }
       
        #endregion

        #region Command Members

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand(tableAct.UpdateMethod);
                }
                return updateCommand;
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

        #endregion

        public void OpenWindow(object obj)
        {
            TableVM table = obj as TableVM;
            if (table.NoOfSeatsOccupied == 0)
            {
                MessageBox.Show("There aren't any people at the table!");
                return;
            }
            var ticket = ticketAct.GetLastTicketForTable(table.Id);
            if (ticket is null && ticket?.IdType != 2)
            {
                ticketAct.AddMethod(User.Id, table.Id);
                ticket = ticketAct.GetLastTicketForTable(table.Id);
            }

            TicketView ticketView = new TicketView(new TicketVM() {Id=ticket.Id,
                TotalPrice=ticket.TotalPrice,
                EmissionDate=ticket.EmissionDate,
                IdUser=User.Id,
                IdTable=table.Id });
            ticketView.Show();
        }
    }
}
