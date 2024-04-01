using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.IDAM.Entities;

namespace Authorization.Data.Interfaces
{
    public interface IUserCreationProxy
    {
        Task CreateUser(User user, HttpContext httpContext);
    }
}
