using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_Structure.App.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public decimal? ProductPrice { get; set; }

        public string? ProductImageUrl { get; set; }

        public string? Notes { get; set; }
    }
}
