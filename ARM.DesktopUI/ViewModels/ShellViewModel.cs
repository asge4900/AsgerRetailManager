using ARM.DesktopUI.EventModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ARM.DesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator @event;
        private SalesViewModel salesVM;

        public ShellViewModel(IEventAggregator @event, SalesViewModel salesVM)
        {            
            this.@event = @event;
            this.salesVM = salesVM;

            this.@event.Subscribe(this);

            ActivateItem(IoC.Get<LoginViewModel>());
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
            TryClose();
        }

        public void LogOut()
        {
            ActivateItem(IoC.Get<LoginViewModel>());

            LoggedIn = false;

            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(salesVM);

            LoggedIn = true;

            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
