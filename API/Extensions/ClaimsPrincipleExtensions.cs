using System.Security.Authentication;
using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == claimsPrincipal.GetEmail()) ?? throw new AuthenticationException("User not found");
            
            return user;
        }

        public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == claimsPrincipal.GetEmail()) ?? throw new AuthenticationException("User not found");
            
            return user;
        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email) ?? throw new AuthenticationException("Email claim not found");
            
            return email;
        }
    }
}