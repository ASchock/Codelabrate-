using Microsoft.AspNetCore.Identity;
using System;

namespace TeacherStudentEditor.Models
{
    //public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<applicationuser>
    //{
    //    public MyUserClaimsPrincipalFactory(
    //        UserManager<applicationuser> userManager,
    //        IOptions<identityoptions> optionsAccessor)
    //        : base(userManager, optionsAccessor)
    //    {
    //    }

    //    protected override async Task<claimsidentity> GenerateClaimsAsync(ApplicationUser user)
    //    {
    //        var identity = await base.GenerateClaimsAsync(user);
    //        identity.AddClaim(new Claim("ContactName", user.ContactName ?? ""));
    //        return identity;
    //    }
    //}

    public class ApplicationUser : IdentityUser<Guid>
    {
        
    }
}