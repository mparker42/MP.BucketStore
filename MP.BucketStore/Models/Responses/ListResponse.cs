namespace MP.BucketStore.Models.Responses
{
    public class ListResponse : WebResponse
    {
        public IEnumerable<string>? CommonPrefixes { get; set; }

        public IEnumerable<BlobDescription>? Blobs { get; set; }

        public string? Delimiter { get; set; }

        public bool? IsTruncated { get; set; }

        public int? KeyCount { get; set; }

        public int? MaxKeys { get; set; }

        public string? ContinuationToken { get; set; }
    }
}
