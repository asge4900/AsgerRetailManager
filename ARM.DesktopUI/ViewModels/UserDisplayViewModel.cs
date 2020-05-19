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
    public class UserDisplayViewModel : Screen
    {
        #region Fields

        private IApiHelper apiHelper;       

        private IMapper mapper;

        private StatusInfoViewModel status;

        private IWindowManager window;

        private BindingList<UserModel> users;

        //private int itemQuantity = 1;

        //private ProductDisplayModel selectedProduct;

        //private CartItemDisplayModel selectedCartItem;        

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

        #endregion
    }
}
