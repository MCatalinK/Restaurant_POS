using Restaurant.Helpers;
using Restaurant.Helpers.Enums;
using Restaurant.Models.Actions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    class MonthlyReportVM:BaseVM
    {
        UserActions userAct;
        TicketActions ticketAct;

        public MonthlyReportVM()
        {
            userAct = new UserActions(null);
            ticketAct = new TicketActions(null);
        }
        private int idUser;
        private UserVM waiter;
        private decimal total;
        ObservableCollection<MonthlyReportVM> monthlyReport;
        ObservableCollection<String> months;

        public int IdUser
        {
            get { return idUser; }
            set
            {
                idUser = value;
                NotifyPropertyChanged("IdUser");
            }
        }
        public UserVM Waiter
        {
            get {
                waiter = userAct.GetUser(IdUser);
                return waiter; }
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
        public ObservableCollection<MonthlyReportVM> MonthlyReport
        {
            get
            {
                return monthlyReport;
            }
            set
            {
                monthlyReport = value;
                NotifyPropertyChanged("MonthlyReport");
            }
        }
        public ObservableCollection<String> Months
        {
            get
            {
                if (months is null)
                    months = new ObservableCollection<string>();
                foreach (var month in Enum.GetNames(typeof(MonthsEnum)))
                    months.Add(month);
                return months;
            }
        }
        public void MonthlyReportForMonth(object obj)
        {
            string month = obj as string;
            ObservableCollection<MonthlyReportVM> result = new ObservableCollection<MonthlyReportVM>();
            var waiters = userAct.GetAllWaiters();
            decimal sum=0;
            foreach (var waiter in waiters)
            {
                sum = 0;
                var tickets = ticketAct.GetTicketsForWaiterByMonth(waiter.Id, month);
                foreach (var ticket in tickets)
                    sum += ticket.TotalPrice;
                result.Add(new MonthlyReportVM()
                {
                    IdUser=waiter.Id,
                    Total = sum
                });
            }
            MonthlyReport = result;
        }
        private ICommand getLastMonths;
        public ICommand GetLastMonths
        {
            get
            {
                if (getLastMonths == null)
                {
                    getLastMonths = new RelayCommand(BestAndWorst);
                }
                return getLastMonths;
            }
        }
        public void BestAndWorst(object obj)
        {
            MonthlyReport = ticketAct.GetTicketsForLastMonths();
        }
    }
}
