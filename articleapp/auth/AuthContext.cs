using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace articleapp.auth
{
    public class AuthContext : ISecureStorage
    {
     
        private static AuthContext? _instance;

        private AuthContext() { }

        public static AuthContext Instance
        {
            get
            {
               
                if (_instance == null)
                {
                    _instance = new AuthContext();
                }

                return _instance;
            }
        }

        public Task<string?> GetAsync(string key)
        {
            return SecureStorage.GetAsync(key);
        }

        public bool Remove(string key)
        {
            return SecureStorage.Default.Remove(key);
        }

        public void RemoveAll()
        {
            SecureStorage.Default.RemoveAll();
        }

        public async Task SetAsync(string key, string value)
        {
            await SecureStorage.Default.SetAsync(key, value);
        }
    }
}
