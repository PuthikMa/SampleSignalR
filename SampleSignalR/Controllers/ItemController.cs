using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SampleSignalR.Hubs;

namespace SampleSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        public IHubContext<MessagesHub> Hub { get; }
        public ItemController(IHubContext<MessagesHub> hub)
        {
            Hub = hub;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            try
            {
                var item = " Hi... Item";
                await Hub.Clients.All.SendAsync("NewItem", item);
            }
            catch (Exception ex)
            {

                throw;
            }

            return Ok();
        }

    }
}
