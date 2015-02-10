using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UserListDemo.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = " Last name cannot be empty.")]
        [DisplayName("Last name")]
        public string LName { get; set; }
        [Required(ErrorMessage = " First name cannot be empty.")]
        [DisplayName("First name")]
        public string FName { get; set; }
        [Required(ErrorMessage = " Email cannot be empty.")]
        [EmailAddress(ErrorMessage = " Invalid Email Address.")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = " Age cannot be empty.")]
        [Range(18, 90,
           ErrorMessage = " Age must be between 18 and 90")]
        public int Age { get; set; }
    }
}