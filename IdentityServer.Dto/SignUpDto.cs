using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.Dto
{
    public class SignUpDto : UserDto
    {
        public string Password { get; set; }

        public string UserName { get; set; }


        public string Token { get; set; }

    }
}
