using Restaurant.Helpers;
using Restaurant.Models.Actions;
using Restaurant.Views.WaiterViews;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class TicketVM:BaseVM
    {
        TicketActions ticketAct;
        UserActions userAct;
        TableActions tableAct;
        TicketTypeActions typeAct;
        MenuItemActions menuAct;
        OrderActions orderAct;

        public TicketVM()
        {
            ticketAct = new TicketActions(this);
            userAct = new UserActions(null);
            tableAct = new TableActions();
            typeAct = new TicketTypeActions(null);
            menuAct = new MenuItemActions(null);
            orderAct = new OrderActions(null);
        }

        #region Data Members
        private int id;
        private Decimal totalPrice;
        private DateTime emissionDate;
        private int idUser;
        private int idTable;
        private int idType;
        private ObservableCollection<TicketTypeVM> types;
        private ObservableCollection<MenuItemVM> allItems;
        private ObservableCollection<MenuItemVM> dishes;
        private TableVM table;
        private TicketTypeVM type;
        private UserVM user;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public Decimal TotalPrice
        {
            get {
                totalPrice = ticketAct.GetTotalPrice(id); 
                return totalPrice; }
            set
            {
                totalPrice = value;
                NotifyPropertyChanged("TotalPrice");
            }
        }
        public DateTime EmissionDate
        {
            get { return emissionDate; }
            set
            {
                emissionDate = value;
                NotifyPropertyChanged("EmissionDate");
            }
        }
        public int IdUser
        {
            get { return idUser; }
            set
            {
                idUser = value;
                NotifyPropertyChanged("IdUser");
            }
        }
        public int IdTable
        {
            get { return idTable; }
            set
            {
                idTable = value;
                NotifyPropertyChanged("IdTable");
            }
        }
        public int IdType
        {
            get
            {
                return idType;
            }
            set
            {
                idType = value;
                NotifyPropertyChanged("IdType");
            }
        }
        public TableVM Table
        {
            get
            {
                table = tableAct.TableById(idTable);
                return table;
            }
            set
            {
                table = value;
                NotifyPropertyChanged("Table");
            }
        }
        public TicketTypeVM Type
        {
            get {
                type = typeAct.GetType(idType);
                return type; }
            set
            {
                type = value;
                NotifyPropertyChanged("Type");
            }
        }
        public UserVM User
        {
            get
            {
                user = userAct.GetUser(idUser);
                return user;
            }
            set
            {
                user = value;
                NotifyPropertyChanged("User");
            }
        }
        public ObservableCollection<TicketTypeVM> Types
        {
            get {
                types = typeAct.AllTypes();
                return types; }
            set
            {
                types = value;
                NotifyPropertyChanged("Types");
            }
        }
        public ObservableCollection<MenuItemVM> Dishes
        {
            get {
                dishes = menuAct.AllItemsFromATicket(Id);
                return dishes; }
            set
            {
                dishes = value;
                NotifyPropertyChanged("Dishes");
            }
        }
        public ObservableCollection<MenuItemVM> AllItems
        {
            get
            {
                allItems = menuAct.AllItems();
                return allItems;
            }
            set
            {
                allItems = value;
                NotifyPropertyChanged("AllItems");
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
                    addCommand = new RelayCommand(orderAct.AddMethod);
                }
                return addCommand;
            }
        }

        private ICommand payCommand;
        public ICommand PayCommand
        {
            get
            {
                if (payCommand == null)
                {
                    payCommand = new RelayCommand(ticketAct.PayMethod);
                }
                return payCommand;
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(orderAct.DeleteMethod);
                }
                return deleteCommand;
            }
        }
       #endregion
    }
}
