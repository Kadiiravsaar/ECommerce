using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIWithCoreMvc.ApiServices.Interfaces;

namespace WebAPIWithCoreMvc.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UserController : Controller
    {
        IUserApiService _userApiService;
        IHttpContextAccessor _contextAccessor;

        public UserController(IUserApiService userApiService, IHttpContextAccessor contextAccessor)
        {
            _userApiService = userApiService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _userApiService.GetListAsync());

            
        }
    }
}
