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
    public class ClientGrantTypeController : Controller
    {
        public IManageOpenIDClient manageOpenIDClient;
        public ClientGrantTypeController(IManageOpenIDClient manageOpenIDClient)
        {
            this.manageOpenIDClient = manageOpenIDClient;
        }

        public async Task<IActionResult> GetClientGrantType(int id)
        {
            ViewBag.clientID = id;            
            return PartialView("ListAllowedGrantTypes", await manageOpenIDClient.FindClientGrantTypeByClientIdAsync(id));
        }

        public IActionResult Create(int clientID)
        {
            //var id = await manageOpenIDClient.GetIdByClientID(clientID);
            return View("_AddOrEdit", new ClientGrantType() { ClientId = clientID}); 
        }

        public async Task<IActionResult> EditClientGrantType(int id,int clientId)
        { 
            return View("_AddOrEdit", await manageOpenIDClient.FindClientGrantTypeByIdAsync(id, clientId));
        }

        [HttpPost]
        public async Task<IActionResult> EditClientGrantType(ClientGrantType client)
        { 
            if (ModelState.IsValid)
            {
                await manageOpenIDClient.StoreClientGrantType(client); 
                return RedirectToAction("GetClientGrantType", new { id = client.ClientId });
            }
            return View("_AddOrEdit", client);
        }
         
        public async Task<IActionResult> DeleteClientGrantType(int id,int clientId)
        {
            await manageOpenIDClient.RemoveClientGrantType(id, clientId);
            return RedirectToAction("GetClientGrantType", new { id = clientId });
        } 

        public IActionResult ClientGrantType()
        {
            return View();
        }
    }
}
