
namespace IdentityServer.Dto
{
    public class SignInDto
    {

        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool RememberMe { get; set; }
    }
}
