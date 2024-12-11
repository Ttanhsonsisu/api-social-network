using System;

namespace social_network_api.DataObjects.Request.Authen
{
    public class EducationRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? EstablishedYear { get; set; }
        public string? Description { get; set; }
    }
}
