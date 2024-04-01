﻿using Authorization.Data.Enum;

namespace TaskManagement.IDAM.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public RoleType RoleType { get; set; }

    }
}
