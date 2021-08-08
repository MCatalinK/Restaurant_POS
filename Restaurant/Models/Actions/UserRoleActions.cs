using Restaurant.Helpers;
using Restaurant.Helpers.Enums;
using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Restaurant.Models.Actions
{
    class UserRoleActions : BaseVM
    {
        RestaurantDbEntities ctx = new RestaurantDbEntities();
        private UserRoleVM rolesCtx;

        public UserRoleActions(UserRoleVM rolesCtx)
        {
            this.rolesCtx = rolesCtx;
        }
        public void SetRoles()
        {
            var rolesList = Enum.GetNames(typeof(RolesEnum)).ToList();
            foreach (var role in rolesList)
                if (ctx.UserRoles.Where(p => p.name.Equals(role)).FirstOrDefault() is null)
                    ctx.UserRoles.Add(new UserRole()
                    {
                        name = role
                    });

            ctx.SaveChanges();
        }
        
        public ObservableCollection<UserRoleVM> AllRoles()
        {
            List<UserRole> roles = ctx.UserRoles.ToList();
            ObservableCollection<UserRoleVM> result = new ObservableCollection<UserRoleVM>();
            foreach (UserRole role in roles)
            {
                result.Add(new UserRoleVM()
                {
                    Id=role.id,
                    Name=role.name
                });
            }
            return result;
        }
        public UserRoleVM GetRole(int id)
        {
            var role = ctx.UserRoles.Where(p => p.id == id).FirstOrDefault();
            if (role is null) throw new Exception("Role doesn't exists!");
            return new UserRoleVM() { Id = role.id, Name = role.name };
        }
    }
}
