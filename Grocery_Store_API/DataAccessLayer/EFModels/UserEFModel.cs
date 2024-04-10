﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class UserEFModel
    {
        public UserEFModel()
        {
            isAdmin = false;
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Full Name must contain only alphabets.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone Number must be 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public bool isAdmin { get; set; }
    }

}
