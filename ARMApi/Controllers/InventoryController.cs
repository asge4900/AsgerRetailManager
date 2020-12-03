using System.Collections.Generic;
using ARM.DataManager.Library.DataAccess;
using ARM.DataManager.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ARMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryData inventoryData;

        public InventoryController(IInventoryData inventoryData)
        {            
            this.inventoryData = inventoryData;
        }

        //[Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public List<InventoryModel> Get()
        {
            return inventoryData.GetInventories();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post(InventoryModel item)
        {
            inventoryData.SaveInventoryRecord(item);
        }
    }
}