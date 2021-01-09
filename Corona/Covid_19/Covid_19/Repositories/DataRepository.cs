using Covid_19.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Covid_19.Repositories
{
    public interface IDataRepository
    {
        List<Region> GetAllRelated();
        List<CoronaCase> GetCoronaCases(int id);
        List<Region> GetRegionsForDropdown();
        void AddCountriesAndCases(Country c);
    }
    public class DataRepository : IDataRepository
    {
        CoronaDbContext db = new CoronaDbContext();
        public List<Region> GetAllRelated()
        {
            return db.Regions
                .Include(t => t.Countries.Select(c => c.CoronaCases))
                .ToList();
        }
        public List<CoronaCase> GetCoronaCases(int id)
        {
            return db.CoronaCases.Where(x => x.CountryId == id).ToList();
        }
        public List<Region> GetRegionsForDropdown()
        {
            return db.Regions.ToList();
        }
        public void AddCountriesAndCases(Country c)
        {
            db.Countries.Add(c);
            db.SaveChanges();
        }
    }
}