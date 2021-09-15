
namespace IdentityServer.Dto
{
    public class SignInDto : UserDto
    {

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string Token { get; set; }
    }
}
