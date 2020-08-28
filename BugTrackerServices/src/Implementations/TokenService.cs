using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using BugTrackerDomain;
using BugTrackerService.DTOs;

namespace BugTrackerService.Implementations {
  public static class TokenService {
    public static TokenDTO GenerateToken(IConfiguration configuration, User user) {
      var tokenHandler = new JwtSecurityTokenHandler();

      var key = Encoding.ASCII.GetBytes(configuration["JWTSecret"]);

      var ValidFrom = DateTime.UtcNow;
      var ValidTo = ValidFrom.AddDays(1);

      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(
          new Claim[] {
            new Claim(ClaimTypes.Name, user.FirstName + user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
            // TODO Claim Roles!
          }
        ),
        Expires = ValidTo,
        IssuedAt = ValidFrom,
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature
        )
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return new TokenDTO {
        Token = tokenHandler.WriteToken(token),
        ValidTo = ValidTo,
        ValidFrom = ValidFrom,
      };
    }
  }
}
