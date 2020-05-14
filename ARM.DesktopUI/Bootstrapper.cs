using ARM.DesktopUI.Library.Api;
using ARM.DesktopUI.Library.Helpers;
using ARM.DesktopUI.Models;
using ARM.DesktopUI.ViewModels;
using ARM.Entities.ViewModels;
using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ARM.DesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container = new SimpleContainer();       

        public Bootstrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
            "Password",
            "PasswordChanged");
        }

        ///// <summary>
        ///// Pipeline for interacting with back end service or database.
        ///// </summary>
        //public static IARMReposity Repository { get; private set; }

        private IMapper ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductModel, ProductDisplayModel>();
                cfg.CreateMap<CartItemViewModel, CartItemDisplayModel>();
            });

            var output = config.CreateMapper();

            return output;
        }

        protected override void Configure()
        {   
            container.Instance(ConfigureAutoMapper());

            container.Instance(container);

            container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IConfigHelper, ConfigHelper>()
                .Singleton<IApiHelper, ApiHelper>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //UseRest();

            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        //public static void UseRest() => Repository = new ARMReposity(ConfigurationManager.AppSettings["api"]);
    }
}
