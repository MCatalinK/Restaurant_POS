using Restaurant.Helpers;
using Restaurant.Models.Actions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    class DailyReportVM:BaseVM
    {
        UserActions userAct;
        TicketActions ticketAct;
        public DailyReportVM()
        {
            userAct = new UserActions(null);
            ticketAct = new TicketActions(null);
        }

        private UserVM waiter;
        private decimal total;
        ObservableCollection<DailyReportVM> dailyReport;

        public UserVM Waiter
        {
            get { return waiter; }
            set
            {
                waiter = value;
                NotifyPropertyChanged("Waiter");
            }
        }
        public decimal Total
        {
            get { return total; }
            set
            {
                total = value;
                NotifyPropertyChanged("Total");
            }
        }
        public ObservableCollection<DailyReportVM> DailyReport
        {
            get {
                dailyReport = new ObservableCollection<DailyReportVM>();
                var waiters = userAct.GetAllWaiters();
                decimal sum = 0;
                foreach(var waiter in waiters)
                {
                    sum = 0;
                    var ticketList = ticketAct.GetTicketsForWaiterByDay(waiter.Id);
                    foreach (var ticket in ticketList)
                        sum += ticket.TotalPrice;
                    dailyReport.Add(new DailyReportVM()
                    {
                        Waiter = waiter,
                        Total = sum
                    });
                }
                return dailyReport; }
            set
            {
                dailyReport = value;
                NotifyPropertyChanged("DailyReport");
            }
        }
    }
}
