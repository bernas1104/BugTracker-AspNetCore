using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BugTrackerApi.Configurations {
  public static class AuthConfiguration {
    public static IServiceCollection AddAuthConfiguration(
      this IServiceCollection services,
      IConfiguration configuration
    ) {
      var key = Encoding.ASCII.GetBytes(configuration["JWTSecret"]);
      services.AddAuthentication(
        opt => {
          opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
      ).AddJwtBearer(
        opt => {
          opt.RequireHttpsMetadata = false;
          opt.SaveToken = true;
          opt.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
          };
        }
      );

      return services;
    }
  }
}
