﻿using Core.Entitites.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitites.Dtos.Auth
{
    public class LoginDto : IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
