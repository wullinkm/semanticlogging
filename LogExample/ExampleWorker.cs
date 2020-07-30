using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace LogExample
{
    public class ExampleWorker
    {
        private readonly ILogger _logger;

        public ExampleWorker(ILogger logger) => _logger = logger;

        public void Start()
        {
            _logger.LogInformation("The worker has started at: {DateTime}", DateTime.Now);
            _logger.LogInformation("The worker has processed a record with id {Id}", 45);
            _logger.LogWarning("The worker has {Records} records left to process", 100);
            _logger.LogInformation("The worker has processed a record for {@Person}", new Person { FirstName = "John", LastName = "Smith" });
            Thread.Sleep(TimeSpan.FromSeconds(2));
            _logger.LogInformation("The worker has started at: {DateTime}", DateTime.Now);
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
