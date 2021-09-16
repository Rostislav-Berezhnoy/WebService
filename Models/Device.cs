using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Models
{
    public abstract class Device
    {
        public int ID { get; set; }
        public string No { get; set; }
        public string Type { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Verification Date")]
        public DateTime VerificationDate { get; set; }
    }
}