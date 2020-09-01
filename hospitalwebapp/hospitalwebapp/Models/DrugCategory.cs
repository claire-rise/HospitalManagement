using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace hospitalwebapp.Models
{
    public class DrugCategory
    {
        [Key]
        public int DrugCategoryId { get; set; }

        [Required(ErrorMessage ="Drug Category Name is Required.")]
        [StringLength(150, MinimumLength =2)]
        public String DrugCategoryName { get; set; }

        //One Category can have multiple Drugs i.e 1 - Many relationship

        public List<Medicine> Medicines { get; set; }
    }
}