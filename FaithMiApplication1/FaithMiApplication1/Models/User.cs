using System;
using System.Collections.Generic;

#nullable disable

namespace FaithMiApplication1.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
