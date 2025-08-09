using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLog.Enums;
using ServiceLog.Filters;
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

        public async Task<GetAllUsersResponseDto> GetAllUsersAsync(UserFilter? userFilter)
        {
            try
            {
                
                var usersQuery = _userManager.Users.AsQueryable();
                if (userFilter != null)
                {
                    if (!string.IsNullOrEmpty(userFilter.Email))
                    {
                        usersQuery = usersQuery.Where(u => u.Email.Contains(userFilter.Email));
                    }
                    if (!string.IsNullOrEmpty(userFilter.UserName))
                    {
                        usersQuery = usersQuery.Where(u => u.UserName.Contains(userFilter.UserName));
                    }
                    if (!string.IsNullOrEmpty(userFilter.PhoneNumber))
                    {
                        usersQuery = usersQuery.Where(u => u.PhoneNumber.Contains(userFilter.PhoneNumber));
                    }
                    if (!string.IsNullOrEmpty(userFilter.Role))
                    {
                        usersQuery = usersQuery.Where(u => _userManager.GetRolesAsync(u).Result.Contains(userFilter.Role));
                    }
                }

                var users = await usersQuery.ToListAsync();

                return new GetAllUsersResponseDto
                {
                    Success = true,
                    Message = "Users retrieved successfully.",
                    Users = users
                };
            }
            catch (Exception ex)
            {
                return new GetAllUsersResponseDto
                {
                    Success = false,
                    Message = $"Error retrieving users: {ex.Message}",
                    ErrorCode = UserErrorCode.Unknown
                };
            }
        }

        public async Task<GetUserDataByIdResponseDto> GetUserDataByIdAsync(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return new GetUserDataByIdResponseDto
                {
                    Success = false,
                    Message = "Request cannot be null.",
                    ErrorCode = UserErrorCode.EmptyFields,
                    User = null
                };
            }
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new GetUserDataByIdResponseDto
                    {
                        Success = false,
                        Message = "User not found.",
                        ErrorCode = UserErrorCode.UserNotFound,
                        User = null
                    };
                }
                return new GetUserDataByIdResponseDto
                {
                    Success = true,
                    Message = "User data retrieved successfully.",
                    User = user
                };
            }
            catch (Exception ex)
            {
                return new GetUserDataByIdResponseDto
                {
                    Success = false,
                    Message = $"Error retrieving user data: {ex.Message}",
                    ErrorCode = UserErrorCode.Unknown,
                    User = null
                };
            }
        }

        public async Task<UpdateUserByIdResponseDto> UpdateUserByIdAsync(string userId, UpdateUserByIdRequestDto updateUserByIdRequestDto)
        {
            if (updateUserByIdRequestDto == null || string.IsNullOrEmpty(userId))
            {
                return new UpdateUserByIdResponseDto
                {
                    Success = false,
                    Message = "Request cannot be null.",
                    ErrorCode = UserErrorCode.EmptyFields
                };
            }
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new UpdateUserByIdResponseDto
                    {
                        Success = false,
                        Message = "User not found.",
                        ErrorCode = UserErrorCode.UserNotFound
                    };
                }
                user.Email = updateUserByIdRequestDto.Email ?? user.Email;
                user.PhoneNumber = updateUserByIdRequestDto.PhoneNumber ?? user.PhoneNumber;
                user.UserName = updateUserByIdRequestDto.UserName ?? user.UserName;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new UpdateUserByIdResponseDto
                    {
                        Success = true,
                        Message = "User updated successfully."
                    };
                }
                else
                {
                    return new UpdateUserByIdResponseDto
                    {
                        Success = false,
                        Message = "Failed to update user.",
                        ErrorCode = UserErrorCode.Unknown
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateUserByIdResponseDto
                {
                    Success = false,
                    Message = $"Error updating user: {ex.Message}",
                    ErrorCode = UserErrorCode.Unknown
                };
            }
        }
    }
}
