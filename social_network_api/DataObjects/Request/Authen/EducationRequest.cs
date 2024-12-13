using social_network_api.DataObjects.Request.Common;
using System;

namespace social_network_api.DataObjects.Request.Authen
{
    public class EducationRequest : PaggingRequest
    {
        public int Id_User { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? EstablishedYear { get; set; }
        public string? Description { get; set; }
    }
}
