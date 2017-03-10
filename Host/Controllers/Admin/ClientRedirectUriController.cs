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
    public class ClientRedirectUriController : Controller
    {
        public IManageOpenIDClient manageOpenIDClient;
        public ClientRedirectUriController(IManageOpenIDClient manageOpenIDClient)
        {
            this.manageOpenIDClient = manageOpenIDClient;
        }

        public async Task<IActionResult> GetClientRedirectUri(int id)
        {
            ViewBag.clientID = id;
            return PartialView("ListClientRedirectUri", await manageOpenIDClient.FindClientRedirectUriByClientIdAsync(id));
        }

        public  IActionResult Create(int clientId)
        {
            return View("_AddOrEdit", new ClientRedirectUri() {  ClientId  = clientId});
        }

        public async Task<IActionResult> EditClientRedirectUri(int id,int clientId)
        {
            return View("_AddOrEdit", await manageOpenIDClient.FindClientRedirectUriByIdAsync(id,clientId));
        }
        [HttpPost]
        public async Task<IActionResult> EditClientRedirectUri(ClientRedirectUri clientRedirectUri)
        {
            if (ModelState.IsValid)
            {
                await manageOpenIDClient.StoreClientRedirectUri(clientRedirectUri);
                return RedirectToAction("GetClientRedirectUri", new { id = clientRedirectUri.ClientId });
            }
            return View("_AddOrEdit", clientRedirectUri);
        }
         
        public async Task<IActionResult> DeleteClientRedirectUri(int id,int clientId)
        {
            await manageOpenIDClient.RemoveClientRedirectUri(id, clientId);
            return RedirectToAction("GetClientRedirectUri", new { id = clientId });
        } 

        public IActionResult ClientRedirectUri()
        {
            return View();
        }
    }
}
