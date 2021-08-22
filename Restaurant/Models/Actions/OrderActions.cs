using Restaurant.Helpers;
using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Restaurant.Models.Actions
{
    class OrderActions : BaseVM
    {
        RestaurantDbEntities ctx = new RestaurantDbEntities();
        private OrderVM orderCtx;

        public OrderActions(OrderVM orderCtx)
        {
            this.orderCtx = orderCtx;
        }
        public void AddMethod(object obj)
        {
            OrderVM orderVM = obj as OrderVM;
            if (orderVM is null) {
                MessageBox.Show("The order couldn't be added!");
                return;
            }
            ctx.Orders.Add(new Order()
            {
                idItem=orderVM.IdItem,
                idTicket=orderVM.IdTicket
            });
            ctx.SaveChanges();
        }
        public void DeleteMethod(object obj)
        {
            OrderVM orderVM = obj as OrderVM;
            if (orderVM is null) {
                MessageBox.Show("No order selected!");
                return;
            }
            var order = ctx.Orders.Where(p => p.id == orderVM.Id).FirstOrDefault();
            if (order is null) {
                MessageBox.Show("The order couldn't be found!");
                return;
            }
            ctx.Orders.Remove(order);
            ctx.SaveChanges();
        }
        public ObservableCollection<OrderVM> AllOrders()
        {
            List<Order> orders = ctx.Orders.ToList();
            ObservableCollection<OrderVM> result = new ObservableCollection<OrderVM>();
            foreach (Order order in orders)
            {
                result.Add(new OrderVM()
                {
                    Id = order.id,
                    IdItem=order.idItem,
                    IdTicket=order.idTicket
                });
            }
            return result;
        }
        public ObservableCollection<OrderVM> GetOrdersForTicket(int idTicket)
        {
            List<Order> orders = ctx.Orders.Where(predicate: p => p.idTicket == idTicket)
                .ToList();
            ObservableCollection<OrderVM> result = new ObservableCollection<OrderVM>();
            foreach (var order in orders)
            {
                result.Add(new OrderVM()
                {
                    Id = order.id,
                    IdItem = order.idItem,
                    IdTicket = order.idTicket
                });
            }
            return result;
        } 
    }
}
