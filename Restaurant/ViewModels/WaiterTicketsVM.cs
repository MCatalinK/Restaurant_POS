using Restaurant.Helpers;
using Restaurant.Helpers.Enums;
using Restaurant.Models.Actions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class WaiterTicketsVM:BaseVM
    {
        TicketActions ticketAct;
        TicketTypeActions typeAct;

        public WaiterTicketsVM()
        {
            ticketAct = new TicketActions(null);
            typeAct = new TicketTypeActions(null);
        }
        private TicketTypeVM type;
        private ObservableCollection<String> days;
        private ObservableCollection<TicketVM> tickets;
        private ObservableCollection<TicketTypeVM> types;
        private int idUser;
        private int idType;

        public int IdUser
        {
            get { return idUser; }
            set { idUser = value;
                NotifyPropertyChanged("IdUser");
            }
        }
        public int IdType
        {
            get { return idType; }
            set
            {
                idType = value;
                NotifyPropertyChanged("IdType");
            }
        }
        public TicketTypeVM Type
        {
            get
            {
                type = typeAct.GetType(IdType);
                return type;
            }
            set
            {
                type = value;
                NotifyPropertyChanged("Type");
            }
        }
        public ObservableCollection<String> Days
        {
            get
            {if (days is null)
                    days = new ObservableCollection<string>();
                foreach (var day in Enum.GetNames(typeof(DaysEnum)))
                    days.Add(day);
                return days;
            }
            set
            {
                days = value;
                NotifyPropertyChanged("Days");
            }
        }
        public ObservableCollection<TicketVM> Tickets
        {
            get { return tickets; }
            set { tickets = value;
                NotifyPropertyChanged("Tickets");
            }
        }
        public ObservableCollection<TicketTypeVM> Types
        {
            get
            {
                types = typeAct.AllTypes();
                return types;
            }
            set
            {
                types = value;
                NotifyPropertyChanged("Types");
            }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(ticketAct.CancelMethod);
                }
                return cancelCommand;
            }
        }

        public void Load(object obj)
        {
            string day = obj as string;
            Tickets = ticketAct.AllTicketsForWeekDay(day);
        }
    }
}
