using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Covid_19.Models
{
    public class Region
    {
        public Region()
        {
            this.Countries = new List<Country>();
        }
        public int RegionId { get; set; }
        public string Region_Name { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
    }
    public class Country
    {
        public Country()
        {
            this.CoronaCases = new List<CoronaCase>();
        }
        public int CountryId { get; set; }

        [Required, StringLength(50), Display(Name = "Country")]
        public string Country_Name { get; set; }
        [Required, ForeignKey("Region")]
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<CoronaCase> CoronaCases { get; set; }

    }
    public class CoronaCase
    {
        public int CoronaCaseId { get; set; }
        [Required, Display(Name = "Date Reported"), Column(TypeName = "date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date_reported { get; set; }
        [Required,Display(Name ="New Cases")]
        public int New_Cases { get; set; }
        [Required, Display(Name = "New deaths")]
        public int New_deaths { get; set; }
        [Required, ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
    public class CoronaDbContext : DbContext
    {
        public CoronaDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CoronaCase> CoronaCases { get; set; }
    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<CoronaDbContext>
    {
        protected override void Seed(CoronaDbContext db)
        {
            string[] Region_Names = { "Asia", "Europe", "africa" };
            var i = 1;
            foreach(string s in Region_Names)
            {
                var p = new Region { Region_Name = s };
                Country con1 = new Country { Country_Name =(s.Substring(0,2)+ "Bangladesh"+(i++))};
                Country con2 = new Country { Country_Name = (s.Substring(0, 2) + "Bangladesh" + (i++)) };
                p.Countries.Add(con1);
                p.Countries.Add(con1);
                db.Regions.Add(p);
            }
            db.SaveChanges();
        }
    }
}