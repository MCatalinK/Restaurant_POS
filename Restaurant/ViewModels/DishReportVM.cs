using Restaurant.Helpers;
using Restaurant.Models.Actions;
using System.Collections.ObjectModel;

namespace Restaurant.ViewModels
{
    class DishReportVM:BaseVM
    {
        MenuItemActions menuAct;
        public DishReportVM()
        {
            menuAct = new MenuItemActions(null);
        }

        private int itemId;
        private MenuItemVM item;
        private int noOfItemsSold;
        private ObservableCollection<DishReportVM> dishReport;

        public int ItemId
        {
            get { return itemId; }
            set
            {
                itemId = value;
                NotifyPropertyChanged("ItemId");
            }
        }
        public MenuItemVM Item
        {
            get
            {
                item = menuAct.GetItemById(ItemId);
                return item;
            }
            set
            {
                item = value;
                NotifyPropertyChanged("Item");
            }
        }
        public int NoOfItemsSold
        {
            get { return noOfItemsSold; }
            set
            {
                noOfItemsSold = value;
                NotifyPropertyChanged("NoOfItemsSold");
            }
        }
        public ObservableCollection<DishReportVM> DishReport
        {
            get {
                dishReport = menuAct.GetTopDishes(); 
                return dishReport; }
            set
            {
                dishReport = value;
                NotifyPropertyChanged("DishReport");
            }
        }
    }
}
