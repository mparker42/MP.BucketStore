namespace MP.BucketStore.Models
{
    public class BucketConfiguration<T>
    {
        public string? ServiceURL { get; set; }
        public string? BucketName { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
