using System;
using System.Collections.Generic;
using System.Text;

namespace MDE.Project.Rosseel_Almanzo.Domain.Models
{
    public class BaseModel<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
    }
}
