using Entitites.Dtos.User;

namespace WebAPIWithCoreMvc.ApiServices.Interfaces
{
    public interface IUserApiService
    {
        Task<List<UserDetailDto>> GetListAsync();
    }
}
