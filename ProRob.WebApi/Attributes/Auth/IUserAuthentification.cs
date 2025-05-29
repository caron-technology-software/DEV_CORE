using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRob.WebApi.Auth
{
    public interface IUserAuthetification
    {
        bool CheckCredential(string username, string password);
    }
}
