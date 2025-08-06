using ServiceLog.Models.Dto.UserDto;

namespace ServiceLog.Services.interfaces
{
    public interface IUserService
    {
        Task<GetUserDataByIdResponseDto> GetUserDataByIdAsync(GetUserDataByIdRequestDto getUserDataByIdRequestDto);
        Task<DeleteUserByIdResponseDto> DeleteUserByIdAsync(DeleteUserByIdRequestDto deleteUserByIdRequestDto);
        Task<GetAllUsersResponseDto> GetAllUsersAsync(GetAllUsersRequestDto getAllUsersRequestDto);
        Task<UpdateUserByIdResponseDto> UpdateUserByIdAsync(UpdateUserByIdRequestDto updateUserByIdRequestDto);
    }
}
