using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using MP.BucketStore.Interface;
using MP.BucketStore.Models;
using MP.BucketStore.Models.Responses;

namespace MP.BucketStore
{
    public class Bucket<T> : IBucket<T> where T : class
    {
        private readonly IBucketProvider<T> _bucketProvider;
        private readonly BucketConfiguration<T> _options;

        public Bucket(IBucketProvider<T> bucketProvider, IOptions<BucketConfiguration<T>> options)
        {
            _bucketProvider = bucketProvider;
            _options = options.Value;
        }

        public async Task<WebResponse> DeleteBlob(string? blobKey)
        {
            var response = await _bucketProvider.DeleteObjectAsync(_options.BucketName, blobKey);

            return new WebResponse
            {
                ContentLength = response.ContentLength,
                ResponseMetadata = new WebResponseMetadata
                {
                    Metadata = response.ResponseMetadata.Metadata,
                    RequestId = response.ResponseMetadata.RequestId
                },
                StatusCode = response.HttpStatusCode
            };
        }

        public async Task<WebResponse> DeleteBlobs(IEnumerable<string> blobKeys)
        {
            var requestObject = new DeleteObjectsRequest
            {
                BucketName = _options.BucketName,
                Objects = blobKeys.Select(b => new KeyVersion { Key = b }).ToList()
            };

            var response = await _bucketProvider.DeleteObjectsAsync(requestObject);

            return new WebResponse
            {
                ContentLength = response.ContentLength,
                ResponseMetadata = new WebResponseMetadata
                {
                    Metadata = response.ResponseMetadata.Metadata,
                    RequestId = response.ResponseMetadata.RequestId
                },
                StatusCode = response.HttpStatusCode
            };
        }

        public async Task DownloadBlob(string? blobKey, string? filepath, IDictionary<string, object>? additionalProperties = null)
        {
            await _bucketProvider.DownloadToFilePathAsync(_options.BucketName, blobKey, filepath, additionalProperties);
        }

        public async Task<IList<string>> GetAllBlobKeys(string? prefix, IDictionary<string, object>? additionalProperties = null)
        {
            return await _bucketProvider.GetAllObjectKeysAsync(_options.BucketName, prefix, additionalProperties);
        }

        public async Task<GetResponse> GetBlob(string? blobKey)
        {
            var response = await _bucketProvider.GetObjectAsync(_options.BucketName, blobKey);

            return new GetResponse
            {
                ContentLength = response.ContentLength,
                ResponseMetadata = new WebResponseMetadata
                {
                    Metadata = response.ResponseMetadata.Metadata,
                    RequestId = response.ResponseMetadata.RequestId
                },
                StatusCode = response.HttpStatusCode,
                AcceptRanges = response.AcceptRanges,
                ContentRange = response.ContentRange,
                ETag = response.ETag,
                Expires = response.Expires,
                Key = response.Key,
                LastModified = response.LastModified,
                Response = response.ResponseStream,
                WebsiteRedirectLocation = response.WebsiteRedirectLocation
            };
        }

        public async Task<Stream> GetBlobStream(string? blobKey, IDictionary<string, object>? additionalProperties = null)
        {
            return await _bucketProvider.GetObjectStreamAsync(_options.BucketName, blobKey, additionalProperties);
        }

        public async Task<ListResponse> ListBlobs()
        {
            var response = await _bucketProvider.ListObjectsAsync(_options.BucketName);

            return new ListResponse
            {
                Blobs = response.S3Objects.Select(b => new BlobDescription
                {
                    ETag = b.ETag,
                    Key = b.Key,
                    LastModified = b.LastModified,
                    Size = b.Size
                }),
                CommonPrefixes = response.CommonPrefixes,
                ContentLength = response.ContentLength,
                Delimiter = response.Delimiter,
                IsTruncated = response.IsTruncated,
                MaxKeys = response.MaxKeys,
                ResponseMetadata = new WebResponseMetadata
                {
                    Metadata = response.ResponseMetadata.Metadata,
                    RequestId = response.ResponseMetadata.RequestId
                },
                StatusCode = response.HttpStatusCode
            };
        }

        public async Task<ListResponse> ListBlobs(string? prefix)
        {
            var response = await _bucketProvider.ListObjectsAsync(_options.BucketName, prefix);

            return new ListResponse
            {
                Blobs = response.S3Objects.Select(b => new BlobDescription
                {
                    ETag = b.ETag,
                    Key = b.Key,
                    LastModified = b.LastModified,
                    Size = b.Size
                }),
                CommonPrefixes = response.CommonPrefixes,
                ContentLength = response.ContentLength,
                Delimiter = response.Delimiter,
                IsTruncated = response.IsTruncated,
                MaxKeys = response.MaxKeys,
                ResponseMetadata = new WebResponseMetadata
                {
                    Metadata = response.ResponseMetadata.Metadata,
                    RequestId = response.ResponseMetadata.RequestId
                },
                StatusCode = response.HttpStatusCode
            };
        }

        public async Task<ListResponse> ListBlobs(string startAfterBlobKey, int? maxKeys = null, string? prefix = null)
        {
            var request = new ListObjectsV2Request
            {
                BucketName = _options.BucketName,
                Prefix = prefix,
                MaxKeys = maxKeys ?? 1000,
                StartAfter = startAfterBlobKey,
                Encoding = EncodingType.Url
            };

            var response = await _bucketProvider.ListObjectsV2Async(request);

            return new ListResponse
            {
                Blobs = response.S3Objects.Select(b => new BlobDescription
                {
                    ETag = b.ETag,
                    Key = b.Key,
                    LastModified = b.LastModified,
                    Size = b.Size
                }),
                CommonPrefixes = response.CommonPrefixes,
                ContentLength = response.ContentLength,
                ContinuationToken = response.NextContinuationToken,
                Delimiter = response.Delimiter,
                IsTruncated = response.IsTruncated,
                KeyCount = response.KeyCount,
                MaxKeys = response.MaxKeys,
                ResponseMetadata = new WebResponseMetadata
                {
                    Metadata = response.ResponseMetadata.Metadata,
                    RequestId = response.ResponseMetadata.RequestId
                },
                StatusCode = response.HttpStatusCode
            };
        }

        public async Task<ListResponse> ContinueListingBlobs(string continuationToken, int? maxKeys = null, string? prefix = null)
        {
            var request = new ListObjectsV2Request
            {
                BucketName = _options.BucketName,
                Prefix = prefix,
                MaxKeys = maxKeys ?? 1000,
                ContinuationToken = continuationToken,
                Encoding = EncodingType.Url
            };

            var response = await _bucketProvider.ListObjectsV2Async(request);

            return new ListResponse
            {
                Blobs = response.S3Objects.Select(b => new BlobDescription
                {
                    ETag = b.ETag,
                    Key = b.Key,
                    LastModified = b.LastModified,
                    Size = b.Size
                }),
                CommonPrefixes = response.CommonPrefixes,
                ContentLength = response.ContentLength,
                ContinuationToken = response.NextContinuationToken,
                Delimiter = response.Delimiter,
                IsTruncated = response.IsTruncated,
                KeyCount = response.KeyCount,
                MaxKeys = response.MaxKeys,
                ResponseMetadata = new WebResponseMetadata
                {
                    Metadata = response.ResponseMetadata.Metadata,
                    RequestId = response.ResponseMetadata.RequestId
                },
                StatusCode = response.HttpStatusCode
            };
        }

        public async Task UploadBlob(string? blobKey, string? filepath, IDictionary<string, object>? additionalProperties = null)
        {
            await _bucketProvider.UploadObjectFromFilePathAsync(_options.BucketName, blobKey, filepath, additionalProperties);
        }

        public async Task UploadBlob(string? blobKey, Stream stream, IDictionary<string, object>? additionalProperties = null)
        {
            await _bucketProvider.UploadObjectFromStreamAsync(_options.BucketName, blobKey, stream, additionalProperties);
        }
    }
}
