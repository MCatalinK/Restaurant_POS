using Restaurant.Helpers;
using System.Collections.ObjectModel;

namespace Restaurant.ViewModels
{
    public class TicketTypeVM:BaseVM
    {
        private int id;
        private string type;
        private ObservableCollection<TicketTypeVM> ticketTypes;

        public int Id
        {
            get { return id; }
            set { id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                NotifyPropertyChanged("Type");
            }
        }
        public ObservableCollection<TicketTypeVM> TicketTypes
        {
            get { return ticketTypes; }
            set
            {
                ticketTypes = value;
                NotifyPropertyChanged("TicketTypes");
            }
        }
    }
}
