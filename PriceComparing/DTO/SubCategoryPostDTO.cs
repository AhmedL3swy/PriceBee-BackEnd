using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SubCategoryPostDTO
    {
		public int Id { get; set; }
		public string Name_Local { get; set; }
        public string Name_Global { get; set; }
        public int CategoryId { get; set; }
    }
}
