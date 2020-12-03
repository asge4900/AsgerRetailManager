using ARM.Entities.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARM.DataManager.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess sql;

        public UserData(ISqlDataAccess sql)
        {
            this.sql = sql;
        }

        public List<UserModel> GetUserById(string Id)
        {
            var output = sql.LoadData<UserModel, dynamic>("UserLookup", new { Id }, "ARMData");

            return output;
        }
    }
}
