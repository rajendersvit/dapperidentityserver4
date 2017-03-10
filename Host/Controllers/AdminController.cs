using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Lshp.OpenIDConnect.FilterAttributes;
using Lshp.OpenIDConnect.Service.AdminService;
using Lshp.OpenIDConnect.Service.AdminService.ClientViewModels;

namespace Lshp.OpenIDConnect.Controllers
{
    [Authorize(Roles = "Admin")]
    [SecurityHeaders]
    public class AdminController : Controller
    {
        public IManageOpenIDClient manageOpenIDClient;
        public AdminController(IManageOpenIDClient manageOpenIDClient)
        {
            this.manageOpenIDClient = manageOpenIDClient;
        }
        // GET: /<controller>/
        public async  Task<IActionResult> Index()
        {            
            return View(await manageOpenIDClient.GetAllClients());
        }

        //public async Task<IActionResult> Edit(string id)
        //{
        //    return View(await manageOpenIDClient.FindClientByClientIdAsync(id));
        //} 

        //public async Task<IActionResult> GetAllClientClaims(string id)
        //{
        //    return View(await manageOpenIDClient.FindClientClaimByClientIdAsync(id));
        //}
        //public async Task<IActionResult> GetClientClaim(string id,string clientId)
        //{
        //    return View("_AddOrEditClaim", await manageOpenIDClient.FindClientClaimByIdAsync(id,clientId)); 
        //}
        //public async Task<IActionResult> StoreClientClaim(ClientClaim clientClaim)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await manageOpenIDClient.StoreClientClaim(clientClaim);
        //        return RedirectToAction("_AddOrEditClaim",new ClientClaim() { ClientId=clientClaim.ClientId });
        //    }
        //    else
        //    {
        //        return View("_AddOrEditClaim", clientClaim);
        //    }
        //}
        //public async Task<IActionResult> RemoveClientClaim(ClientClaim clientClaim)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await manageOpenIDClient.RemoveClientClaim(clientClaim);
        //        return RedirectToAction("_AddOrEditClaim", new ClientClaim() { ClientId = clientClaim.ClientId });
        //    }
        //    else
        //    {
        //        return View("_AddOrEditClaim", clientClaim);
        //    }
        //}

        //public async Task<IActionResult> GetAllClientSecret(string id)
        //{
        //    return View(await manageOpenIDClient.FindClientSecretByClientIdAsync(id));
        //}
        //public async Task<IActionResult> GetClientSecret(string id, string clientId)
        //{
        //    return View(await manageOpenIDClient.FindClientSecretByIdAsync(id, clientId));
        //}
        //public async Task<IActionResult> GetAllClientRedirectUri(string id)
        //{
        //    return View(await manageOpenIDClient.FindClientSecretByClientIdAsync(id));
        //}
    }
}
