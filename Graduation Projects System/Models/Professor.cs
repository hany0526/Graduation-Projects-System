using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation_Projects_System.Models
{
    public class Professor 
    {
        [Required]
        public int id { set; get; }

        [Required]
        [DisplayName("Professor Name")]
        public string name { set; get; }

        [Required]
        public string email { set; get; }

        [Required]
        [MinLength(5)]
        public string password { set; get; }

        public int Phone { get; set; }

        public Department Department { get; set; }
        public int Departmentid { get; set; }

        public static implicit operator Professor(int v)
        {throw new NotImplementedException();}
    }
}