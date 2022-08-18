using System;
using System.Collections.Generic;

namespace DapperPlayGround.Infrastructure.Models
{
    public partial class VMProductDetail
    {
        public string Name { get; set; } = null!;
        public string ProductNumber { get; set; } = null!;
        public int ProductId { get; set; }
    }
}
