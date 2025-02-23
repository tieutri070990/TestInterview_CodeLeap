﻿using System;
using System.Collections.Generic;

namespace WebAPI_Structure.Core.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
  
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }
}
