using System.Net;

namespace MP.BucketStore.Models.Responses
{
    public class WebResponse
    {
        public WebResponseMetadata? ResponseMetadata { get; set; }

        public long? ContentLength { get; set; }

        public HttpStatusCode? StatusCode { get; set; }
    }
}
