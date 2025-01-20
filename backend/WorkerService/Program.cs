using Hangfire;
using Infrastructure.Repositories;
using Infrastructure.ApiClients;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using JobLibrary;
using Infrastructure.Database;
using Domain.Interfaces;
using Domain.Entities;
using WorkerService;
using Hangfire.MySql;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


        services.AddHangfire(config => config
         .UseSimpleAssemblyNameTypeSerializer()
         .UseDefaultTypeSerializer()
         .UseStorage(new MySqlStorage(connectionString, new MySqlStorageOptions
         {
             TablesPrefix = "Hangfire"
         })));

         services.AddHangfireServer();

        services.AddScoped<IRepository<Breed>, BreedRepository>();
        services.AddHttpClient("DogApi", client =>
        {
            client.BaseAddress = new Uri("https://dogapi.dog/api/v2/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddScoped<IBreedService, BreedService>();
        services.AddScoped<BreedJob>();
    })
    .Build();

RecurringJob.AddOrUpdate<BreedJob>(
    "BreedJob",
    job => job.ExecuteAsync(),
    Cron.Hourly);

await builder.RunAsync();
