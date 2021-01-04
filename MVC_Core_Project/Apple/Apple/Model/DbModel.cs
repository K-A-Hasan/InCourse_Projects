using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Apple.Model
{
    public class ProductType
    {
        public ProductType()
        {
            this.Products = new List<Product>();
            this.Accessories = new List<Accessory>();
        }
        
        public int ProductTypeID { get; set; }
        [Required,Display(Name ="Product Type")]
        public string ProductTypeName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Accessory> Accessories { get; set; }
    }
    public class Product
    {
        public Product()
        {
            this.Features = new List<Feature>();
        }
        public int ProductId { get; set; }
        [Required,Display(Name ="Product Name")]

        public string ProductName { get; set; }
        [Required, Display(Name = "Release Date")]
        [DataType(DataType.Date),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required, ForeignKey("ProductType"),Display(Name ="Product Type")]
        public int ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }
        public virtual ICollection<Feature> Features { get; set; }

    }
    public class Feature
    {
        public int FeatureId { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Display { get; set; }
        public string Camera { get; set; }
        public bool KeybordCompatibility { get; set; }

        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        
    }
    public class Accessory
    {
        public int AccessoryId { get; set; }
        [Required,Display(Name ="Accessory Name")]
        public string AccessoryName { get; set; }

        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
    public class AppleDbContext : DbContext
    {
        public AppleDbContext(DbContextOptions<AppleDbContext> options) : base(options) { }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
    }
    public class seeder
    {
        public void seed (AppleDbContext db)
        {
            ProductType pt = new ProductType { ProductTypeName = "iPhone" };
            Product p = new Product { ProductName = "iPhone", ReleaseDate = DateTime.Now.Date };
            p.Features.Add(new Feature { Price = 1000, Display = "6.5 inch Amoled", Camera = "14 MP front camera", KeybordCompatibility = true });
            db.ProductTypes.Add(pt);
            db.SaveChanges();
        }
    }


}
