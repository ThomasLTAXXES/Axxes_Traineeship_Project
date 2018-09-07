using System;
using System.Collections.Generic;
using System.Text;

namespace Who.Data
{
    public class UserEntity : Entity
    {
        public string FullName { get; set; }
        public string TenantId { get; set; }
    }
}
