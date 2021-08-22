using Restaurant.Helpers;
using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Restaurant.Models.Actions
{
    
    class TicketActions : BaseVM
    {
        RestaurantDbEntities ctx = new RestaurantDbEntities();
        private TicketVM ticketCtx;

        public TicketActions(TicketVM ticketCtx)
        {
            this.ticketCtx = ticketCtx;
        }
        public void AddMethod(int idUser,int idTable)
        {
            if (idUser == 0 || idTable == 0)
            {
                MessageBox.Show("The user or table couldn't be found!");
                return;
            }
            ctx.Tickets.Add(new Ticket()
            {
               emissionDate=DateTime.Now,
               totalPrice=0,
               idUser=idUser,
               idTable=idTable,
               idType=2
            });
            ctx.SaveChanges();
        }
        public void CancelMethod(object obj)
        {
            TicketVM ticketVM = obj as TicketVM;
            if (ticketVM is null)
            {
                MessageBox.Show("No ticket!");
                return;
            }
            var ticket = ctx.Tickets.Where(p => p.id == ticketVM.Id).FirstOrDefault();
            if (ticket is null)
            {
                MessageBox.Show("No ticket found!");
                return;
            }
            if(ticket.idType!=1)
            {
                MessageBox.Show("You can't cancel this ticket!");
                return;
            }
            ticket.idType = 3;
            ctx.SaveChanges();
        }
        public void PayMethod(object obj)
        {
            int? id = Int32.Parse(obj as string);
            if (id is null)
            {
                MessageBox.Show("No ticket!");
                return;
            }
            var ticket = ctx.Tickets.Where(p => p.id == id).FirstOrDefault();
            if (ticket is null) {
                MessageBox.Show("No ticket found!");
                return;
            }
            if(ticket.idType==1)
            {
                MessageBox.Show("Ticket already payed!");
                return;

            }
            var table = ctx.Tables.Where(p => p.id == ticket.idTable).FirstOrDefault();
            table.noOfSeatsOccupied = 0;
            ticket.idType = 1;
            ticket.emissionDate = DateTime.Now;
            ticket.totalPrice = GetTotalPrice(ticket.id);
            ctx.SaveChanges();
        }
        public ObservableCollection<TicketVM> AllTickets()
        {
            List<Ticket> tickets = ctx.Tickets.ToList();
            ObservableCollection<TicketVM> result = new ObservableCollection<TicketVM>();
            foreach (Ticket ticket in tickets)
            {
                result.Add(new TicketVM()
                {
                    Id = ticket.id,
                    EmissionDate= (DateTime)ticket.emissionDate,
                    TotalPrice= (decimal)ticket.totalPrice,
                    IdUser=ticket.idUser,
                    IdTable=ticket.idTable,
                    IdType=ticket.idType
                });
            }
            return result;
        }
        public ObservableCollection<TicketVM> AllTicketsForWeekDay(string day)
        {
            List<Ticket> tickets = ctx.Tickets.ToList();
            ObservableCollection<TicketVM> result = new ObservableCollection<TicketVM>();
           
            foreach (var ticket in tickets)
            {
                if (ticket.emissionDate.DayOfWeek.ToString()==day)
                    result.Add(new TicketVM()
                    {
                        Id = ticket.id,
                        EmissionDate = ticket.emissionDate,
                        TotalPrice = (decimal)ticket.totalPrice,
                        IdUser = ticket.idUser,
                        IdTable = ticket.idTable,
                        IdType = ticket.idType
                    });
            }
            result.Reverse();
            return result;
        }
        public ObservableCollection<TicketVM> AllTicketsForTable(int idTable)
        {
            List<Ticket> tickets = ctx.Tickets
                .Where(predicate:p=>p.idTable==idTable)
                .ToList();
            ObservableCollection<TicketVM> result = new ObservableCollection<TicketVM>();
           
            foreach (var ticket in tickets)
            {
                result.Add(new TicketVM()
                {
                    Id = ticket.id,
                    EmissionDate = (DateTime)ticket.emissionDate,
                    TotalPrice = (decimal)ticket.totalPrice,
                    IdUser = ticket.idUser,
                    IdTable = ticket.idTable,
                    IdType = ticket.idType
                });
            }
            return result;
        }
        public TicketVM GetLastTicketForTable(int idTable)
        {
            var ticket = ctx.Tickets.Where(predicate: p => p.idTable == idTable)
                .OrderByDescending(p=>p.id)
                .FirstOrDefault();
            if (ticket is null) return null;
            return new TicketVM()
            {
                Id = ticket.id,
                EmissionDate = (DateTime)ticket.emissionDate,
                TotalPrice = (decimal)ticket.totalPrice,
                IdUser = ticket.idUser,
                IdTable = ticket.idTable,
                IdType = ticket.idType
            };
        }
        public TicketVM GetTicketById(int id)
        {
            var ticket = ctx.Tickets.Where(predicate: p => p.id == id).FirstOrDefault();
            if (ticket is null) return null;
            return new TicketVM()
            {
                Id = ticket.id,
                EmissionDate = (DateTime)ticket.emissionDate,
                TotalPrice = (decimal)ticket.totalPrice,
                IdUser = ticket.idUser,
                IdTable = ticket.idTable,
                IdType = ticket.idType
            };
        }
        public decimal GetTotalPrice(int idTicket)
        {
            var listOrders = ctx.Orders.Where(predicate: p => p.idTicket == idTicket)
                .ToList();
            if (listOrders is null) return 0;
            List<MenuItem> items = new List<MenuItem>();
            decimal price = 0;
            foreach (var order in listOrders)
                items.Add(ctx.MenuItems.Where(p => p.id == order.idItem).First());
            foreach (var item in items)
                price += item.price;
            return price;
        }
        public ObservableCollection<TicketVM>GetTicketsForWaiterByDay(int idUser)
        {
            List<Ticket> tickets = ctx.Tickets
               .Where(predicate: p => p.idUser==idUser && p.emissionDate.Day==DateTime.Now.Day && p.idType == 1)
               .ToList();
            ObservableCollection<TicketVM> result = new ObservableCollection<TicketVM>();
            foreach(var ticket in tickets)
            {
                result.Add(new TicketVM()
                {
                    Id=ticket.id,
                    TotalPrice= (decimal)ticket.totalPrice,
                    EmissionDate=ticket.emissionDate,
                    IdTable=ticket.idTable,
                    IdType=ticket.idType,
                    IdUser=ticket.idUser
                });
            }
            return result;
        }
        public ObservableCollection<TicketVM> GetTicketsForWaiterByMonth(int idUser,string month)
        {
            List<Ticket> tickets;
            if (month is "0")
                tickets = ctx.Tickets
                  .Where(predicate: p => p.idUser == idUser && p.emissionDate.Month == DateTime.Now.Month && p.emissionDate.Year == DateTime.Now.Year && p.idType==1)
                  .ToList();
            else
                tickets = ctx.Tickets
                   .Where(predicate: p => p.idUser == idUser && p.emissionDate.Month.ToString() == month && p.emissionDate.Year == DateTime.Now.Year && p.idType == 1)
                   .ToList();

            ObservableCollection<TicketVM> result = new ObservableCollection<TicketVM>();
            foreach (var ticket in tickets)
            {
                result.Add(new TicketVM()
                {
                    Id = ticket.id,
                    TotalPrice = (decimal)ticket.totalPrice,
                    EmissionDate = ticket.emissionDate,
                    IdTable = ticket.idTable,
                    IdType = ticket.idType,
                    IdUser = ticket.idUser
                });
            }
            return result;
        }
        public ObservableCollection<MonthlyReportVM> GetTicketsForLastMonths()
        {
            ObservableCollection<MonthlyReportVM> result = new ObservableCollection<MonthlyReportVM>();
            var date = DateTime.Now.AddMonths(-6);
            var ticketSum = (from row in ctx.Tickets.AsEnumerable()
                           where row.emissionDate >= date && row.emissionDate <= DateTime.Now && row.idType==1
                           group row by new {row.idUser}
                           into g
                           select new
                           {
                               Id = g.Key.idUser,
                               Sum = g.Sum(o => decimal.Parse(o.totalPrice.ToString()))
                           }).OrderBy(p => p.Sum);

            result.Add(new MonthlyReportVM()
            {
                IdUser = ticketSum.FirstOrDefault().Id,
                Total = (decimal)ticketSum.FirstOrDefault().Sum
            });
            if(ticketSum.FirstOrDefault().Id!= ticketSum.LastOrDefault().Id)
                result.Add(new MonthlyReportVM()
                {
                    IdUser = ticketSum.LastOrDefault().Id,
                    Total = (decimal)ticketSum.LastOrDefault().Sum
                });

            return result;
        }
    }
}
