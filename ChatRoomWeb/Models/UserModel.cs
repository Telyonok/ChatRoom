using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ChatRoomWeb.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
