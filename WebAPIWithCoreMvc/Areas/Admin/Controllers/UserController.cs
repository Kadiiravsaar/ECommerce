﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIWithCoreMvc.ApiServices.Interfaces;

namespace WebAPIWithCoreMvc.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UserController : Controller
    {
        IUserApiService _userApiService;

        public UserController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _userApiService.GetListAsync());

            
        }
    }
}
