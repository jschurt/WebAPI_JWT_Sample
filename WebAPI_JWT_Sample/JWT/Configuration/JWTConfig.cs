using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_JWT_Sample.JWT.Model;

namespace WebAPI_JWT_Sample.JWT.Configuration
{
    public static class JWTConfig
    {
        public static IServiceCollection addJWT(this IServiceCollection services, IConfiguration configuration)
        {

            IConfigurationSection jwtSettingsSection = configuration.GetSection("TokenSettings");
            services.Configure<TokenSettings>(jwtSettingsSection);

            TokenSettings jwtSettings = jwtSettingsSection.Get<TokenSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            services
                .AddAuthentication(options => {
                    //Toda vez que for autenticar alguem, utilizar JWT
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //Toda vez que for validar o token, utilizar JWT
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => {
                    //Exige que se use https (para evitar o ataque man-in-the-middle)
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),

                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.ValidUrl

                    };
                });

            return services;

        } //addJWT

    } //class
} //namespace
