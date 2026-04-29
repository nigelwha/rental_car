using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class CarStatus
    {
        public CarStatus()
        {
            Cars = new HashSet<Car>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }
    }
}
