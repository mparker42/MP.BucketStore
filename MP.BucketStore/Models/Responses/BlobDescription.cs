namespace MP.BucketStore.Models.Responses
{
    public class BlobDescription
    {
        public string? ETag { get; set; }

        public string? Key { get; set; }

        public DateTime? LastModified { get; set; }

        public long? Size { get; set; }
    }
}
