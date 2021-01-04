using Apple.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apple.Repository
{
    public interface IProductType
    {
        List<ProductType> Get();
        List<ProductType> GetAll();

        ProductType Get(int id);
        ProductType GetWithChildren(int id);
        List<ProductType> GetWithChild();
        bool Insert(ProductType pt);
        bool Update(ProductType type, bool childIncluded = false);
        bool Delete(int id);
    }
    public interface IProduct
    {
        List<Product> Get();
        Product Get(int id);
        bool Insert(Product p);
        bool Update(Product p);
        bool Delete(int id);
        List<ProductType> GetProductTypes();
        int RecordCount();
        int TotalPages(int size);
    }
    public interface IFeature
    {
        List<Feature> Get();
        Feature Get(int id);
        bool Insert(Feature f);
        bool Update(Feature f);
        bool Delete(int id);
        List<Product> GetProducts();
    }
    public interface IAccessory
    {
        List<Accessory> Get();
        Feature Get(int id);
        bool Insert(Accessory a);
        bool Update(Accessory a);
        bool Delete(int id);
        List<ProductType> GetProductTypes();
        int RecordCount();
        int TotalPages(int size);
    }
}
