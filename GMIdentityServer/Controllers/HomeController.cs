using GMIdentityServer.ViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GMIdentityServer.Controllers
{
    public class HomeController : Controller
    {

        public IIdentityServerInteractionService _IdentityServer { get; }

        public HomeController(IIdentityServerInteractionService identityServer)
        {
            _IdentityServer = identityServer;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string errorId)
        {
            var res = await _IdentityServer.GetErrorContextAsync(errorId);
            return View();
        }

    }
}
