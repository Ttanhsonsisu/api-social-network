using social_network_api.DataObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Helpers
{
    public interface ILoggingHelpers
    {
        public Task InsertLogging(LoggingRequest longgingRequest );
    }
}
