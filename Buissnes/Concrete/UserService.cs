using Buissnes.Abstract;
using Buissnes.Constants;
using Core.Utilities.Response;
using Core.Utilities.Security.Token;
using DataAccess.Abstract;
using Entitites.Concrete;
using Entitites.Dtos.UserDtos;
using Microsoft.Extensions.Options;

namespace Buissnes.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly AppSettings _appSettings;

        public UserService(IUserDal userDal, IOptions<AppSettings> appSettings)
        {
            _userDal = userDal;
            _appSettings = appSettings.Value;
        }

        public async Task<ApiDataResponse<IEnumerable<UserDetailDto>>> GetListAsync()
        {
            List<UserDetailDto> userDetailDtos = new List<UserDetailDto>();
            var response = await _userDal.GetListAsync();
            foreach (var item in response.ToList())
            {
                userDetailDtos.Add(new UserDetailDto()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Gender = item.Gender == true ? "Erkek" : "Kadın",
                    DateOfBirth = item.DateOfBirth,
                    UserName = item.UserName,
                    Address = item.Address,
                    Email = item.Email,
                    Id = item.Id,
                });
            }
            return new SuccessApiDataResponse<IEnumerable<UserDetailDto>>(userDetailDtos,Messages.Listed);
        }

        public async Task<ApiDataResponse<UserDto>> GetByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(x => x.Id == id);
            if (user != null)
            {
                UserDto userDto = new UserDto()
                {
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Gender = user.Gender,
                    Id = user.Id,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Password = user.Password
                };
                return new SuccessApiDataResponse<UserDto>(userDto, Messages.Listed);

            }
            return new ErrorApiDataResponse<UserDto>(null,Messages.NotListed);
        }

        public async Task<ApiDataResponse<UserDto>> AddAsync(UserAddDto userAddDto)
        {
            User user = new User()
            {
                LastName = userAddDto.LastName,
                Address = userAddDto.Address,

                //Todo:CreatedDate ve CreatedUserId düzenlenecek.
                CreatedDate = DateTime.Now,
                CreatedUserId = 1,
                DateOfBirth = userAddDto.DateOfBirth,
                Email = userAddDto.Email,
                FirstName = userAddDto.FirstName,
                Gender = userAddDto.Gender,
                Password = userAddDto.Password,
                UserName = userAddDto.UserName,
            };

            var userAdd = await _userDal.AddAsync(user);

            UserDto userDto = new UserDto()
            {
                LastName = userAdd.LastName,
                Address = userAdd.Address,
                DateOfBirth = userAdd.DateOfBirth,
                Email = userAdd.Email,
                FirstName = userAdd.FirstName,
                Gender = userAdd.Gender,
                UserName = userAdd.UserName,
                Id = userAdd.Id,
            };
            return new SuccessApiDataResponse<UserDto>(userDto, Messages.Listed);

        }

        public async Task<ApiDataResponse<UserUpdateDto>> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var getUser = await _userDal.GetAsync(x => x.Id == userUpdateDto.Id);
            User user = new User()
            {
                LastName = userUpdateDto.LastName,
                Address = userUpdateDto.Address,
                DateOfBirth = userUpdateDto.DateOfBirth,
                Email = userUpdateDto.Email,
                FirstName = userUpdateDto.FirstName,
                Gender = userUpdateDto.Gender,
                UserName = userUpdateDto.UserName,
                Id = userUpdateDto.Id,
                CreatedDate = getUser.CreatedDate,
                CreatedUserId = getUser.CreatedUserId,
                Password = userUpdateDto.Password,
                UpdatedDate = DateTime.Now,
                UpdatedUserId = 1,
            };
            var userUpdate = await _userDal.UpdateAsync(user);
            UserUpdateDto newUserUpdateDto = new UserUpdateDto()
            {
                LastName = userUpdate.LastName,
                Address = userUpdate.Address,
                DateOfBirth = userUpdate.DateOfBirth,
                Email = userUpdate.Email,
                FirstName = userUpdate.FirstName,
                Gender = userUpdate.Gender,
                UserName = userUpdate.UserName,
                Id = userUpdate.Id,
                Password = userUpdate.Password,
            };
            return new SuccessApiDataResponse<UserUpdateDto>(newUserUpdateDto, Messages.Updated);

        }

        public async Task<ApiDataResponse<bool>> DeleteAsync(int id)
        {
            var deletedUser=  await _userDal.DeleteAsync(id);
            return new SuccessApiDataResponse<bool>(deletedUser, Messages.Deleted);
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
