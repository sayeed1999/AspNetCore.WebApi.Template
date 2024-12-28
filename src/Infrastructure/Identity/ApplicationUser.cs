using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AspNetCore.WebApi.Template.Infrastructure.Identity;

public class ApplicationUserToken : IdentityUserToken<Guid> { }
public class ApplicationUserLogin : IdentityUserLogin<Guid> { }
public class ApplicationRoleClaim : IdentityRoleClaim<Guid> { }
public class ApplicationUserRole : IdentityUserRole<Guid> { }
public class ApplicationUser : IdentityUser<Guid> { }
public class ApplicationUserClaim : IdentityUserClaim<Guid> { }
public class ApplicationRole(string role) : IdentityRole<Guid>(role) { }
