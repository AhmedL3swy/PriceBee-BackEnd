using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AddProductDTO
    {
        //ProductPostDTO
        public ProductPostDTO ProductPostDTO { get; set; }
        //ProductLinkDTO   
        public List<ProductDetailPostDTO> ProductDetailDTO { get; set; }

        public List<ProductImageDTO> ProductImageDTO { get; set; }

    }
}
