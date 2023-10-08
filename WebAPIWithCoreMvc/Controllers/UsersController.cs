using Entitites.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using WebAPIWithCoreMvc.ViewModel;

namespace WebAPIWithCoreMvc.Controllers
{
    public class UsersController : Controller
    {
        #region Define ve url
        private readonly HttpClient _httpClient;
        private string url = "https://localhost:7258/api/";
        #endregion

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        #region CRUD işlemleri

        public async Task<IActionResult> Index()
        {
            var users = await _httpClient.GetFromJsonAsync<List<UserDetailDto>>(url + "Users/GetList");
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.GenderList = GenderFill();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddViewModel userAddViewModel)
        {
            UserAddDto userAddDto = new UserAddDto()
            {
                UserName = userAddViewModel.UserName,
                FirstName = userAddViewModel.FirstName,
                LastName = userAddViewModel.LastName,
                Email = userAddViewModel.Email,
                Address = userAddViewModel.Address,
                DateOfBirth = userAddViewModel.DateOfBirth,
                Gender = userAddViewModel.GenderId == 1 ? true : false,
                Password = userAddViewModel.Password
            };
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(url + "Users/Add", userAddDto);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<UserDto>(url + "Users/GetById/" + id);
            UserUpdateViewModel userUpdateViewModel = new UserUpdateViewModel()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                GenderId = user.Gender == true ? 1 : 2,
                Password = user.Password

            };
            ViewBag.GenderList = GenderFill();

            return View(userUpdateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UserUpdateViewModel userViewModel)
        {
            UserUpdateDto userUpdateDto = new UserUpdateDto()
            {
                Id = userViewModel.Id,
                UserName = userViewModel.UserName,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Email = userViewModel.Email,
                Address = userViewModel.Address,
                DateOfBirth = userViewModel.DateOfBirth,
                Gender = userViewModel.GenderId == 1 ? true : false,
                Password = userViewModel.Password

            };

            HttpResponseMessage httpResponseMessage = await _httpClient.PutAsJsonAsync(url + "Users/Update", userUpdateDto);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<UserDto>(url + "Users/GetById/" + id);
            UserDeleteViewModel userDeleteViewModel = new UserDeleteViewModel()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                GenderName = user.Gender == true ? "Erkek" : "Kadın",
                Password = user.Password

            };
            ViewBag.GenderList = GenderFill();

            return View(userDeleteViewModel);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) // silne onaylandı
        {
            await _httpClient.DeleteAsync(url + "Users/Delete/" + id);



            return RedirectToAction("Index");


        }
         // demografik bilgilerin hatalı olduğunu


        #endregion


        #region Method

        private dynamic GenderFill()
        {
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender()
            {
                Id = 1,
                GenderName = "Erkek"
            });
            genders.Add(new Gender()
            {
                Id = 2,
                GenderName = "Kadın"
            });
            return genders;
        }
        private class Gender
        {
            public int Id { get; set; }
            public string GenderName { get; set; }
        }

        #endregion




    }
}
