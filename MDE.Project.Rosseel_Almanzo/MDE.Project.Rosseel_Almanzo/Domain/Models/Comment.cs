using System;

namespace MDE.Project.Rosseel_Almanzo.Domain.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set;}
        public string Content { get; set; }
    }
}