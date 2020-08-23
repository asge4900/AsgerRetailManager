using ARM.DataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM.DataManager.Library.DataAccess
{
    public class InventoryData
    {
        private readonly IConfiguration configuration;

        public InventoryData(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public List<InventoryModel> GetInventories()
        {
            SqlDataAccess sql = new SqlDataAccess(configuration);

            var ouput = sql.LoadData<InventoryModel, dynamic>("GetAllInventories", new { }, Constants.ARMDATA);

            return ouput;
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            SqlDataAccess sql = new SqlDataAccess(configuration);

            sql.SaveData("CreateInventory", item, Constants.ARMDATA);
        }
    }
}
