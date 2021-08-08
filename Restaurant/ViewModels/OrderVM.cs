using Restaurant.Helpers;
using Restaurant.Models.Actions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class OrderVM:BaseVM
    {
        private int id;
        private int idTicket;
        private int idItem;
        private ObservableCollection<OrderVM> orders;
        private ObservableCollection<MenuItemVM> allItems;
        private MenuItemVM item;
        private TicketVM ticket;

        OrderActions orderAct;
        MenuItemActions itemAct;

        public OrderVM()
        {
            orderAct = new OrderActions(this);
            itemAct = new MenuItemActions(null);
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public int IdTicket
        {
            get {
                return idTicket; }
            set { idTicket = value;
                NotifyPropertyChanged("IdTicket");
            }
        }
        public int IdItem
        {
            get { return idItem; }
            set
            {
                idItem = value;
                NotifyPropertyChanged("IdItem");
            }
        }
        public ObservableCollection<OrderVM> Orders
        {
            get {
                orders = orderAct.GetOrdersForTicket(idTicket);
                return orders; }
            set
            {
                orders = value;
                NotifyPropertyChanged("Orders");
            }
        }
        public ObservableCollection<MenuItemVM> AllItems
        {
            get {
                allItems = itemAct.AllItems();
                return allItems; }
            set
            {
                allItems = value;
                NotifyPropertyChanged("AllItems");
            }
        }
        public TicketVM Ticket
        {
            get { return ticket; }
            set
            {
                ticket = value;
                NotifyPropertyChanged("Ticket");
            }
        }
        public MenuItemVM Item
        {
            get {
                item = itemAct.GetItemById(IdItem);
                return item; }
            set
            {
                item = value;
                NotifyPropertyChanged("Item");
            }
        }

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
