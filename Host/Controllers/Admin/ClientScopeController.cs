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
    public class ClientScopeController : Controller
    {
        public IManageOpenIDClient manageOpenIDClient;
        public ClientScopeController(IManageOpenIDClient manageOpenIDClient)
        {
            this.manageOpenIDClient = manageOpenIDClient;
        }

        public async Task<IActionResult> GetClientScope(int id)
        { 
            ViewBag.clientID = id;
            return PartialView("ListClientScope",await manageOpenIDClient.FindClientScopeByClientIdAsync(id));
        }

        public IActionResult Create(int clientId)
        { 
            return View("_AddOrEdit", new ClientScope() { ClientId= clientId });
        }

        public async Task<IActionResult> EditClientScope(int id,int clientId)
        {
            return View("_AddOrEdit", await manageOpenIDClient.FindClientScopeByIdAsync(id,clientId));
        }
        [HttpPost]
        public async Task<IActionResult> EditClientScope(ClientScope clientscope)
        {
            if (ModelState.IsValid)
            {
                await manageOpenIDClient.StoreClientScope(clientscope);
                return RedirectToAction("GetClientScope", new { id = clientscope.ClientId }); 
            }
            return View("_AddOrEdit", clientscope);
        }
         
        public async Task<IActionResult> DeleteClient(int id,int clientId)
        {
            await manageOpenIDClient.RemoveClientScope(id, clientId); 
            return RedirectToAction("GetClientScope", new { id = clientId });
        } 

        public IActionResult Client()
        {
            return View();
        }
    }
}
