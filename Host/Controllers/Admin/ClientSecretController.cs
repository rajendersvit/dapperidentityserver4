using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lshp.OpenIDConnect.Service.AdminService;
using Lshp.OpenIDConnect.FilterAttributes;
using Microsoft.AspNetCore.Authorization;
using Lshp.OpenIDConnect.Service.AdminService.ClientViewModels;
using IdentityServer4.Models;

namespace Lshp.OpenIDConnect.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [SecurityHeaders]
    public class ClientSecretController : Controller
    {
        public IManageOpenIDClient manageOpenIDClient;
        public ClientSecretController(IManageOpenIDClient manageOpenIDClient)
        {
            this.manageOpenIDClient = manageOpenIDClient;
        }

        public async Task<IActionResult> GetClientSecret(int id)
        { 
            ViewBag.clientID = id;
            return PartialView("ListClientSecret",await manageOpenIDClient.FindClientSecretByClientIdAsync(id));
        }

        public  IActionResult Create(int clientid)
        {
            return View("_AddOrEdit", new ClientSecret() { ClientId=clientid });
        }

        public async Task<IActionResult> EditClientSecret(int id)
        {
            return View("_AddOrEdit", await manageOpenIDClient.FindClientSecretByClientIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> EditClientSecret(ClientSecret clientSecret)
        {
            if (ModelState.IsValid)
            {
                if(clientSecret.Type == IdentityServer4.IdentityServerConstants.SecretTypes.SharedSecret)
                {
                    clientSecret.Value = clientSecret.Value.Sha256();
                }

                await manageOpenIDClient.StoreClientSecret(clientSecret); 
                return RedirectToAction("GetClientSecret", new { id = clientSecret.ClientId });
            }
            return View("_AddOrEdit",clientSecret);
        }
         
        public async Task<IActionResult> DeleteClientSecret(int id,int clientId)
        {
            await manageOpenIDClient.RemoveClientSecret(id,clientId);
            return RedirectToAction("GetClientSecret", new { id = clientId });
        }
         
    }
}
