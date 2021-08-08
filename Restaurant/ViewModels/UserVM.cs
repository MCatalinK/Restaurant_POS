using Restaurant.Helpers;
using Restaurant.Models.Actions;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Restaurant.ViewModels
{
    public class UserVM : BaseVM
    {
        UserActions userAct;
        UserRoleActions roleAct;

        public UserVM()
        {
            userAct = new UserActions(this);
            roleAct = new UserRoleActions(null);
        }

        #region Data Memebers
        private int id;
        private string name;
        private string username;
        private string password;
        private int idRole;
        private ObservableCollection<UserVM> users;
        private ObservableCollection<UserRoleVM> roles;
        private UserRoleVM role;

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
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                NotifyPropertyChanged("Username");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged("Password");
            }
        }
        public int IdRole
        {
            get
            {
                return idRole;
            }
            set
            {
                idRole = value;
                NotifyPropertyChanged("IdRole");
            }
        }
       
        public ObservableCollection<UserVM> Users
        {
            get { users= userAct.AllUsers();
                return users;
            }
            set
            {
                users = value;
                NotifyPropertyChanged("Users");
            }
        }
        public ObservableCollection<UserRoleVM> Roles
        {
            get
            {
                roles = roleAct.AllRoles();
                return roles;
            }
            set
            {
                roles = value;
                NotifyPropertyChanged("Roles");
            }
        }
        public UserRoleVM Role
        {
            get {
                role = roleAct.GetRole(idRole);
                return role; }
            set
            {
                role = value;
                NotifyPropertyChanged("Role");
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
                    addCommand = new RelayCommand(userAct.AddMethod);
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
                    updateCommand = new RelayCommand(userAct.UpdateMethod);
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
                    deleteCommand = new RelayCommand(userAct.DeleteMethod);
                }
                return deleteCommand;
            }
        }

        public static readonly ICommand CloseCommand =
            new RelayCommand(o => ((Window)o).Close());


        #endregion

        public UserVM GetUser(UserVM user)
        {
            return userAct.GetUser(user);
        }
        public void GetRole(int id)
        {
            idRole = id;
            Role = roleAct.GetRole(id);
        }
        
    }
}
