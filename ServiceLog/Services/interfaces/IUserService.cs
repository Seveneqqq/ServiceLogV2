using ServiceLog.Filters;
using ServiceLog.Models.Dto.UserDto;

namespace ServiceLog.Services.interfaces
{
    public interface IUserService
    {
        Task<GetUserDataByIdResponseDto> GetUserDataByIdAsync(string userId);
        Task<DeleteUserByIdResponseDto> DeleteUserByIdAsync(string userId);
        Task<GetAllUsersResponseDto> GetAllUsersAsync(UserFilter? userFilter);
        Task<UpdateUserByIdResponseDto> UpdateUserByIdAsync(string id, UpdateUserByIdRequestDto updateUserByIdRequestDto);
    }
}
