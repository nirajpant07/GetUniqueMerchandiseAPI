using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly:OwinStartup(typeof(GUM.API.Startup))]

namespace GUM.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //for checking the authentication of the token
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44388",
                    ValidAudience= "https://localhost:4200",
                    //ValidAudience = "http://192.168.0.102:4200",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GetUniqueMerchandise@07"))
                }
            });

            HttpConfiguration config = new HttpConfiguration();

            //enabling cors
            var cors = new EnableCorsAttribute(origins: "http://192.168.0.102:4200", headers: "*", methods: "*");
            config.EnableCors(cors);

            WebApiConfig.Register(config);
        }
    }
}