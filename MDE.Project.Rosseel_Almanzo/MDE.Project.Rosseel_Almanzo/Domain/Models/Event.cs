using System;
using System.Collections.Generic;
using System.Text;

namespace MDE.Project.Rosseel_Almanzo.Domain.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateEvent { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public string OrginazerId { get; set; }
        public ICollection<string> AttendingUserNames { get; set; }
    }
}
