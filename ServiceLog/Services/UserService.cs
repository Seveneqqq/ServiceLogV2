using Microsoft.AspNetCore.Identity;
using ServiceLog.Models.Dto.UserDto;
using ServiceLog.Services.interfaces;

namespace ServiceLog.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<DeleteUserByIdResponseDto> DeleteUserByIdAsync(DeleteUserByIdRequestDto deleteUserByIdRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<GetAllUsersResponseDto> GetAllUsersAsync(GetAllUsersRequestDto getAllUsersRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDataByIdResponseDto> GetUserDataByIdAsync(GetUserDataByIdRequestDto getUserDataByIdRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserByIdResponseDto> UpdateUserByIdAsync(UpdateUserByIdRequestDto updateUserByIdRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
