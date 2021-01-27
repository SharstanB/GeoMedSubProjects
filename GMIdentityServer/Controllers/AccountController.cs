using AutoMapper;
using GMIdentityServer.ViewModels;
using IdentityServer.Dto;
using IdentityServer.IData;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GMIdentityServer.Controllers
{
    
    public class AccountController : Controller
    {

        public readonly IAccountRepository AccountRepository ;

        private readonly IMapper Mapper;


        public IIdentityServerInteractionService _IdentityServer { get; }

        public AccountController(IAccountRepository accountRepository , 
            IMapper mapper,
            IIdentityServerInteractionService identityServer)
        {
            AccountRepository = accountRepository;
            Mapper = mapper;
            _IdentityServer = identityServer;
        }
        [HttpGet]
        public  IActionResult LoginAsync(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login( LoginViewModel loginView)
        {
            var jsd = loginView.ReturnUrl;
            if (ModelState.IsValid)
            {
               await AccountRepository.SignIn(Mapper.Map<SignInDto>(loginView));

            }
          
            return  Redirect(loginView.ReturnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var result = await AccountRepository.Logout(logoutId);

            string url = Url.Action("Logout", new { logoutId = logoutId  });

            return RedirectToAction(nameof(Login)) ;
        }





    }
}
