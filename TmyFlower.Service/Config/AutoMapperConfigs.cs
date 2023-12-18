using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmyFlower.Model.Entitys;
using TmyFlower.Service.User.Dto;

namespace TmyFlower.Service.Config
{
    public class AutoMapperConfigs : Profile
    {
        /// <summary>
        /// 在构造函数中配置映射关系
        /// </summary>
        public AutoMapperConfigs()
        {
            //从A => B
            CreateMap<Flower, FlowerRes>();
            CreateMap<Users, UserRes>();
            CreateMap<RegisterReq, Users>();
        }
    }
}
