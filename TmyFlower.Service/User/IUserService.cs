using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmyFlower.Model.Entitys;
using TmyFlower.Service.User.Dto;

namespace TmyFlower.Service
{
    public interface IUserService
    {
        UserRes GetUsers(UserReq req);
        UserRes RegisterUser(RegisterReq req, ref string msg);
    }
}
