using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Lshp.OpenIDConnect.Util.ConfigManager;

namespace Lshp.OpenIDConnect.Data.Repository
{
    public abstract class BaseRepository
    {
        private static string defaultConnection;
        private static bool _initialized;
        public BaseRepository(IOptions<ConfigEntry> option)
        {
            if (!_initialized)
            {
                _initialized = true;
                defaultConnection = option.Value.ConnectionStrings.DefaultConnection;
            }
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(defaultConnection);
        }
    }
}
