using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apple.Model;
using Microsoft.EntityFrameworkCore;

namespace Apple.Repository
{
    public class ProductTypeRepo : IProductType
    {
        AppleDbContext db;

        public ProductTypeRepo(AppleDbContext db)
        {
            this.db = db;
        }
        public bool Delete(int id)
        {

            db.Entry(new ProductType { ProductTypeID = id }).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        public List<ProductType> Get()
        {
            return db.ProductTypes.ToList();
        }

        public ProductType Get(int id)
        {
            return db.ProductTypes.FirstOrDefault(x => x.ProductTypeID == id);
        }

        public List<ProductType> GetAll()
        {
            return db.ProductTypes
                
                .Include(x => x.Products)
                .ThenInclude(y => y.Features)

                .ToList();
        }

        public ProductType GetWithChildren(int id)
        {
            return db.ProductTypes
                 .Include(x => x.Products)
                 .FirstOrDefault(x => x.ProductTypeID == id);
        }

        public bool Insert(ProductType pt)
        {
            db.ProductTypes.Add(pt);
            return db.SaveChanges() > 0;
        }

       

        public bool Update(ProductType type, bool childIncluded = false)
        {
            var orignal = db.ProductTypes.Include(x => x.Products).First(x => x.ProductTypeID == type.ProductTypeID);
            orignal.ProductTypeName = type.ProductTypeName;

            if (type.Products != null && type.Products.Count > 0)
            {
                var c = type.Products.ToArray();
                for (var i = 0; i < c.Length; i++)
                {
                    var temp = orignal.Products.FirstOrDefault(p => p.ProductId == c[i].ProductId);
                    if (temp != null)
                    {
                        temp.ProductName = c[i].ProductName;

                    }
                    else
                    {
                        orignal.Products.Add(c[i]);
                    }
                }
                foreach (var x in orignal.Products)
                {
                    var temp = type.Products.FirstOrDefault(t => t.ProductId == x.ProductId);
                    if (temp == null)
                        db.Entry(c).State = EntityState.Deleted;
                }
            }

            return db.SaveChanges() > 0;
        }

        List<ProductType> IProductType.GetWithChild()
        {
            return db.ProductTypes
                 .Include(x => x.Products)
                 .ThenInclude(y => y.Features)
                 .ToList();
        }
    }
}
