using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_Structure.App.DTO;

namespace WebAPI_Structure.Infra.Services.UserAdmin
{
    public interface IUserServices
    {
        Task<ErrorOr<UserResponse>> Login(UserDTO request);

        Task<ErrorOr<int>> Create(UserInfoDTO request);

        Task<ErrorOr<UserInfoResponse>> GetUserInfo(string email);
    }
}
