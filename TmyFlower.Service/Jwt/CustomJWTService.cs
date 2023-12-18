﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TmyFlower.Model;
using TmyFlower.Service.User.Dto;

namespace TmyFlower.Service
{
    public class CustomJWTService: ICustomJWTService
    {
        private JWTTokenOptions _JWTTokenOptions;
        private readonly IOptionsMonitor<JWTTokenOptions> _jwtTokenOptionsMonitor;
        /// <summary>
        ///  
        /// </summary>
        /// <param name="jwtTokenOptions"></param>
        public CustomJWTService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptionsMonitor)
        {
            _jwtTokenOptionsMonitor = jwtTokenOptionsMonitor;
            _JWTTokenOptions = _jwtTokenOptionsMonitor.CurrentValue;
            _jwtTokenOptionsMonitor.OnChange((options, name) =>
            {
                _JWTTokenOptions = options;
            });
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetToken(UserRes user)
        {
            #region 有效载荷，可以自己写，爱写多少写多少；尽量避免敏感信息
            var claims = new[]
            {
                new Claim("Id",user.Id.ToString()),
                new Claim("NickName",user.NickName),
                new Claim("UserName",user.UserName),
                new Claim("UserType",user.UserType.ToString()),
            };

            //需要加密：需要加密key:
            //Nuget引入：Microsoft.IdentityModel.Tokens
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SecurityKey));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Nuget引入：System.IdentityModel.Tokens.Jwt
            JwtSecurityToken token = new JwtSecurityToken(
             issuer: _JWTTokenOptions.Issuer,
             audience: _JWTTokenOptions.Audience,
             claims: claims,
             expires: DateTime.Now.AddMinutes(10),//5分钟有效期
             signingCredentials: creds);

            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
            #endregion
        }
    }
}
