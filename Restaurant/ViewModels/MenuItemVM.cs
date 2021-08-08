using Restaurant.Helpers;
using Restaurant.Models.Actions;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class MenuItemVM:BaseVM
    {
        MenuItemActions menuAct;
 
        public MenuItemVM()
        {
            menuAct = new MenuItemActions(this);
        }

        #region Data Members
        private int id;
        private string name;
        private decimal price;
        private bool? isAvailable;
        private ObservableCollection<MenuItemVM> menuItems;

        public int Id
        {
            get { return id; }
            set { id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                NotifyPropertyChanged("Price");
            }
        }
        public bool? IsAvailable
        {
            get { return isAvailable; }
            set
            {
                isAvailable = value;
                NotifyPropertyChanged("IsAvailable");
            }
        }
        public ObservableCollection<MenuItemVM> MenuItems
        {
            get {
                menuItems = menuAct.AllItems();
                return menuItems; }
            set
            {
                menuItems = value;
                NotifyPropertyChanged("MenuItems");
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
                    addCommand = new RelayCommand(menuAct.AddMethod);
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
                    updateCommand = new RelayCommand(menuAct.UpdateMethod);
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
                    deleteCommand = new RelayCommand(menuAct.DeleteMethod);
                }
                return deleteCommand;
            }
        }
        public static readonly ICommand CloseCommand =
           new RelayCommand(o => ((Window)o).Close());

        #endregion
    }
}
