using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class Model
    {
        public Model()
        {
            Cars = new HashSet<Car>();
        }

        public int ModelId { get; set; }
        public string Name { get; set; } = null!;
        public int BrandId { get; set; }
        public string? Class { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual ICollection<Car> Cars { get; set; }
    }
}
