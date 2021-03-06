﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLL.Services.Interfaces;
using BLL.Models;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var res = await _userService.UserAuthentication(login.UserName, login.Password);
            if (res)
            {
                var resultModel = await _userService.UpdateTokensByUsername(login.UserName);
                HttpContext.Response.Cookies.Append("UserId", resultModel.User.Id);
                HttpContext.Response.Cookies.Append("AccessToken", resultModel.AccessToken);
                HttpContext.Response.Cookies.Append("RefreshToken", resultModel.RefreshToken);
                HttpContext.Response.Headers.Add("Token-Expired", "false");
                return Ok(resultModel);
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] UserModel user)
        {
            var result = await _userService.UserRegistration(user);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpGet]
        public IActionResult ConfirmEmail([FromQuery(Name = "userId")] string userId, [FromQuery(Name = "token")] string token)
        {
            _userService.ConfirmUserEmail(userId, token);

            return Ok("Email have been confirmed");
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken()
        {
            StringValues valueUserId;
            HttpContext.Request.Headers.TryGetValue("UserId", out valueUserId);
            if (valueUserId.Count > 0)
            {
                var userId = valueUserId.First().ToString();
                StringValues stringValues;
                HttpContext.Request.Headers.TryGetValue("RefreshToken", out stringValues);
                var refreshToken = stringValues.First().ToString();


                if (_userService.VerifyRefreshToken(userId, refreshToken))
                {
                    var resultModel = await _userService.UpdateTokens(userId);
                    HttpContext.Response.Cookies.Append("UserId", resultModel.User.Id);
                    HttpContext.Response.Cookies.Append("AccessToken", resultModel.AccessToken);
                    HttpContext.Response.Cookies.Append("RefreshToken", resultModel.RefreshToken);
                    HttpContext.Response.Headers.Add("Token-Expired", "false");
                    return Ok();
                }
                else
                {
                    return StatusCode(403);
                }

            }
            else
            {
                return StatusCode(403);
            }
        }



    }
}
