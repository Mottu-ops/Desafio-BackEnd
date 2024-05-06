using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.S3.Lib.Service;

namespace User.S3.Lib
{
    public static class S3LibModulesExtensions
    {
        public static IServiceCollection AddS3LibModules(this IServiceCollection services)
        {
            services.AddScoped<S3Uploader>(p =>
            {
                var configuration = p.GetRequiredService<IConfiguration>();
                var bucket = configuration["S3Image:Bucket"];
                var key = configuration["S3Image:Key"];
                var secret = configuration["S3Image:Secret"];

                return new S3Uploader(bucket!, key!, secret!);
            });
            return services;
        }


    }
}
