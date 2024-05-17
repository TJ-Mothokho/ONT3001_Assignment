using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.Domain
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; } //this is a primary key for the Employee table

        // Required attribute is used to specify that a data field value is required.
        // ErrorMessage is the error message to be displayed if the input value is null.
        // The StringLength attribute is used to specify the maximum and minimum length of characters that are allowed for the property.
        // The RegularExpression attribute is used to specify that only letters, spaces and hypens can used for the property. (e.g. "Van Dijk", "Alexandre-Arnold", etc.)
        // The Display attribute is used to specify the display name of the property.

        [Required(ErrorMessage = "Please enter your first name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name should be at least 2 characters and not longer than 50 characters!")]
        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "First Name should contain only letters, spaces, or hyphens!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name should be at least 2 characters and not longer than 50 characters!")]
        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Last Name should contain only letters, spaces, or hyphens!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Please choose a department")]
        [Display(Name = "Department")]
        [ForeignKey("Department")]
        public int DepartmentID { get; set; } //Forein Key property
    }
}
