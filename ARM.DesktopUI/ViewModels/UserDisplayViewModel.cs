using ARM.DesktopUI.Library.Api;
using ARM.Entities.ViewModels;
using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ARM.DesktopUI.ViewModels
{
    // TODO: If not working check https://www.youtube.com/watch?v=qJyTzUozUHY&list=PLLWMQd6PeGY0bEMxObA6dtYXuJOGfxSPx&index=30
    public class UserDisplayViewModel : Screen
    {
        #region Fields

        private IApiHelper apiHelper;       

        private IMapper mapper;

        private StatusInfoViewModel status;

        private IWindowManager window;

        private BindingList<UserModel> users;

        private UserModel selectedUser;

        private string selectedUserName;

        private BindingList<string> userRoles = new BindingList<string>();

        private BindingList<string> availableRoles = new BindingList<string>();

        private string selectedUserRole;

        private string selectedAvailableRoles;

        #endregion

        public UserDisplayViewModel(IApiHelper apiHelper, IMapper mapper, StatusInfoViewModel status, IWindowManager window)
        {
            this.apiHelper = apiHelper;            
            this.mapper = mapper;
            this.status = status;
            this.window = window;
        }       

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    status.UpdateMessage("Unauthorized Access", "You do not have permission to interact with the Sales Form");
                    window.ShowDialog(status, null, settings);
                }
                else
                {
                    status.UpdateMessage("Fatal Exception", ex.Message);
                    window.ShowDialog(status, null, settings);
                }

                TryClose();
            }
        }

        private async Task LoadUsersAsync()
        {
            var userList = await apiHelper.Repository.User.GetUsersAsync();
            Users = new BindingList<UserModel>(userList);
        }

        #region Properties

        public BindingList<UserModel> Users 
        { 
            get => users;
            set
            {
                users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        public UserModel SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                SelectedUserName = value.Email;
                UserRoles = new BindingList<string>(value.Roles.Select(x => x.Value).ToList());
                LoadRoles();
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        public string SelectedUserName
        {
            get => selectedUserName;
            set
            {
                selectedUserName = value;
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }

        public BindingList<string> UserRoles
        {
            get => userRoles;
            set
            {
                userRoles = value;
                NotifyOfPropertyChange(() => UserRoles);
            }
        }

        public BindingList<string> AvailableRoles
        {
            get => availableRoles;
            set
            {
                availableRoles = value;
                NotifyOfPropertyChange(() => AvailableRoles);
            }
        }        

        public string SelectedUserRole
        {
            get => selectedUserRole;
            set
            {
                selectedUserRole = value;
                NotifyOfPropertyChange(() => SelectedUserRole);
            }
        }

        public string SelectedAvailableRoles
        {
            get => selectedAvailableRoles;
            set
            {
                selectedAvailableRoles = value;
                NotifyOfPropertyChange(() => SelectedAvailableRoles);
            }
        }



        #endregion

        private async Task LoadRoles()
        {
            var roles = await apiHelper.Repository.User.GetRolesAsync();

            foreach (var role in roles)
            {
                if (UserRoles.IndexOf(role.Value) < 0)
                {
                    AvailableRoles.Add(role.Value);
                }
            }
        }

        public async Task AddSelectedRole()
        {
            await apiHelper.Repository.User.AddToRole(SelectedUser.Id, SelectedAvailableRoles);

            UserRoles.Add(SelectedAvailableRoles);
            AvailableRoles.Remove(SelectedAvailableRoles);
        }

        public async Task RemoveSelectedRole()
        {
            await apiHelper.Repository.User.RemoveRole(SelectedUser.Id, SelectedUserRole);

            AvailableRoles.Add(SelectedUserRole);
            UserRoles.Remove(SelectedUserRole);
        }
    }
}
