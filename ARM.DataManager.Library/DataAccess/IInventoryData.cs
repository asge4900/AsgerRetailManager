using ARM.DataManager.Library.Models;
using System.Collections.Generic;

namespace ARM.DataManager.Library.DataAccess
{
    public interface IInventoryData
    {
        List<InventoryModel> GetInventories();
        void SaveInventoryRecord(InventoryModel item);
    }
}