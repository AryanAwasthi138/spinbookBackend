using System.ComponentModel.DataAnnotations;

namespace TableTennisBooking.DTO
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "First name must have atleast {2} and maximum {1} characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Last name must have atleast {2} and maximum {1} characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone Number is not valid.")]

        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Invalid Email Address,it should be of type Eg:aryan@gmail.com")]

        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must have atleast {2} and maximum {1} charcters.")]
        public string Password { get; set; }

        [Required]
        public int CityId { get; set; }
    }
}
