using Core.Utilities.Response;
using Core.Utilities.Security.Token;
using Entitites.Dtos.UserDtos;

namespace Buissnes.Abstract
{
    public interface IUserService
    {
        Task<ApiDataResponse<IEnumerable<UserDetailDto>>> GetListAsync();

        Task<ApiDataResponse<UserDto>> GetByIdAsync(int id);

        Task<ApiDataResponse<UserDto>> AddAsync(UserAddDto userAddDto);

        Task<ApiDataResponse<UserUpdateDto>> UpdateAsync(UserUpdateDto userUpdateDto);

        Task<ApiDataResponse<bool>> DeleteAsync(int id);

        //Task<AccessToken> Authenticate(UserForLoginDto userForLoginDto);


    }
}
