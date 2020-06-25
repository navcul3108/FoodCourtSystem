using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FoodCourt.Models
{
    public enum accountRole
    {
        Manager,
        Cook,
        VendorOwner,
        Default
    }
    public class InternalAccount
    {
        public int Id { get; set; }
        public string userName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }
        public string emailAddress { get; set; }
        public accountRole Role { get; set; }
    }
}