using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apple.Model;

namespace Apple.Repository
{
    public class AccessoryRepo : IAccessory
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Accessory> Get()
        {
            throw new NotImplementedException();
        }

        public Feature Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductType> GetProductTypes()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Accessory a)
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

        public bool Update(Accessory a)
        {
            throw new NotImplementedException();
        }
    }
}
