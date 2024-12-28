using Microsoft.AspNetCore.Identity;

namespace AspNetCore.WebApi.Template.Infrastructure.Identity;

public class ApplicationUserToken : IdentityUserToken<Guid>
{
}

public class ApplicationUserLogin : IdentityUserLogin<Guid>
{
}

public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
}

public class ApplicationUserRole : IdentityUserRole<Guid>
{
}

public class ApplicationUser : IdentityUser<Guid>
{
}

public class ApplicationUserClaim : IdentityUserClaim<Guid>
{
}

public class ApplicationRole : IdentityRole<Guid>
{
}
