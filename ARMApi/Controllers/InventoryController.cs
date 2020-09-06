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
        private readonly IConfiguration configuration;

        public InventoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //[Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public List<InventoryModel> Get()
        {
            InventoryData data = new InventoryData(configuration);
            return data.GetInventories();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post(InventoryModel item)
        {
            InventoryData data = new InventoryData(configuration);
            data.SaveInventoryRecord(item);
        }
    }
}