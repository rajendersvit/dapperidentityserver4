using Lshp.OpenIDConnect.FilterAttributes;
using Lshp.OpenIDConnect.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Controllers
{
    [SecurityHeaders]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;

        public HomeController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
        public IActionResult Errors(string errorCode)
        {
            if(errorCode =="404" | errorCode =="500")
            {
                return View($"~/Views/Error/{errorCode}.cshtml");
            }
            return View("~/Views/Error/Index.cshtml");
        }
    }
}