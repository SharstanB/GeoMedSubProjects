using GeoMed.Model.Account;
using GeoMed.SqlServer;
using GM.Base;
using IdentityServer.Dto;
using IdentityServer.IData;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        private SignInManager<GMUser> SignInManager { get; }

        private UserManager<GMUser> UserManager { get; }

        private  IConfiguration Configration { get; }

        private readonly IIdentityServerInteractionService _interactionService;

        public AccountRepository(GMApiContext gMApiContext,
            SignInManager<GMUser> _SignInManager,
             UserManager<GMUser> _UserManager,
             IConfiguration configuration)
            : base(gMApiContext)
        {
            SignInManager = _SignInManager;
            UserManager = _UserManager;
            Configration = configuration;
        }

        
        public async Task<OperationResult<SignInDto>> SignIn(SignInDto signInDto)
        {
            OperationResult<SignInDto> operation = new OperationResult<SignInDto>();

            try
            {

                var userEntity = Context.Users.Where(user => !user.DeleteDate.HasValue)
                 .SingleOrDefault(user => signInDto.Username == user.UserName ||
                                            signInDto.Username == user.Email);

                if (userEntity is null)
                {
                    operation.OperationResultType = OperationResultTypes.NotExist;
                    operation.OperationResultMessage = "NotExist execution :" + nameof(SignIn);

                    return operation;
                }

                var loginResult = await SignInManager.PasswordSignInAsync(userEntity, signInDto.Password, signInDto.RememberMe, false);
                if (loginResult == SignInResult.Success)
                {
                    operation.Result = new SignInDto()
                    {
                        Id = userEntity.Id,
                        Token = generateJwtToken(new UserDto()
                        {
                            Username = userEntity.UserName,
                            Email = userEntity.Email,
                            Id = userEntity.Id
                        }),
                        RememberMe = signInDto.RememberMe,
                        Email = userEntity.Email
                    };

                    operation.OperationResultType = OperationResultTypes.Success;
                    operation.OperationResultMessage = "Success SignInManager execution :" + nameof(SignIn);

                    return operation;
                }
                operation.OperationResultType = OperationResultTypes.Failed;
                operation.OperationResultMessage = "Failed SignInManager execution :" + nameof(SignIn);
                return operation;
            } catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exeption;
                operation.Exception = ex;
                operation.OperationResultMessage = "Failed SignInManager execution :" + nameof(SignIn);
                return operation;
            }
        }

        public async Task<OperationResult<SignUpDto>> SignUp(SignUpDto signUpDto)
        {
            OperationResult<SignUpDto> operation = new OperationResult<SignUpDto>();
            try
            {
                using (var transaction = Context.Database.BeginTransaction())
                {
                    try
                    {

                        GMUser SetUser = new GMUser()
                        {
                            UserName = signUpDto.UserName,
                            Email = signUpDto.UserName,
                            FirstName = signUpDto.FirstName ,
                            LastName = signUpDto.LastName,
                        };

                        var result = await UserManager.CreateAsync(SetUser, signUpDto.Password);


                        if (result == IdentityResult.Success)
                        {
                            signUpDto.Token = generateJwtToken(new UserDto()
                            {
                                Id = signUpDto.Id,
                                Email = signUpDto.Email,
                                Username = signUpDto.Username
                            });
                            operation.Result = signUpDto;
                            operation.OperationResultType = OperationResultTypes.Success;
                            operation.OperationResultMessage = "Success CreateAsync execution :" + nameof(SignUp);
                            transaction.Commit();
                            return operation;
                        }

                        operation.OperationResultType = OperationResultTypes.Failed;
                        operation.OperationResultMessage = "Faild DuplicateUserName CreateAsync execution :" + nameof(SignUp);
                        transaction.Rollback();

                        return operation;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        operation.OperationResultType = OperationResultTypes.Exeption;
                        operation.OperationResultMessage = "Exeption execution :" + nameof(SignUp);
                        operation.Exception = ex;

                        return operation;
                    }
                }
            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exeption;
                operation.OperationResultMessage = "Exeption execution :" + nameof(SignUp);
                operation.Exception = ex;

            }
            return operation;
        }

        public async Task<OperationResult<bool>> SignOut(string logoutId)
        {

            OperationResult<bool> operation = new OperationResult<bool>();

            try
            {
                await SignInManager.SignOutAsync();

                //var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

                //if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
                //{
                    operation.OperationResultType = OperationResultTypes.Success;
                    operation.Result = true;
                //}
            }
            catch (Exception ex)
            {
                operation.OperationResultType = OperationResultTypes.Exeption;
                operation.Exception = ex;
                operation.Result = false;
            }
            return operation;
        }

        private string generateJwtToken(UserDto user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configration.GetSection("BarearSecurity").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim( "id", user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

