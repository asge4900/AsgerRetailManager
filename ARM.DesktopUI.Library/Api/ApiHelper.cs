using ARM.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM.DesktopUI.Library.Api
{
    public class ApiHelper : IApiHelper
    {
        /// <summary>
        /// Pipeline for interacting with back end service or database.
        /// </summary>
        public IARMReposity Repository { get; private set; }

        public ApiHelper()
        {
            UseRest();
        }

        public void UseRest() => Repository = new ARMReposity(ConfigurationManager.AppSettings["api"]);
    }
}
