using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.ViewModels
{
    public class UserRegistrationInputModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
    }
}
