namespace WebCoreEFCRUD.Models
{
    public class Attachment
    {
        public int Id { get; set; }

        public string OriginalFileName { get; set; } = "";

        public string StorageFileName { get; set; } = "";

        // navigation property
        public int ContactId { get; set; }
    }
}
