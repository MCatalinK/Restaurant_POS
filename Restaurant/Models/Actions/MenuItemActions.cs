using Restaurant.Helpers;
using Restaurant.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Restaurant.Models.Actions
{
    class MenuItemActions : BaseVM
    {
        RestaurantDbEntities ctx = new RestaurantDbEntities();
        private MenuItemVM menuCtx;

        public MenuItemActions(MenuItemVM menuCtx)
        {
            this.menuCtx = menuCtx;
        }

        public void AddMethod(object obj)
        {
            MenuItemVM menuVM = obj as MenuItemVM;
            if (menuVM is null) {
                MessageBox.Show("Couldn't find the item!");
                return;
            }
            ctx.MenuItems.Add(new MenuItem()
            {
                name = menuVM.Name,
                price=menuVM.Price,
                isAvailable=menuVM.IsAvailable
            });
            ctx.SaveChanges();
            menuCtx.MenuItems = AllItems();
        }
        public void UpdateMethod(object obj)
        {
            MenuItemVM menuVM = obj as MenuItemVM;
            if (menuVM is null) {
                MessageBox.Show("You need to select an item!");
                return;
            }
            var menu = ctx.MenuItems.Where(p => p.id == menuVM.Id).FirstOrDefault();
            if (menu is null) {
                MessageBox.Show("No item was found!");
                return;
            }
            menu.name = menuVM.Name;
            menu.price = menuVM.Price;
            menu.isAvailable = menuVM.IsAvailable;
            ctx.SaveChanges();
            menuCtx.MenuItems = AllItems();
        }
        public void DeleteMethod(object obj)
        {

            MenuItemVM menuVM = obj as MenuItemVM;
            if (menuVM is null) {
                MessageBox.Show("No item selected!");
                return;
            }
            var menu = ctx.MenuItems.Where(p => p.id == menuVM.Id).FirstOrDefault();
            if (menu is null) {
                MessageBox.Show("No item found!");
                return;
            }
            ctx.MenuItems.Remove(menu);
            ctx.SaveChanges();
            menuCtx.MenuItems = AllItems();
        }
        public ObservableCollection<MenuItemVM> AllItems()
        {
            List<MenuItem> items = ctx.MenuItems.ToList();
            ObservableCollection<MenuItemVM> result = new ObservableCollection<MenuItemVM>();
            foreach (MenuItem item in items)
            {
                result.Add(new MenuItemVM()
                {
                    Id=item.id,
                    Name = item.name,
                    Price = item.price,
                    IsAvailable=item.isAvailable
                });
            }
            return result;
        }
        public MenuItemVM GetItemById(int id)
        {
            var item = ctx.MenuItems.Where(predicate: p => p.id == id).FirstOrDefault();
            if (item is null)
            {
                MessageBox.Show("This item doesn't exist!");
                return null;
            }
            return new MenuItemVM()
            {
                Id = item.id,
                Name = item.name,
                Price = item.price,
                IsAvailable = item.isAvailable
            };
        }
        public ObservableCollection<DishReportVM> GetTopDishes()
        {
            ObservableCollection<DishReportVM> result = new ObservableCollection<DishReportVM>();
          
            var dishes = (from row in ctx.Orders.AsEnumerable()
                             group row by new { row.idItem }
                                   into g
                             select new
                             {
                                 Id = g.Key.idItem,
                                 Count = g.Count()
                             }).OrderByDescending(p=>p.Count);
            for (int i = 0; i < 10; i++)
            {
                if (dishes.ElementAtOrDefault(i) is null)
                    break;
                result.Add(new DishReportVM()
                {
                    ItemId = dishes.ElementAtOrDefault(i).Id,
                    NoOfItemsSold = dishes.ElementAtOrDefault(i).Count
                });
            }

            return result;
        }
        public ObservableCollection<MenuItemVM> AllItemsFromATicket(int idTicket)
        {
            ObservableCollection<MenuItemVM> result = new ObservableCollection<MenuItemVM>();
            var orders = ctx.Orders.Where(p => p.idTicket == idTicket)
                .ToList();
            foreach(var order in orders)
            {
                var item = ctx.MenuItems.Where(p => p.id == order.idItem).First();
                result.Add(new MenuItemVM()
                {
                    Id = item.id,
                    Name = item.name,
                    Price = item.price,
                    IsAvailable = item.isAvailable
                });
            }
            return result;
        }
    }
}
