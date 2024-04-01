using Api.Constants;
using Api.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Application.Exceptions;

namespace Api.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IJwtTokenValidator jwtTokenValiator;
        private readonly ILogger<AuthorizationMiddleware> logger;
        private readonly IConfiguration Configuration;
        private const string claimUserId = "UserId";
        private const string authenticationType = "Bearer";
        private const string options = "OPTIONS";
        private const string swagger = "swagger";
        private const string authorization = "Authorization";
        private const string roles = "roles";
        private const string authorizationNotFound = "Authorization header not found!";
        private const string TerritoryTokens = "TerritoryTokens";

        public AuthorizationMiddleware(RequestDelegate next, IJwtTokenValidator jwtTokenValiator, ILogger<AuthorizationMiddleware> logger, IConfiguration configuration)
        {
            this.next = next;
            this.jwtTokenValiator = jwtTokenValiator;
            this.logger = logger;
            this.Configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                bool isAuthorized = false;

                try
                {
                    isAuthorized = Authorize(context);
                }
                catch (Exception ex)
                {
                    logger.LogInformation(default(EventId), ex, ex.Message);
                }

                if (isAuthorized)
                {
                    await next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(AuthorizationException) || (ex.InnerException is AuthorizationException authException && authException.Status == System.Net.HttpStatusCode.Unauthorized))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
                else
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }

        public bool Authorize(HttpContext context)
        {
            if (context.Request.Method == options)
            {
                return true;
            }

            if (context.Request.Path.Value.Contains(swagger))
            {
                return true;
            }

            if (context.Request.Headers.Keys.Contains(authorization))
            {
                string accessToken = GetBearerToken(context);
                return jwtTokenValiator.ValidateToken(accessToken);
            }

            return false;
        }

        private ClaimsIdentity GetClaimsIdentity(JwtSecurityToken jwtSecurityToken, List<string> roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(claimUserId, jwtSecurityToken.Subject));
            roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
            var claimIdentity = new ClaimsIdentity(claims, authenticationType);
            return claimIdentity;
        }

        private string GetBearerToken(HttpContext context)
        {
            string accessToken = context.Request.Headers[authorization];
            if (accessToken.Contains(authenticationType))
            {
                return accessToken.Replace("Bearer ", string.Empty);
            }
            else
            {
                return authorizationNotFound;
            }
        }
    }
}