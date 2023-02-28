using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.AuthenticationModels
{
    public class UserCredentials
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class ForgotPassword
    {
        public string UserId { get; set; }
        public string OTP { get; set; }
    }

    public class RegisterationInfo
    {
        public string OTP { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobNo { get; set; }
        public string PreferredUserId { get; set; }
        public string PreferredPassword { get; set; }
    }

    public class PasswordChange
    {
        //public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
