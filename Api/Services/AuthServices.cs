using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.UserModel;
using Microsoft.AspNetCore.Identity;

namespace Api.Services
{
    public class AuthServices : IAuthServices
    {   
        private readonly UserManager<User> _userManager;
        private readonly SignInManager <User> _signInManager;
        public AuthServices(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public Task<string> Login(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public Task<string> Register(User user)
        {
             throw new NotImplementedException();
        }
    }
}