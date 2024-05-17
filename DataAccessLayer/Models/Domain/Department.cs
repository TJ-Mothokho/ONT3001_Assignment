using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Domain
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        // Required attribute is used to specify that a data field value is required.
        // ErrorMessage is the error message to be displayed if the input value is null.
        // The StringLength attribute is used to specify the maximum and minimum length of characters that are allowed for the property.
        // The RegularExpression attribute is used to specify that only letters, spaces and hypens can used for the property.
        // The Display attribute is used to specify the display name of the property.

        [Required(ErrorMessage = "Please enter your department name")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Department Name should be at least 2 characters and not longer than 255 characters!")]
        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Department Name should contain only letters, spaces, or hyphens!")]
        [Display(Name = "Department Name")]
        public string Name { get; set; }
    }
}
