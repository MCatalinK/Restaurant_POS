using Restaurant.Helpers;
using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Restaurant.Models.Actions
{
    class TableActions : BaseVM
    {
        RestaurantDbEntities ctx = new RestaurantDbEntities();
        private TableVM tableCtx;
        private WaiterTableVM waiterTableCtx;

        public TableActions(TableVM tableCtx)
        {
            this.tableCtx = tableCtx;
        }
        public TableActions(WaiterTableVM tableCtx)
        {
            this.waiterTableCtx = tableCtx;
        }
        public TableActions() { }
        public void AddMethod(object obj)
        {
            TableVM tableVM = obj as TableVM;
            if (tableVM is null) MessageBox.Show("The table couldn't be found!");
            if (tableVM.UserId is 0)
                tableVM.UserId = null;
            ctx.Tables.Add(new Table()
            {
               noOfSeats=tableVM.NoOfSeats,
               noOfSeatsOccupied=tableVM.NoOfSeatsOccupied,
               userId=tableVM.UserId     
            });
            ctx.SaveChanges();
            if(tableCtx!=null)
                tableCtx.Tables = AllTables();
        }
        public void UpdateMethod(object obj)
        {
            TableVM tableVM = obj as TableVM;
            if (tableVM is null) MessageBox.Show("No table selected!");
            var table = ctx.Tables.Where(p => p.id == tableVM.Id).FirstOrDefault();
            if (table is null) MessageBox.Show("The table couldn't be found!");
            table.noOfSeats = tableVM.NoOfSeats;
            table.noOfSeatsOccupied = tableVM.NoOfSeatsOccupied;
            table.userId = tableVM.UserId;
            ctx.SaveChanges();
            if (tableCtx != null)
                tableCtx.Tables = AllTables();
            else
                waiterTableCtx.Tables = AllTablesForWaiter((int)tableVM.UserId);
        }
        public void DeleteMethod(object obj)
        {

            TableVM tableVM = obj as TableVM;
            if (tableVM is null) MessageBox.Show("No table selected!");
            var table = ctx.Tables.Where(p => p.id == tableVM.Id).FirstOrDefault();
            if (table is null) MessageBox.Show("The table couldn't be found!");
            ctx.Tables.Remove(table);
            ctx.SaveChanges();
            if (tableCtx != null)
                tableCtx.Tables = AllTables();
        }
        public ObservableCollection<TableVM> AllTables()
        {
            List<Table> tables = ctx.Tables.ToList();
            ObservableCollection<TableVM> result = new ObservableCollection<TableVM>();
            foreach (Table table in tables)
            {
                result.Add(new TableVM()
                {
                    Id=table.id,
                    NoOfSeats=table.noOfSeats,
                    NoOfSeatsOccupied= (int)table.noOfSeatsOccupied,
                    UserId=table.userId
                });
            }
            return result;
        }
        public ObservableCollection<TableVM> AllTablesForWaiter(int id)
        {
            List<Table> tables = ctx.Tables.Where(p=>p.userId==id).ToList();
            ObservableCollection<TableVM> result = new ObservableCollection<TableVM>();
            foreach (Table table in tables)
            {
                result.Add(new TableVM()
                {
                    Id = table.id,
                    NoOfSeats = table.noOfSeats,
                    NoOfSeatsOccupied = (int)table.noOfSeatsOccupied,
                    UserId = table.userId
                });
            }
            return result;
        }
        public TableVM TableById(int id)
        {
            var table = ctx.Tables.Where(p => p.id == id).FirstOrDefault();
            if (table is null)
                MessageBox.Show("The table couldn't be found!");

            return new TableVM()
            {
                Id = table.id,
                NoOfSeats = table.noOfSeats,
                NoOfSeatsOccupied = table.noOfSeatsOccupied,
                UserId = table.userId
            };
        }
    }
}
