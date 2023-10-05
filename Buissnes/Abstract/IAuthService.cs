using Core.Utilities.Response;
using Entitites.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buissnes.Abstract
{
    public interface IAuthService
    {
        Task<ApiDataResponse<UserDto>> Login(LoginDto loginDto);
    }
}
