using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MP.BucketStore.Interface;
using MP.BucketStore.Models;

namespace MP.BucketStore
{
    public static class Dependencies
    {
        public static IServiceCollection AddBucket<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            var bucketConfigSection = configuration.GetSection($"Bucket:{typeof(T).Name}");

            var bucketConfig = bucketConfigSection.Get<BucketConfiguration<T>>();
            var amazonConfig = new AmazonS3Config()
            {
                ServiceURL = bucketConfig.ServiceURL
            };

            services.Configure<BucketConfiguration<T>>(bucketConfigSection);
            services.AddTransient<IBucketProvider<T>>(sp => new BucketProvider<T>(bucketConfig.ClientId ?? "", bucketConfig.ClientSecret ?? "", amazonConfig));
            services.AddTransient<IBucket<T>, Bucket<T>>();

            return services;
        }
    }
}