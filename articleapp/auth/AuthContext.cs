using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace articleapp.auth
{
    public class AuthContext  : ISecureStorage
    {

        public AuthContext() {
        
        }

        public Task<string?> GetAsync(string key)
        {
           return SecureStorage.GetAsync(key);
        }

        public bool Remove(string key)
        {
           return SecureStorage.Default.Remove("access_token");
        }

        public void RemoveAll()
        {
            SecureStorage.Default.RemoveAll();
        }

        public async Task SetAsync(string key, string value)
        {
            await SecureStorage.Default.SetAsync("access_token", "secret-oauth-token-value");
        }
    }
}
