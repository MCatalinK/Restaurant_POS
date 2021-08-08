using Restaurant.Helpers;
using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Restaurant.Models.Actions
{
    class UserActions:BaseVM
    {
        RestaurantDbEntities ctx = new RestaurantDbEntities();
        private UserVM userCtx;

        public UserActions(UserVM userCtx)
        {
            this.userCtx = userCtx;
            int nrUsers = ctx.Users.Count();
            if(nrUsers is 0)
            {
                ctx.Users.Add(new User()
                {
                    name = "Admin #1",
                    username = "admin",
                    password = "admin",
                    idRole = 1
                });
                ctx.SaveChanges();
            }
        }
        public void AddMethod(object obj)
        {
            UserVM userVM = obj as UserVM;
            if (userVM is null) { MessageBox.Show("No user found!");
                return;
            }
            var users = ctx.Users.Where(p => p.username == userVM.Username).FirstOrDefault();
            if (users != null)
            {
                MessageBox.Show("This user already exists!");
                return;
            }
                ctx.Users.Add(new User()
            {
                name=userVM.Name,
                username=userVM.Username,
                password=userVM.Password,
                idRole=userVM.IdRole
            });
            ctx.SaveChanges();
            userCtx.Users = AllUsers();
        }
        public void UpdateMethod(object obj)
        {
            UserVM userVM = obj as UserVM;
            if (userVM is null)
            {
                MessageBox.Show("No user found!");
                return;
            }
            var user = ctx.Users.Where(p => p.id == userVM.Id).FirstOrDefault();
            if (user is null)
            {
                MessageBox.Show("No user found!");
                return;
            }
            user.name = userVM.Name;
            user.username = userVM.Username;
            user.password = userVM.Password;
            user.idRole = userVM.IdRole;
            ctx.SaveChanges();
            userCtx.Users = AllUsers();
        }
        public void DeleteMethod(object obj)
        {

            UserVM userVM = obj as UserVM;
            if (userVM is null)
            {
                MessageBox.Show("No user found!");
                return;
            }
            var user = ctx.Users.Where(p => p.id == userVM.Id).FirstOrDefault();
            if (user is null)
            {
                MessageBox.Show("No user found!");
                return;
            }
            ctx.Users.Remove(user);
            ctx.SaveChanges();
            userCtx.Users = AllUsers();
        }
        public ObservableCollection<UserVM> AllUsers()
        {
            List<User> users = ctx.Users.ToList();
            ObservableCollection<UserVM> result = new ObservableCollection<UserVM>();
            foreach (User user in users)
            {
                result.Add(new UserVM()
                {
                    Id=user.id,
                    Name = user.name,
                    Username = user.username,
                    Password = user.password,
                    IdRole=user.idRole
                });
            }
            return result;
        }
        public UserVM GetUser(UserVM fake)
        {
            var user = ctx.Users.Where(p => p.username.Equals(fake.Username) && p.password.Equals(fake.Password))
                .FirstOrDefault();
            if (user is null)
            {
                MessageBox.Show("No user found!");
                return null;
            }
            return new UserVM()
            {
                Id = user.id,
                Name = user.name,
                Username = user.username,
                Password = user.password,
                IdRole = user.idRole
            };
        }
        public UserVM GetUser(int? id)
        {
            if (id is null || id==0) return null;
            var user = ctx.Users.Where(p => p.id == id).FirstOrDefault();
            if (user is null)
            {
                MessageBox.Show("No user found!");
                return null;
            }
            return new UserVM()
            {
                Id = user.id,
                Name = user.name,
                Username = user.username,
                Password = user.password,
                IdRole = user.idRole
            };
        }

        public ObservableCollection<UserVM> GetAllWaiters()
        {
            List<User> users = ctx.Users.Where(predicate: p=>p.idRole==2).ToList();
            ObservableCollection<UserVM> result = new ObservableCollection<UserVM>();
            foreach (User user in users)
            {
                result.Add(new UserVM()
                {
                    Id = user.id,
                    Name = user.name,
                    Username = user.username,
                    Password = user.password,
                    IdRole = user.idRole
                });
            }
            return result;
        }
    }
}
