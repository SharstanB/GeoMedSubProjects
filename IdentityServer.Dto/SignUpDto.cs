using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.Dto
{
    public class SignUpDto
    {
        public string  FirstName { get; set; }

        public string  LastName { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

    }
}
