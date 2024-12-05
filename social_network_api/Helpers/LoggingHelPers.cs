using social_network_api.Data;
using social_network_api.DataObjects.Common;
using social_network_api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Helpers
{
    public class LoggingHelPers : ILoggingHelpers
    {
        public readonly ApplicationDBContext _context;
        
        public LoggingHelPers (ApplicationDBContext context)
        {
            this._context = context;
        }
        public async Task<Task> InsertLogging(LoggingRequest longgingRequest)
        {
            try
            {
                var logging = new Logging();
                logging.Actions = longgingRequest.Actions;
                logging.Api_Name = longgingRequest.Api_Name;
                logging.Content = longgingRequest.Content;
                logging.Date_Created = DateTime.Now;
                logging.Functions = longgingRequest.Functions;
                logging.IP = longgingRequest.IP;
                logging.Is_Call_Api = longgingRequest.Is_Call_Api;
                logging.Is_Login = longgingRequest.Is_Login;
                logging.Result_Logging = longgingRequest.Result_Logging;
                logging.User_Created = longgingRequest.User_Created;
                logging.User_type = longgingRequest.User_type;

                await _context.Loggings.AddAsync(logging);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
