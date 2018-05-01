using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Graduation_Projects_System.Models
{
    public class Student 
    {
        [Required]
        [DisplayName("ID")]
        public int id { set; get; }

        //------------------------------//
        [Required]
        public int level { set; get; }

        [Required]
        public double GPA { set; get; }

        [Required]
        public string skills { set; get; }

        public ApplicationUser user { get; set; }
        public string userId { get; set; }

        public string file { get; set; }
    }
}