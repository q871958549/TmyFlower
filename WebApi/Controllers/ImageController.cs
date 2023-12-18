using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        //获取图片列表的方法
        [HttpGet]
        public List<ImageModel> GetImages()
        {
            List<ImageModel> list = new()
            {
                new(){ ImageUrl="/images/banners/21_birthday_banner_pc.jpg",CourseUrl="http://localhost:8080/" },
                new(){ ImageUrl="/images/banners/21_brand_banner_pc.jpg",CourseUrl="http://localhost:8080/" },
                new(){ ImageUrl="/images/banners/21_syz_banner_pc.jpg",CourseUrl="http://localhost:8080/" }
            };
            return list;
        }
    }
}
