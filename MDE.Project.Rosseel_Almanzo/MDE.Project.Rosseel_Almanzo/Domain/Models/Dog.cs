using System;
using System.Collections.Generic;
using System.Text;

namespace MDE.Project.Rosseel_Almanzo.Domain.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
