using AutoMapper;
using Buissnes.Abstract;
using Buissnes.Constants;
using Core.Utilities.Response;
using Core.Utilities.Security.Token;
using DataAccess.Abstract;
using Entitites.Concrete;
using Entitites.Dtos.UserDtos;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Buissnes.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UserService(IUserDal userDal, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _userDal = userDal;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<ApiDataResponse<IEnumerable<UserDetailDto>>> GetListAsync(Expression<Func<User, bool>> filter = null)
        {
            if (filter == null)
            {
                var response = await _userDal.GetListAsync();
                var userDetailDtos = _mapper.Map<IEnumerable<UserDetailDto>>(response);
                return new SuccessApiDataResponse<IEnumerable<UserDetailDto>>(userDetailDtos, Messages.Listed);
            }
            else
            {
                var response = await _userDal.GetListAsync(filter);
                var userDetailDtos = _mapper.Map<IEnumerable<UserDetailDto>>(response);
                return new SuccessApiDataResponse<IEnumerable<UserDetailDto>>(userDetailDtos, Messages.Listed);
            }

        }

        public async Task<ApiDataResponse<UserDto>> GetByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(x => x.Id == id);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return new SuccessApiDataResponse<UserDto>(userDto, Messages.Listed);
            }
            return new ErrorApiDataResponse<UserDto>(null, Messages.NotListed);

        }

        public async Task<ApiDataResponse<UserDto>> AddAsync(UserAddDto userAddDto)
        {
            var user = _mapper.Map<User>(userAddDto);

            user.CreatedDate = DateTime.Now;
            user.CreatedUserId = 1;

            var userAdd = await _userDal.AddAsync(user);

            var userDto = _mapper.Map<UserDto>(user);
            
            return new SuccessApiDataResponse<UserDto>(userDto, Messages.Listed);
            //Todo:create
        }

        public async Task<ApiDataResponse<UserUpdateDto>> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var getUser = await _userDal.GetAsync(x => x.Id == userUpdateDto.Id);

            var user = _mapper.Map<User>(userUpdateDto);

            user.CreatedDate = getUser.CreatedDate;
            user.CreatedUserId = getUser.CreatedUserId;
            user.UpdatedDate = DateTime.Now;
            user.UpdatedUserId =1;
            user.Token = userUpdateDto.Token;
            user.TokenExpireDate = userUpdateDto.TokenExpireDate;


            var resultUpdate = await _userDal.UpdateAsync(user);

            var userUpdateDtoMap = _mapper.Map<UserUpdateDto>(resultUpdate);
          
            return new SuccessApiDataResponse<UserUpdateDto>(userUpdateDtoMap, Messages.Updated);

        }

        public async Task<ApiDataResponse<bool>> DeleteAsync(int id)
        {
            var deletedUser = await _userDal.DeleteAsync(id);
            return new SuccessApiDataResponse<bool>(deletedUser, Messages.Deleted);
        }

        public async Task<ApiDataResponse<UserDto>> GetAsync(Expression<Func<User, bool>> filter)
        {
            var user = await _userDal.GetAsync(filter);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDto>(user);
                return new SuccessApiDataResponse<UserDto>(userDto, Messages.Listed);
            }
            return new ErrorApiDataResponse<UserDto>(null, Messages.NotListed);

        }

        //public async Task<AccessToken> Authenticate(UserForLoginDto userForLoginDto)
        //{
        //    var user = await _userDal.GetAsync(x => x.UserName == userForLoginDto.UserName && x.Password == userForLoginDto.Password);

        //    if (user == null)
        //    {
        //        return null;

        //    }

        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    var key = Encoding.ASCII.GetBytes(_appSettings.SecurityKey);

        //    var tokenDescriptor = new SecurityTokenDescriptor()
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.Id.ToString())     //                 burası token içeriği
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };


        //    var token = tokenHandler.CreateToken(tokenDescriptor); // token oluşturucaz

        //    AccessToken accessToken = new AccessToken()
        //    {
        //        Token = tokenHandler.WriteToken(token),
        //        UserName = user.UserName,
        //        Expiration = (DateTime)tokenDescriptor.Expires,
        //        UserId = user.Id
        //    };

        //    return await Task.Run(() => accessToken);
        //}


    }
}
