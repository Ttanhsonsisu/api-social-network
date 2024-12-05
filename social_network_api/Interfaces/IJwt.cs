using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Interfaces
{
    public interface IJwt
    {
        public string Authentitcation(string username, string password, string userType);
    }
}
