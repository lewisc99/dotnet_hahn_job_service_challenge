using Hangfire;
using JobLibrary;

namespace WorkerService
{
    public class JobScheduler
    {
        private readonly IRecurringJobManager _recurringJobManager;

        public JobScheduler(IRecurringJobManager recurringJobManager) => _recurringJobManager = recurringJobManager;

        public void ScheduleJobs()
        {
            _recurringJobManager.AddOrUpdate<BreedJob>(
                "BreedJob",
                job => job.ExecuteAsync(),
                Cron.Hourly);
        }
    }

}
