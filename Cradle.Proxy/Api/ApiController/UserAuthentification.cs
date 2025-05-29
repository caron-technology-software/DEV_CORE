using System;
using System.Collections.Concurrent;

using ProRob.WebApi.Auth;

namespace Cradle.Proxy
{
    public class UserAuthentification : IUserAuthetification
    {
        private static readonly ConcurrentDictionary<string, string> users = new ConcurrentDictionary<string, string>();
        static UserAuthentification()
        {
            //users.TryAdd("root", "31033");
            //users.TryAdd("manufacturer", "31039");
            //users.TryAdd("distributor", "11111");
            //users.TryAdd("user", "22222");
            users.TryAdd("zund", "zund");
            //users.TryAdd("autometrix", "autometrix");
        }

        public UserAuthentification()
        {
            //--
        }

        public bool CheckCredential(string username, string password)
        {
            if (!users.ContainsKey(username))
            {
                return false;
            }

            return users[username] == password;
        }
    }
}
