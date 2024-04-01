using Api.Constants;
using Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Filters
{
    public class BearerTokenAttribute : AuthorizationHandler<BearerTokenAttribute>, IAuthorizationRequirement
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BearerTokenAttribute requirement)
        {
            if (context.User.Identities.Any(x => x.AuthenticationType == AuthenticationConstants.ClaimsIdentityBearer && x.IsAuthenticated))
            {
                context.Succeed(requirement);
                await Task.CompletedTask;
                return;
            }

            context.Fail();
            await Task.CompletedTask;
            throw new AuthorizationException(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}