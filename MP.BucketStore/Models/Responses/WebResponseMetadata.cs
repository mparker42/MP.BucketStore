namespace MP.BucketStore.Models.Responses
{
    public class WebResponseMetadata
    {
        public string? RequestId { get; set; }

        public IDictionary<string, string>? Metadata { get; set; }
    }
}
