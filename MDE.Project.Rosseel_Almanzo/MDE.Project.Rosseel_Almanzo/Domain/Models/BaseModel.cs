using System;
using System.Collections.Generic;
using System.Text;

namespace MDE.Project.Rosseel_Almanzo.Domain.Models
{
    public class BaseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; }
        public string OrginazerId { get; set; }
    }
}
