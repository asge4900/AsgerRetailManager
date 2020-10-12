using ARM.DesktopUI.EventModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ARM.DesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator @event;

        public ShellViewModel(IEventAggregator @event)
        {            
            this.@event = @event;

            this.@event.SubscribeOnPublishedThread(this);

            ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
        }

        public bool LoggedIn { get; set; } = false;

        public bool IsLoggedIn
        {
            get
            {
                bool output = false;

                if (LoggedIn)
                {
                    output = true;
                }

                return output;
            }
        }

        public void ExitApplication()
        {
            TryCloseAsync();
        }

        public async Task UserManagement()
        {
            await ActivateItemAsync(IoC.Get<UserDisplayViewModel>(), new CancellationToken());
        }

        public async Task LogOut()
        {
            await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());

            LoggedIn = false;

            NotifyOfPropertyChange(() => IsLoggedIn);
        }        

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<SalesViewModel>(), cancellationToken);

            LoggedIn = true;

            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
