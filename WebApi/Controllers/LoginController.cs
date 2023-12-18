using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TmyFlower.Common;
using TmyFlower.Model;
using TmyFlower.Service.User.Dto;
using TmyFlower.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IUserService _userService;
        public ICustomJWTService _customJWTService;
        public LoginController(IUserService userService, ICustomJWTService customJWTService)
        {
            _userService = userService;
            _customJWTService = customJWTService;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetValidateCodeImages(string t)
        {
            var validateCodeString = Tools.CreateValidateString();
            //将验证码记入缓存中
            MemoryHelper.SetMemory(t, validateCodeString, 5);
            //接收图片返回的二进制流
            byte[] buffer = Tools.CreateValidateCodeBuffer(validateCodeString);
            return File(buffer, @"image/jpeg");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Check(UserReq req)
        {
            var currCode = MemoryHelper.GetMemory(req.ValidateKey);
            ApiResult apiResult = new ApiResult() { IsSuccess = false };
            if (string.IsNullOrEmpty(req.UserName) || string.IsNullOrEmpty(req.Password))
            {
                apiResult.Msg = "参数不能为空！";
            }
            //else if (currCode == null)
            //{
            //    apiResult.Msg = "验证码不存在，请刷新验证码！";
            //}
            //else if (currCode.ToString() != req.ValidateCode)
            //{
            //    apiResult.Msg = "验证码错误，请重新输入或刷新重试！";
            //}
            else
            {
                UserRes user = _userService.GetUsers(req);
                if (string.IsNullOrEmpty(user.UserName))
                {
                    apiResult.Msg = "账号不存在，用户名或密码错误！";
                }
                else
                {
                    apiResult.IsSuccess = true;
                    apiResult.Result = _customJWTService.GetToken(user);
                }
            }
            return apiResult;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Register(RegisterReq req)
        {
            //var currCode = MemoryHelper.GetMemory(req.ValidateKey);
            ApiResult apiResult = new ApiResult() { IsSuccess = false };
            if (string.IsNullOrEmpty(req.UserName) || string.IsNullOrEmpty(req.Password) || string.IsNullOrEmpty(req.NickName))
            {
                apiResult.Msg = "参数不能为空！";
            }
            //else if (currCode == null)
            //{
            //    apiResult.Msg = "验证码不存在，请刷新验证码！";
            //}
            //else if (currCode.ToString() != req.ValidateCode)
            //{
            //    apiResult.Msg = "验证码错误，请重新输入或刷新重试！";
            //}
            else
            {
                string msg = string.Empty;
                var res = _userService.RegisterUser(req, ref msg);
                if (!string.IsNullOrEmpty(msg))
                {
                    apiResult.Msg = msg;
                }
                else
                {
                    apiResult.IsSuccess = true;
                    apiResult.Result = _customJWTService.GetToken(res);
                }
            }
            return apiResult;
        }
    }
}
