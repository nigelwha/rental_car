using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class Authorization
    {
        public int AuthId { get; set; }
        public int UserId { get; set; }
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
