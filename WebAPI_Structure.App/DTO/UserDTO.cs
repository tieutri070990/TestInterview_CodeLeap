using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_Structure.App.DTO
{
    public class UserDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UserInfoDTO : UserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }

    public static class RegexPatterns
    {
        public const string Email = @"^[a-zA-Z0-9._\\-]+@[a-zA-Z0-9]+(([\\-]*[a-zA-Z0-9]+)*[.][a-zA-Z0-9]+)+(;[ ]*[a-zA-Z0-9._\\-]+@[a-zA-Z0-9]+(([\\-]*[a-zA-Z0-9]+)*[.][a-zA-Z0-9]+)+)*$";
    }
}
