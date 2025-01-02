using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhileLagoon.Domian.Entities
{
    public class Villa
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name ="price per night")]
        [Range(1,100000)]
        public double price {  get; set; }
        public int sqft {  get; set; }
        [Range(1,10)]
        public int Occupancy {  get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [Display(Name = "Image Uel")]
        public string? ImageUel {  get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set;}
    }
}
