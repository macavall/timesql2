using System;
using System.Data.Entity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace timesql2
{
    public class timer
    {
        private readonly ILogger _logger;
        private readonly BloggingContext _dbContext;

        public timer(ILoggerFactory loggerFactory, BloggingContext dbContext)
        {
            _logger = loggerFactory.CreateLogger<timer>();
            _dbContext = dbContext;
        }

        [Function("timer")]
        public void Run([TimerTrigger("*/5 * * * * *")] MyInfo myTimer)
        {
            var blog = new Blog()
            {
                Name = "BlogName"
            };

            _dbContext.Blogs.Add(blog);

            _dbContext.SaveChanges();



            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
           // _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
