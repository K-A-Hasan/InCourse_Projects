using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TCMS_angularJS.Models
{
    public class Trade
    {
        public Trade()
        {
            this.Courses = new List<Course>();
        }
        [Display(Name = "ID")]
        public int TradeId { get; set; }
        [Required, StringLength(30), Display(Name = "Trade Name")]
        public string TradeName { get; set; }
        [Required, StringLength(150), Display(Name = "Trade Description")]
        public string Description { get; set; }
        [Display(Name = "Female Allowed")]
        public bool FemaleAllowed { get; set; }
        
        //
        public virtual ICollection<Course> Courses { get; set; }
    }
    public class Course
    {
       
        [Display(Name = "Course ID")]
        public int CourseId { get; set; }
        [Required, StringLength(40), Display(Name = "Course Name")]
        public string CourseName { get; set; }
        [Required, Display(Name = "Duration (Hrs)")]
        public int Duration { get; set; }

        [Required, ForeignKey("Trade"), Display(Name = "Trade")]
        public int TradeId { get; set; }
        //
        public virtual Trade Trade { get; set; }
       
    }
   
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Course> Courses { get; set; }
       
    }
}
