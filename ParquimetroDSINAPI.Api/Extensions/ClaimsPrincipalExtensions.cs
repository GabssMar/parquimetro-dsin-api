using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ParquimetroDSINAPI.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetId(this ClaimsPrincipal user)
        {
            var claim = user.Claims.FirstOrDefault(c =>
                c.Type == JwtRegisteredClaimNames.Sub ||
                c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                throw new Exception("Token inválido ou ID do usuário não encontrado.");
            }

            return new Guid(claim.Value);
        }
    }
}