using System;
using BLL.Services.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
            private readonly IAccountService _accountService;

            public AccountController(IAccountService accountService)
            {
                _accountService = accountService;
            }

            [Authorize(AuthenticationSchemes = "Bearer")]
            [HttpPost]
            public IActionResult UpdateUserInfo([FromBody] UserModel user)
            {

                var resultModel = new LoginResultModel();
                try
                {
                    resultModel = _accountService.UpdateUserInfo(user);
                    resultModel.Message = "User information has been updated";
                    resultModel.Successed = true;
                }
                catch (Exception e)
                {
                    resultModel.Successed = false;
                    resultModel.Message = e.Message;
                }

                return Ok(resultModel);
            }

            [Authorize(AuthenticationSchemes = "Bearer")]
            [HttpPost]
            public IActionResult ChangeUserEmail([FromBody] EmailModel model)
            {
                var resultModel = new BaseModel();
                resultModel.Successed = true;
                try
                {
                    _accountService.TryEmailChanging(model.UserId, model.NewEmail);
                    resultModel.Message = "We have sent you a confirmation email, please check your inbox";
                }
                catch (Exception e)
                {
                    resultModel.Successed = false;
                    resultModel.Message = e.Message;
                }
                return Ok(resultModel);
            }
            [HttpGet]
            public IActionResult ConfirmEmailChanging(string userId, string newEmail, string token)
            {
                _accountService.ChangeEmail(userId, newEmail, token.Replace(' ','+'));

                return Ok("Email have been changed");
            }
            [Authorize(AuthenticationSchemes = "Bearer")]
            [HttpPost]
            public IActionResult ChangeUserPassword([FromBody] PasswordModel model)
            {
                BaseModel result = new BaseModel();
                try
                {
                    _accountService.ChangePassword(model.UserId, model.CurrentPassword, model.NewPassword);
                    result.Message = "Password is changed";
                    result.Successed = true;
                }
                catch (Exception e)
                {
                    result.Message = e.Message;
                    result.Successed = false;
                }
                return Ok(result);
            }
        
    }
}
