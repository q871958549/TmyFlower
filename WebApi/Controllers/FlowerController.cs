using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TmyFlower.Model;
using TmyFlower.Service;


namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlowerController : ControllerBase
    {
        public IFlowerService _flowerService;
        public FlowerController(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        [HttpPost]
        public ApiResult GetFlowers(FlowerReq req)
        {
            ApiResult apiResult = new()
            {
                IsSuccess = true,
                Result = _flowerService.GetFlowers(req)
            };
            return apiResult;
        }
    
    }
}
