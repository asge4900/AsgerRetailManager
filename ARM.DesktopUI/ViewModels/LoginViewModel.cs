using ARM.DesktopUI.EventModels;
using ARM.DesktopUI.Library.Api;
using Caliburn.Micro;
using System.Threading.Tasks;
using System.Threading;

namespace ARM.DesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        #region Fields
        private string userName = "A";

        private string password = "A";

        private IApiHelper apiHelper;

        private IEventAggregator @event;
        #endregion

        #region Constructor
        public LoginViewModel(IApiHelper apiHelper, IEventAggregator @event)
        {
            this.apiHelper = apiHelper;
            this.@event = @event;
        } 
        #endregion

        public string UserName 
        { 
            get => userName;
            set 
            {
                userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }
        public string Password 
        { 
            get => password;
            set 
            { 
                password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool CanLogIn
        {
            get
	        {
		        bool output = false;

                    if (UserName?.Length > 0 && Password?.Length > 0)
                    {
                        output = true;
                    }

                    return output; 
	        }
        }

        public async Task LogIn()
        {
            //var result = await apiHelper.Repository.Login.AuthenticateAsync(UserName, Password);

            await @event.PublishOnUIThreadAsync(new LogOnEvent(), new CancellationToken());
        }
    }
}
