using Graduation_Projects_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation_Projects_System.Models
{
    public class Idea
    {
        [Required]
        public int id { set; get; }

        public ApplicationUser leader { set; get; }
        public string leaderid { get; set; }

        [Required]
        [DisplayName("Idea Name")]
        public string name { set; get; }
        [Required]
        [DisplayName("Idea Description")]
        public string description { set; get; }
        [Required]
        [DisplayName("Tools")]
        public string tools { set; get; }

        [DisplayName("First Professor")]
        public ApplicationUser professor1 { get; set; }
        public string professor1id { get; set; }

        [DisplayName("Second Professor")]
        public ApplicationUser professor2 { get; set; }
        public string professor2id { get; set; }

        [DisplayName("Third Professor")]
        public ApplicationUser professor3 { get; set; }
        public string professor3id { get; set; }

        public int isApproved { set; get; }
    }
}