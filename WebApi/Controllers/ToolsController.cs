using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TmyFlower.Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        [HttpGet]
        public void InitDatabase()
        {
            DbContext.InitDataBase();
        }
    }
}
