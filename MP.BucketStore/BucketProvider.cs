using Amazon.S3;
using MP.BucketStore.Interface;

namespace MP.BucketStore
{
    public class BucketProvider<T> : AmazonS3Client, IBucketProvider<T> where T : class
    {
        public BucketProvider(string awsAccessKeyId, string awsSecretAccessKey, AmazonS3Config clientConfig) : base(awsAccessKeyId, awsSecretAccessKey, clientConfig)
        {
        }
    }
}
