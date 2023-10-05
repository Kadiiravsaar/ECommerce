using Core.Utilities.Response;
using Entitites.Dtos.Auth;
using Entitites.Dtos.User;

namespace Buissnes.Abstract
{
    public interface IAuthService
    {
        Task<ApiDataResponse<UserDto>> Login(LoginDto loginDto);
    }
}
