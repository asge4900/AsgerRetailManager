﻿using ARM.DataManager.Library.DataAccess;
using ARM.DataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ARM.DataManager.Controllers
{
    //[Authorize]
    public class InventoryController : ApiController
    {
        //[Authorize(Roles = "Manager, Admin")]
        public List<InventoryModel> Get()
        {
            InventoryData data = new InventoryData();
            return data.GetInventories();
        }

        //[Authorize(Roles = "Admin")]
        public void Post(InventoryModel item)
        {
            InventoryData data = new InventoryData();
            data.SaveInventoryRecord(item);
        }
    }
}
