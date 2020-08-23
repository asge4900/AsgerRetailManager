using ARM.Entities.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARM.DataManager.Library.DataAccess
{
    public class UserData
    {
        private readonly IConfiguration configuration;

        public UserData(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess(configuration);

            var p = new { id = Id };

            var output = sql.LoadData<UserModel, dynamic>("UserLookup", p, "ARMData");

            return output;
        }
    }
}
