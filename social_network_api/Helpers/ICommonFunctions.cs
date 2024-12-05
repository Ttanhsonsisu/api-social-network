using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Helpers
{
    public interface ICommonFunctions
    {
        public string ComputeSha256Hash(string rawData);

    }
}
