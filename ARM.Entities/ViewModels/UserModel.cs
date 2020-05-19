﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARM.Entities.ViewModels
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();

        public string RoleList => string.Join(", ", Roles.Select(x => x.Value));
    }
}
