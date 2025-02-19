using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GyrusWebAPI.Models
{
    public partial class UserMaster
    {
        public int Userid { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
