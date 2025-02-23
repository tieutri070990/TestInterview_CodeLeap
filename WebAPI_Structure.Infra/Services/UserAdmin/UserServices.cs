using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAPI_Structure.App.DTO;
using WebAPI_Structure.Core.Models;
using WebAPI_Structure.Infra.Context;

namespace WebAPI_Structure.Infra.Services.UserAdmin
{
    public class UserServices : IUserServices 
    {
        private readonly DemoTestDBConText _context;
        private readonly IConfiguration _configuration;
        private int salt = 11;
        public UserServices(DemoTestDBConText context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ErrorOr<int>> Create(UserInfoDTO request)
        {
            bool isEmail = Regex.IsMatch(request.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if(!isEmail)
                return Error.Failure("Email is not valid!");

            var user = await _context.Users
                            .Where(x => x.Email == request.Email)
                            .SingleOrDefaultAsync();

            if(user != null)
                return Error.Failure("User is exist. Please input other user!");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = passwordHash,
                IsActive = true,
                IsDeleted = false,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser.Id;
        }

        public async Task<ErrorOr<UserInfoResponse>> GetUserInfo(string email)
        {
            var user = await _context.Users
                            .Where(x => x.Email == email)
                            .SingleOrDefaultAsync();

            if (user == null) return Error.Failure("User is not exist!");

            return new UserInfoResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,   
                Email = user.Email,
                UserId = user.Id
            };
        }

        public async Task<ErrorOr<UserResponse>> Login(UserDTO request)
        {
            var user = await _context.Users
                            .Where(x => x.Email == request.Email && x.IsActive == true)
                            .SingleOrDefaultAsync();

            if (user == null) return Error.Failure("User is not exist!");

            if (user.Email != request.Email)
            {
                return Error.Failure("Email is not exist!");
            }

            if (user.IsActive == false) return Error.Failure("User is not active!");

            if (user.IsDeleted == true) return Error.Failure("User is deleted ");

            bool verified = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (verified)
            {
                string token = CreateToken(user);
                var data = new UserResponse();
                data.Email = user.Email;
                data.UserId = user.Id;
                data.FirstName = user.FirstName;
                data.IsActive = user.IsActive;
                data.LastName = user.LastName;
                data.Token = token;
                return data;
            }
            else
            {
                return Error.Failure("Password is not match");
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Appsettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            DateTime d1 = DateTime.Now;
            DateTime d2 = d1.AddSeconds(300);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: d2,
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
