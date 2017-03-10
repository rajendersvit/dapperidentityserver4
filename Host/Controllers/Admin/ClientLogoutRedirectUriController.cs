using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lshp.OpenIDConnect.Service.AdminService;
using Lshp.OpenIDConnect.FilterAttributes;
using Microsoft.AspNetCore.Authorization;
using Lshp.OpenIDConnect.Service.AdminService.ClientViewModels;

namespace Lshp.OpenIDConnect.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [SecurityHeaders]
    public class ClientLogoutRedirectUriController : Controller
    {
        public IManageOpenIDClient manageOpenIDClient;
        public ClientLogoutRedirectUriController(IManageOpenIDClient manageOpenIDClient)
        {
            this.manageOpenIDClient = manageOpenIDClient;
        }

        public async Task<IActionResult> GetClientLogoutRedirectUri(int id)
        {
            ViewBag.clientID = id;
            return PartialView("ListClientRedirectUri", await manageOpenIDClient.FindClientPostLogoutRedirectUriByClientIdAsync(id));
        }

        public  IActionResult Create(int clientId)
        {
            return View("_AddOrEdit", new ClientPostLogoutRedirectUri() {  ClientId  = clientId});
        }
         
        [HttpPost]
        public async Task<IActionResult> EditClientLogoutRedirectUri(ClientPostLogoutRedirectUri clientRedirectUri)
        {
            if (ModelState.IsValid)
            {
                await manageOpenIDClient.StoreClientPostLogoutRedirectUri(clientRedirectUri);
                return RedirectToAction("GetClientLogoutRedirectUri", new { id = clientRedirectUri.ClientId });
            }
            return View("_AddOrEdit", clientRedirectUri);
        }
         
        public async Task<IActionResult> DeleteClientLogoutRedirectUri(int id,int clientId)
        {
            await manageOpenIDClient.RemoveClientPostLogoutRedirectUri(id, clientId);
            return RedirectToAction("GetClientLogoutRedirectUri", new { id = clientId });
        } 

        public IActionResult ClientRedirectUri()
        {
            return View();
        }
    }
}
