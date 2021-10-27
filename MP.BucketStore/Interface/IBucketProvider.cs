using Amazon.S3;

namespace MP.BucketStore.Interface
{
    public interface IBucketProvider<T> : IAmazonS3
    {
    }
}
