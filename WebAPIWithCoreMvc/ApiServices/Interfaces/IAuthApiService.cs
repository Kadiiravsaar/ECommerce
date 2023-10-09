using Core.Utilities.Response;
using Entitites.Dtos.Auth;
using Entitites.Dtos.User;

namespace WebAPIWithCoreMvc.ApiServices.Interfaces
{
    public interface IAuthApiService
    {
        Task<ApiDataResponse<UserDto>> LoginAsync(LoginDto loginDto);
    }
}
