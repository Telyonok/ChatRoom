using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ChatRoomWeb.ViewModels
{
    public class SignUpViewModel
    {
        [Required, StringLength(20, MinimumLength = 4, ErrorMessage = "Username can't be more than 20 and less than 4 characters long")]
        [Remote(action: "IsUniqueUsername", controller: "UserManagement", HttpMethod = "Post", ErrorMessage = "This username is already taken")]
        public string Username { get; set; }
        [Required, EmailAddress]
        [Remote(action: "IsUniqueEmail", controller: "UserManagement", HttpMethod = "Post", ErrorMessage = "This email is already registered")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Compare("Password", ErrorMessage = "ConfirmPassword and password don't match")]
        public string ConfirmPassword { get; set; }
    }
}
