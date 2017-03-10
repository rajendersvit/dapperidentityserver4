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
    public class ClientController : Controller
    {
        public IManageOpenIDClient manageOpenIDClient;
        public ClientController(IManageOpenIDClient manageOpenIDClient)
        {
            this.manageOpenIDClient = manageOpenIDClient;
        }

        public async Task<IActionResult> GetClient(string clientId)
        {
            return View(await manageOpenIDClient.FindClientByClientIdAsync(clientId));
        }

        public  IActionResult Create()
        {
            return View("_AddOrEditNew", new Client());
        }

        public async Task<IActionResult> EditClient(string clientId)
        {
            return View("EditClient", await manageOpenIDClient.FindClientByClientIdAsync(clientId));
        }
        [HttpPost]
        public async Task<IActionResult> EditClient(Client client)
        {
            if (ModelState.IsValid)
            {
                await manageOpenIDClient.StoreClient(client);
                return RedirectToAction("EditClient", new { clientId = client.ClientId });
            }
            if (client.Id == 0)
            {
                return View("_AddOrEditNew", client);
            }
            else
            { return View("_AddOrEdit", client); }
        }
         
        public async Task<IActionResult> DeleteClient(string id,string clientId)
        {
            return View(await Task.FromResult(0));
        } 

        public IActionResult Client()
        {
            return View();
        }
    }
}
