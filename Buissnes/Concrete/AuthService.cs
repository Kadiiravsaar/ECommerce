using AutoMapper;
using Buissnes.Abstract;
using Buissnes.Constants;
using Core.Utilities.Response;
using Core.Utilities.Security.Token;
using Entitites.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buissnes.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(IUserService userService, ITokenService tokenService, IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<ApiDataResponse<UserDto>> Login(LoginDto loginDto)
        {
            var hasUser = await _userService.GetAsync(x => x.UserName == loginDto.UserName && x.Password == loginDto.Password);

            if (hasUser == null)
            {
                return new ErrorApiDataResponse<UserDto>(null, Messages.NotUser);
            }
            else
            {
                if (hasUser.Data.TokenExpireDate == null || String.IsNullOrEmpty(hasUser.Data.Token))
                {
                    return await UpdateToken(hasUser);
                }

                if (hasUser.Data.TokenExpireDate < DateTime.Now)
                {
                    return await UpdateToken(hasUser);
                }
            }
            return new SuccessApiDataResponse<UserDto>(hasUser.Data, Messages.SuccesLogin);
        }


        private async Task<ApiDataResponse<UserDto>> UpdateToken(ApiDataResponse<UserDto> hasUser)
        {
            var accessToken = _tokenService.CreateToken(hasUser.Data.Id, hasUser.Data.UserName);

            var userUpdateDto = _mapper.Map<UserUpdateDto>(hasUser.Data);

            userUpdateDto.Token = accessToken.Token;

            userUpdateDto.TokenExpireDate = accessToken.Expiration;

            userUpdateDto.UpdatedUserId = hasUser.Data.Id;

            var newUserUpdateDto = await _userService.UpdateAsync(userUpdateDto);

            var userDto = _mapper.Map<UserDto>(newUserUpdateDto.Data);

            return new SuccessApiDataResponse<UserDto>(userDto, Messages.SuccesLogin);
        }
    }
}
