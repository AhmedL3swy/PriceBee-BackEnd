using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserProdHistoryDto
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public int Price { get; set; }

        public string ProductLink { get; set; }
    }
}
