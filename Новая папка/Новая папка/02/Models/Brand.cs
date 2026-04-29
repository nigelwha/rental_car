using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Models = new HashSet<Model>();
        }

        public int BrandId { get; set; }
        public string Name { get; set; } = null!;
        public string? Country { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}
