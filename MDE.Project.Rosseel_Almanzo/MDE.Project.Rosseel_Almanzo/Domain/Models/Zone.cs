using System;
using System.Collections.Generic;
using System.Text;

namespace MDE.Project.Rosseel_Almanzo.Domain.Models
{
    public class Zone
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public string OrganizerId { get; set; }
    }
}
