using Restaurant.Helpers;
using Restaurant.Helpers.Enums;
using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Restaurant.Models.Actions
{
    class TicketTypeActions : BaseVM
    {
        RestaurantDbEntities ctx = new RestaurantDbEntities();
        private TicketTypeVM typesCtx;

        public TicketTypeActions(TicketTypeVM typesCtx)
        {
            this.typesCtx = typesCtx;
        }
        public void SetTypes()
        {
            var ticketTypes = Enum.GetNames(typeof(TicketTypeEnum)).ToList();
            foreach (var type in ticketTypes)
                if (ctx.TicketTypes.Where(p => p.type.Equals(type)).FirstOrDefault() is null)
                    ctx.TicketTypes.Add(new TicketType()
                    {
                        type = type
                    });

            ctx.SaveChanges();
        }

        public ObservableCollection<TicketTypeVM> AllTypes()
        {
            List<TicketType> types = ctx.TicketTypes.ToList();
            ObservableCollection<TicketTypeVM> result = new ObservableCollection<TicketTypeVM>();
            foreach (var type in types)
            {
                result.Add(new TicketTypeVM()
                {
                     Type=type.type
                });
            }
            return result;
        }
        public TicketTypeVM GetType(int idType)
        {
            var type = ctx.TicketTypes.Where(p => p.id == idType).FirstOrDefault();
            if (type is null) return null;
            return new TicketTypeVM()
            {
                Id = idType,
                Type = type.type
            };
        }
    }
}
