using Interesse.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interesse.App.Extensions
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<User> userManager)
        {
            //if (userManager.Users.Any()) return;

            //var user = new User
            //{
            //    UserName = "email",
            //    Email = "email",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //};

            //var result = userManager.CreateAsync(user, "password").Result;
        }
    }
}
