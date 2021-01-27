using GM.Base;
using IdentityServer.Dto;
using System.Threading.Tasks;

namespace IdentityServer.IData
{
    public interface IAccountRepository
    {

        Task<OperationResult<SignInDto>> SignIn(SignInDto signInDto);

        Task<OperationResult<SignUpDto>> SignUp(SignUpDto signUpDto);

        Task<OperationResult<bool>> Logout(string logoutId);
    }
}
