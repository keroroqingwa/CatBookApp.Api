using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.UI;
using Backstage.Api.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.IdentityModel.Tokens;

namespace Backstage.Api.Controllers
{
    /// <summary>
    /// 账号相关
    /// </summary>
    [AbpAllowAnonymous]
    public class AccountController : CatControllerBase
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private string GetToken(long userId, string username)
        {
            // push the user’s name into a claim, so we can identify the user later on.
            var claims = new[]
            {
                   new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                   new Claim(ClaimTypes.Name, username)
                };
            //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "jwttest",
                audience: "jwttest",
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 用户登录，为了尽量简单，登录验证就不通过数据库了
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionRes> LoginAsync([FromBody]AccountLoginInput input)
        {
            if (input.UserName != _configuration["Account"] || input.Password != _configuration["AccountPwd"]) throw new UserFriendlyException("登录失败，用户账号或密码错误！");

            long userId = 1;
            string token = "";

            token = GetToken(userId, input.UserName);

            var data = new
            {
                status = "ok",
                type = input.Type,
                currentAuthority = "administrator",
                token,
                UserId = userId,
                NickName = input.UserName
            };
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionRes GetCurrentUser()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var data = new
                {
                    name = "小李",
                    avatar = "",
                    userid = 1
                };
                return ActionRes.Success(data);
            }

            return ActionRes.Fail("401 not login");
        }
    }
}