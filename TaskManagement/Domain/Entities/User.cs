﻿using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }

        public RoleType RoleType { get; set; }
    }
}
