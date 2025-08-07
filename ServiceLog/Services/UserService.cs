using Microsoft.AspNetCore.Identity;
using ServiceLog.Enums;
using ServiceLog.Models.Dto.UserDto;
using ServiceLog.Services.interfaces;
using static ServiceLog.Enums.UserErrorCodes;

namespace ServiceLog.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DeleteUserByIdResponseDto> DeleteUserByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId)) { 
                return new DeleteUserByIdResponseDto
                {
                    Success = false,
                    Message = "Request cannot be null.",
                    ErrorCode = UserErrorCode.EmptyFields
                };
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new DeleteUserByIdResponseDto
                {
                    Success = false,
                    Message = "User not found.",
                    ErrorCode = UserErrorCode.UserNotFound
                };
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new DeleteUserByIdResponseDto
                {
                    Success = true,
                    Message = "User deleted successfully."
                };
            }
            else
            {
                return new DeleteUserByIdResponseDto
                {
                    Success = false,
                    Message = "Failed to delete user.",
                    ErrorCode = UserErrorCode.Unknown
                };
            }
        }

        public Task<GetAllUsersResponseDto> GetAllUsersAsync(GetAllUsersRequestDto getAllUsersRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDataByIdResponseDto> GetUserDataByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserByIdResponseDto> UpdateUserByIdAsync(UpdateUserByIdRequestDto updateUserByIdRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
