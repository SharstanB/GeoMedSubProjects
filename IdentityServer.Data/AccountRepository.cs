using GeoMed.Model.Account;
using GeoMed.SqlServer;
using GM.Base;
using IdentityServer.Dto;
using IdentityServer.IData;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public SignInManager<GMUser> SignInManager { get; }

        public UserManager<GMUser> UserManager { get; }

        private readonly IIdentityServerInteractionService _interactionService;

        public AccountRepository(GMApiContext gMApiContext,
            SignInManager<GMUser> _SignInManager,
             UserManager<GMUser> _UserManager)
            : base(gMApiContext)
        {
            SignInManager = _SignInManager;
            UserManager = _UserManager;
        }

        
        public async Task<OperationResult<SignInDto>> SignIn(SignInDto signInDto)
        {
            OperationResult<SignInDto> operation = new OperationResult<SignInDto>();

            try
            {

                var userEntity = Context.Users.Where(user => !user.DeleteDate.HasValue)
                 .SingleOrDefault(user => signInDto.UserName == user.UserName ||
                                            signInDto.UserName == user.Email);

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
                        UserName = userEntity.UserName,
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
    }
}

