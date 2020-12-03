using ARM.DataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM.DataManager.Library.DataAccess
{
    public class InventoryData : IInventoryData
    {       
        private readonly ISqlDataAccess sql;

        public InventoryData(ISqlDataAccess sql)
        {
            this.sql = sql;
        }
        public List<InventoryModel> GetInventories()
        {
            var ouput = sql.LoadData<InventoryModel, dynamic>("GetAllInventories", new { }, Constants.ARMDATA);

            return ouput;
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            sql.SaveData("CreateInventory", item, Constants.ARMDATA);
        }
    }
}
