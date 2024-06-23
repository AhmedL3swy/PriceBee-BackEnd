using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductDetailsComponentDetailsDTO
    { 
        public int Id { get; set; }
        public string Name_Local { get; set; }
        public string Name_Global { get; set; }
        public string Description_Local { get; set; }
        public string Description_Global { get; set; }
        public List<decimal> Prices { get; set; }
        public decimal? Rating { get; set; }
        public bool isAvailable { get; set; }
        public string Brand { get; set; }
        public List<string> Images { get; set; }

        public List<DomainDTO> Domains { get; set; }
        public  List<ProductLinkDTO> ProductLinks { get; set; }



    }
}
