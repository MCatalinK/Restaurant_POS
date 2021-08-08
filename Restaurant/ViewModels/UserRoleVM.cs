using Restaurant.Helpers;
using Restaurant.Helpers.Enums;
using Restaurant.Models.Actions;
using System;
using System.Collections.ObjectModel;

namespace Restaurant.ViewModels
{
    public class UserRoleVM:BaseVM
    {

        UserRoleActions rolesAct;

        public UserRoleVM()
        {
            rolesAct = new UserRoleActions(this);
        }
        public void SetRoles()
        {
            rolesAct.SetRoles();
        }

        private int id;
        private string name;
        private ObservableCollection<UserRoleVM> roles;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
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
        public ObservableCollection<UserRoleVM> Roles
        {
            get { return roles; }
            set
            {
                roles = value;
                NotifyPropertyChanged("Roles");
            }
        }
       

    }
}
