using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_Structure.Core.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public string? Notes { get; set; }

        public decimal? Price { get; set; }
    }
}
