using ServiceLog.Models.Dto.UserDto;

namespace ServiceLog.Services.interfaces
{
    public interface IUserService
    {
        Task<GetUserDataByIdResponseDto> GetUserDataByIdAsync(string userId);
        Task<DeleteUserByIdResponseDto> DeleteUserByIdAsync(string userId);
        Task<GetAllUsersResponseDto> GetAllUsersAsync(GetAllUsersRequestDto getAllUsersRequestDto);
        Task<UpdateUserByIdResponseDto> UpdateUserByIdAsync(UpdateUserByIdRequestDto updateUserByIdRequestDto);
    }
}
