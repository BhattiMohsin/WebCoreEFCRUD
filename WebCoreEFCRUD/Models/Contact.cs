using System.ComponentModel.DataAnnotations;

namespace WebCoreEFCRUD.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; } = "";

        [MaxLength(100)]
        public string LastName { get; set; } = "";

        [MaxLength(100)]
        public string Email { get; set; } = "";

        [MaxLength(100)]
        public string Phone { get; set; } = "";

        [MaxLength(100)]
        public string Subject { get; set; } = "";

        public string Message { get; set; } = "";

        public DateTime CreatedAt { get; set; }


        // navigation property
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
