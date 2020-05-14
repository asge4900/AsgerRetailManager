using ARM.DataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM.DataManager.Library.DataAccess
{
    public class InventoryData
    {
        public List<InventoryModel> GetInventories()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var ouput = sql.LoadData<InventoryModel, dynamic>("GetAllInventories", new { }, Constants.ARMDATA);

            return ouput;
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData("CreateInventory", item, Constants.ARMDATA);
        }
    }
}
