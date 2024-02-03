using System;

namespace Assets.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Registration { get; set; }
        public DateTimeOffset LastActivity { get; set; }
        public long? OrganizationId { get; set; }
    }
}
