using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Covid_19.ViewModel
{
    public class DataViewModel
    {
        public int CountreyId { get; set; }
        [Required(ErrorMessage ="Enter Name")]
        public string Country_Name { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public DateTime Date_reported { get; set; }
        public int New_Cases { get; set; }
        public int New_deaths { get; set; }
    }
}