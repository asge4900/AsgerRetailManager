using System.Collections.Generic;
using ARM.DataManager.Library.DataAccess;
using ARM.DataManager.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace ARMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InventoryController : ControllerBase
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