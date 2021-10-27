using MP.BucketStore.Models.Responses;

namespace MP.BucketStore.Interface
{
    public interface IBucket<T> where T : class
    {
        Task<WebResponse> DeleteBlob(string? blobKey);
        Task<WebResponse> DeleteBlobs(IEnumerable<string> blobKeys);

        Task DownloadBlob(string? blobKey, string? filepath, IDictionary<string, object>? additionalProperties = null);

        Task<IList<string>> GetAllBlobKeys(string? prefix, IDictionary<string, object>? additionalProperties = null);
        Task<GetResponse> GetBlob(string? blobKey);
        Task<Stream> GetBlobStream(string? blobKey, IDictionary<string, object>? additionalProperties = null);

        Task<ListResponse> ListBlobs();
        Task<ListResponse> ListBlobs(string? prefix);
        Task<ListResponse> ListBlobs(string startAfterBlobKey, int? maxKeys = null, string? prefix = null);
        Task<ListResponse> ContinueListingBlobs(string continuationToken, int? maxKeys = null, string? prefix = null);

        Task UploadBlob(string? blobKey, string? filepath, IDictionary<string, object>? additionalProperties = null);
        Task UploadBlob(string? blobKey, Stream stream, IDictionary<string, object>? additionalProperties = null);
    }
}
