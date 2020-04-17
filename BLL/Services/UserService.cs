using BLL.Helpers;
using BLL.Services.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using System.Security.Claims;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public void UserRegistration(UserModel user)
        {
            var newUser = _mapper.Map<User>(user);
            User createdUser = _database.UserRepostitory.CreateUser(newUser, user.Password);

             var token = _database.UserRepostitory.GenereteEmailConfirmToken(createdUser);

            EmailSender emailSender = new EmailSender();

            emailSender.SendEmail(user.Email, "Confirm your account", $"<h3>To confirm your email address click \" <a href='http://192.168.0.102:50395/api/User/ConfirmEmail?userId={createdUser.Id}&token={token}'>link</a></h3>");
        }

        public void ConfirmUserEmail(string userId, string token)
        {
            _database.UserRepostitory.ConfirmEmail(userId, token);
        }

        public LoginResultModel UserAuthentication(string userName, string password)
        {
            var user = _database.UserRepostitory.ValidateUser(userName, password);
            return UpdateTokens(user.Id);
        }

        public LoginResultModel UpdateTokens(string userId)
        {
            var user = _database.UserRepostitory.GetUser(userId);
            if (user != null)
            {
                DateTime timeNow = DateTime.UtcNow;

                UserModel userModel = _mapper.Map<UserModel>(user);

                userModel.Role = _database.UserRepostitory.GetRole(user);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userModel.Id.ToString()),
                        new Claim(ClaimTypes.Role, userModel.Role)
                    }),
                    Issuer = AuthenticationOptions.ISSUER,
                    Audience = AuthenticationOptions.AUDIENCE,
                    NotBefore = timeNow,
                    Expires = timeNow.Add(TimeSpan.FromMinutes(AuthenticationOptions.LIFETIME)),
                    SigningCredentials = new SigningCredentials(AuthenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
                };

                var jwt = tokenHandler.CreateToken(tokenDescriptor);
               
                var refreshToken = TokenGenerator.Generate().Trim();

                LoginResultModel result = new LoginResultModel();
                result.User = userModel;
                result.AccessToken = tokenHandler.WriteToken(jwt);
                result.RefreshToken = refreshToken;

                _database.UserRepostitory.SetRefreshToken(user, refreshToken);
                return result;
            }
            else
            {
                return null;
            }
        }

        public bool VerifyRefreshToken(string userId, string token)
        {
            if (_database.UserRepostitory.HasValidRefreshToken(userId))
            {
                if (_database.UserRepostitory.GetRefreshToken(userId) == token)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}
