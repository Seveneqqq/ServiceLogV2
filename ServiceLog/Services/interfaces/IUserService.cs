namespace ServiceLog.Services.interfaces
{
    public interface IUserService
    {
        Task<GetUserDataByIdRequestDto> GetUserDataByIdAsync(GetUserDataByIdResponseDto getUserDataByIdResponseDto);
        Task<DeleteUserByIdResponseDto> DeleteUserByIdAsync(DeleteUserByIdRequestDto deleteUserByIdRequestDto);
        Task<GetAllUsersResponseDto> GetAllUsersAsync(GetAllUsersRequestDto getAllUsersRequestDto);
        Task<UpdateUserByIdResponseDto> UpdateUserByIdAsync(UpdateUserByIdRequestDto updateUserByIdRequestDto);
    }
}
