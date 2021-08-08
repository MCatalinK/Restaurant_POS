using Restaurant.Helpers;
using Restaurant.Models.Actions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class TableVM:BaseVM
    {
        TableActions tableAct;
        UserActions userAct;
        public TableVM()
        {
            tableAct = new TableActions(this);
            userAct = new UserActions(null);
        }

        #region Data Members
        private int id;
        private int noOfSeats;
        private int noOfSeatsOccupied;
        private int? userId;

        private UserVM user;
        private ObservableCollection<TableVM> tables;
        private ObservableCollection<UserVM> waiters;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public int NoOfSeats
        {
            get { return noOfSeats; }
            set
            {
                noOfSeats = value;
                NotifyPropertyChanged("NoOfSeats");
            }
        }
        public int NoOfSeatsOccupied
        {
            get { return noOfSeatsOccupied; }
            set
            {
                noOfSeatsOccupied = value;
                NotifyPropertyChanged("NoOfSeatsOccupied");
            }
        }
        public int? UserId
        {
            get { return userId; }
            set { userId = value;
                NotifyPropertyChanged("UserId");
            }
        }

        public UserVM User
        {
            get { user = userAct.GetUser((int?)userId);
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
            get {
                tables = tableAct.AllTables();
                return tables; }
            set
            {
                tables = value;
                NotifyPropertyChanged("Tables");
            }
        }
        public ObservableCollection<UserVM> Waiters
        {
            get {
                waiters = userAct.GetAllWaiters();
                return waiters; }
            set
            {
                waiters = value;
                NotifyPropertyChanged("Waiters");
            }
        }
        #endregion

        #region Command Members

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(tableAct.AddMethod);
                }
                return addCommand;
            }
        }

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

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(tableAct.DeleteMethod);
                }
                return deleteCommand;
            }
        }

        public static readonly ICommand CloseCommand =
           new RelayCommand(o => ((Window)o).Close());

        #endregion
    }
}
