using Core.Utilities.Response;
using Core.Utilities.Security.Token;
using Entitites.Concrete;
using Entitites.Dtos.UserDtos;
using System.Linq.Expressions;

namespace Buissnes.Abstract
{
    public interface IUserService
    {
        Task<ApiDataResponse<IEnumerable<UserDetailDto>>> GetListAsync(Expression<Func<User, bool>> filter = null);

        Task<ApiDataResponse<UserDto>> GetAsync(Expression<Func<User, bool>> filter);
        Task<ApiDataResponse<UserDto>> GetByIdAsync(int id);

        Task<ApiDataResponse<UserDto>> AddAsync(UserAddDto userAddDto);

        Task<ApiDataResponse<UserUpdateDto>> UpdateAsync(UserUpdateDto userUpdateDto);

        Task<ApiDataResponse<bool>> DeleteAsync(int id);

        //Task<AccessToken> Authenticate(UserForLoginDto userForLoginDto);


    }
}
