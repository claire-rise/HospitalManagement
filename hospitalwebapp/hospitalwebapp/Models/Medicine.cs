using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hospitalwebapp.Models
{
    public class Medicine
    {
        [Key]
        public int DrugId { get; set; }

        [Required(ErrorMessage ="Drug name is Required.")]
        [DisplayName("Drug Name")]
        public String DrugName { get; set; }

        [Required(ErrorMessage ="Price for drug is Required.")]
        [DataType(DataType.Currency)]
        public double DrugPrice { get; set; }

        [Required(ErrorMessage = "Short description of Drug is Required.")]
        [DisplayName("Description Label")]
        public String Description { get; set; }


        //Foreign Key in DrugCategory model
        public int DrugCategoryId { get; set; }
        public DrugCategory DrugsCategory { get; set; }
    }
}