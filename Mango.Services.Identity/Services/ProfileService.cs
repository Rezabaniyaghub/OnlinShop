using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Services.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfileService(IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user=await _userManager.FindByIdAsync(sub);  
            ClaimsPrincipal userClaims=await _userClaimsPrincipalFactory.CreateAsync(user);
            List<Claim> claims = userClaims.Claims.ToList();
                claims=claims.Where(claim=>context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
            claims.Add(new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber??""));
            if (_userManager.SupportsUserRole)
            {
                IList<string> roles =await _userManager.GetRolesAsync(user);
                foreach (string role in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, role));
                    if (_roleManager.SupportsRoleClaims)
                    {
                        IdentityRole currentRole =await _roleManager.FindByNameAsync(role);
                        if(currentRole != null)
                        {
                            claims.AddRange(await _roleManager.GetClaimsAsync(currentRole));
                        }
                    }
                }
            }
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
           var sub= context.Subject.GetSubjectId();
            var user=await _userManager.FindByIdAsync(sub);
        }
    }
}
