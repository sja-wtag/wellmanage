using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using wellmanage.domain.Entity;

namespace wellmanage.application.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GetAccessToken(List<Claim> authClaims);
    string GetRefreshToken();
    bool ValidateToken(string token);
    List<Claim> PrepareAuthClaimFromUser(User user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}