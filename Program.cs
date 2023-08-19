using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
//using System.Data.Entity;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = new HostBuilder()
        .ConfigureFunctionsWorkerDefaults()
        .ConfigureServices((s) =>
        {
            //s.AddSingleton<ISqlClient, SqlClient>();
            s.AddDbContext<BloggingContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("sqlconnstring"), sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(2),
                    errorNumbersToAdd: null);
                });
            });
        })
    .Build();

        host.Run();
    }
}