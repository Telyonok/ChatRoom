using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ChatRoomWeb.Models
{
    public class ChatModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
