﻿using ShoppingCart.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models
{
    public class Product
    {

        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        
        public string Slug { get; set; }

        [Required, MinLength(4,ErrorMessage = "Minimum Length is 2 ")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
        [Column(TypeName = "decimal(8, 2)")]    
        public decimal Price { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "You must choose a category")]
        public long CategoryId { get; set; }
        
        public Category Category { get; set; }

        public String Image { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
        public bool IsOnSale { get; set; }

        public int Rating { get; set; } = 0;

        public int RatingCount { get; set; } = 0;

        public List<Review> Reviews { get; set; }

        [NotMapped]
        public string body { get; set; }



    }
}
