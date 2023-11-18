using Microsoft.AspNetCore.Identity;

namespace learn.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
