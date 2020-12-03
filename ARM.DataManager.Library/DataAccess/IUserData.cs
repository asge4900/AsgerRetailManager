using ARM.Entities.ViewModels;
using System.Collections.Generic;

namespace ARM.DataManager.Library.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string Id);
    }
}