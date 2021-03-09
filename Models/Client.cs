using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations;


namespace NBD.Models
{
    public class Client 
    {
        [Display(Name = "Client")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;             
                  
            }
        }
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage ="First Name can be no longer than 50 characters.")]
        public string FirstName { get; set; }

        [Display(Name ="Last Name")]
        [Required(ErrorMessage ="Last Name is required.")]
        [StringLength(50, ErrorMessage ="Last Name can be no longer than 50 characters.")]
        public string LastName { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(50, ErrorMessage = "Company name can be no longer than 50 characters.")]
        public string CompanyName { get; set; }
        public string Position { get; set; }

        [Display(Name = "Phone Number (e.g 888-888-8888")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64 PhoneNumber { get; set; } 

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please choose a city")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "Province is required.")]
        public string Province { get; set; }

        [Display(Name = "Postal Code (e.g A1AA1A")]
        [Required(ErrorMessage = "Postal Code is required")]
        [StringLength(6, ErrorMessage = "Please enter a valid postal code")]
        public string PostalCode { get; set; }

        [Display(Name ="Email (e.g johndoe@gmail.com")]
        [Required(ErrorMessage = "Email Address is required.")]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public City City { get; set; }
        public ICollection<Project> Projects { get; set; }
        


    }
}
