using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace ChatRoomWeb.Models
{
    public class UserSignUp
    {
        public UserSignUp(string username, string email, string passwordHash)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            IsConfirmed= false;
            CreationDate= DateTime.Now;
        }
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
