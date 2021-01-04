using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCMS_angularJS.Models
{
    public static class DbSeeder
    {
        public static void Seed(CourseDbContext db)
        {
            Trade t1 = new Trade { TradeName = "Carpentry", Description = "Pofessional Carpanter Works", FemaleAllowed = false };
            t1.Courses.Add(new Course { CourseName = "Wood Work", Duration = 6 });
            t1.Courses.Add(new Course { CourseName = "Furniture Works", Duration = 6 });
            db.Trades.Add(t1);
            db.SaveChanges();
        }
    }
}
