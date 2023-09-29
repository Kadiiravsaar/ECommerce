using Core.Utilities.Security.Token;
using Entitites.Dtos.UserDtos;

namespace Buissnes.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserDetailDto>> GetListAsync();

        Task<UserDto> GetByIdAsync(int id);

        Task<UserDto> AddAsync(UserAddDto userAddDto);

        Task<UserUpdateDto> UpdateAsync(UserUpdateDto userUpdateDto);

        Task<bool> DeleteAsync(int id);

        Task<AccessToken> Authenticate(UserForLoginDto userForLoginDto);

        
    }
}
