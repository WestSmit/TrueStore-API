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
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

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

        public async Task<IdentityResult> UserRegistration(UserModel user)
        {
            var newUser = _mapper.Map<User>(user);
            var result = await _database.UserRepostitory.CreateUser(newUser, user.Password);

            if (result.Succeeded)
            {
                var token = _database.UserRepostitory.GenereteEmailConfirmToken(newUser);
                EmailSender emailSender = new EmailSender();
                emailSender.SendEmail(user.Email, "Confirm your account",
                    $"<h3>To confirm your email address click \" <a href='http://localhost:5000/api/User/ConfirmEmail?userId={newUser.Id}&token={token}'>link</a></h3>");
            }

            return result;
        }

        public void ConfirmUserEmail(string userId, string token)
        {
            _database.UserRepostitory.ConfirmEmail(userId, token);
        }

        public async Task<bool> UserAuthentication(string username, string password)
        {
            var user = await _database.UserRepostitory.GetUserByName(username);

            if (user == null)
            {
                return false;
            }

            var result = await _database.UserRepostitory.ValidateUser(user, password);

            return result;
        }
        public async Task<LoginResultModel> UpdateTokensByUsername(string username)
        {
            var user = await _database.UserRepostitory.GetUserByName(username);

            return await UpdateTokens(user.Id);
        }
        public async Task<LoginResultModel> UpdateTokens(string userId)
        {
            var user = await _database.UserRepostitory.GetUser(userId);
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
