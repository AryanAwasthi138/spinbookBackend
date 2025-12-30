using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TableTennisBooking.DTO
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
