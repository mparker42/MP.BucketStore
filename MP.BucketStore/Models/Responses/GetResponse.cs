namespace MP.BucketStore.Models.Responses
{
    public class GetResponse : WebResponse
    {
        public Stream? Response { get; set; }

        public string? Key { get; set; }

        public string? AcceptRanges { get; set; }

        public string? ContentRange { get; set; }

        public DateTime? LastModified { get; set; }

        public string? ETag { get; set; }

        public DateTime? Expires { get; set; }

        public string? WebsiteRedirectLocation { get; set; }
    }
}
