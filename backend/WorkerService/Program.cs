using Hangfire;
using Infrastructure.Repositories;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using JobLibrary;
using Domain.Interfaces;
using WorkerService;
using Infrastructure.ApiClients;
using Domain.Database;
using Domain.Entities;

var builder = Host.CreateDefaultBuilder(args)
   .ConfigureServices((hostContext, services) =>
   {
       var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");

       services.AddDbContext<ApplicationDbContext>(options =>
       {
           options.UseSqlServer(connectionString, builder =>
           {
               builder.MigrationsAssembly("Domain"); 
           });
       });


       services.AddHangfire(config => config
      .UseSqlServerStorage(connectionString, new Hangfire.SqlServer.SqlServerStorageOptions
      {
          PrepareSchemaIfNecessary = true
      }));


       services.AddHangfireServer();

       services.AddHttpClient<DogApiClient>(client =>
       {
           client.BaseAddress = new Uri("https://dogapi.dog/api/v2/");
           client.DefaultRequestHeaders.Add("Accept", "application/json");
       });

       services.AddScoped<IRepository<Breed>, BreedRepository>();

       services.AddScoped<IBreedService, BreedService>();

       services.AddScoped<BreedJob>();

       services.AddScoped<IRecurringJobManager, RecurringJobManager>();
       services.AddScoped<JobScheduler>();
   });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var jobScheduler = scope.ServiceProvider.GetRequiredService<JobScheduler>();
    jobScheduler.ScheduleJobs();
}

await app.RunAsync();
