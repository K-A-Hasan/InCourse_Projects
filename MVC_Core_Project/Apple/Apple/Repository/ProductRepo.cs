using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apple.Model;

namespace Apple.Repository
{
    public class ProductRepo : IProduct
    {

        AppleDbContext db;
        public ProductRepo(AppleDbContext db)
        {
            this.db = db;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> Get()
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductType> GetProductTypes()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Product p)
        {
            throw new NotImplementedException();
        }

        public int RecordCount()
        {
            throw new NotImplementedException();
        }

        public int TotalPages(int size)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product p)
        {
            throw new NotImplementedException();
        }
    }
}
