using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JawdaTask.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int Quanity { get; set; }
        public string Note { get; set; }
        public double Price { get; set; }
        public IFormFile Picture { get; set; }
    }
}
