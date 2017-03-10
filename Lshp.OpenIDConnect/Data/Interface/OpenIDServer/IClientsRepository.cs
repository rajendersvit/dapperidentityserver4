using Lshp.OpenIDConnect.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IClientsRepository
    {
        Task<IEnumerable<Client>> GetAllClients();

        Task<int> GetIdByClientID(string clientId);
        Task<IEnumerable<Client>> SearchClients(int pageId, string clientName, string clientCode);

        Task<Client> FindClientByIdAsync(string clientId);

        Task StoreClient(Client client); 

        Task RemoveClient(int id, int clientId); 
    }
}