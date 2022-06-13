using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WorkerDemo.Services.Interfaces;

namespace WorkerDemo.Services
{
    public class Service : IService
    {
        private readonly ILogger<Service> _log;
        private readonly IConfiguration _configuration;

        public Service(ILogger<Service> log, IConfiguration configuration)
        {
            _log = log;
            _configuration = configuration;
        }

        public void Run()
        {
            _log.LogInformation($"Application started at: {DateTime.Now}");

            for (int i = 0; i < _configuration.GetValue<int>("Iterations"); i++)
            {
                _log.LogInformation("iteration {runNumber}", i);
            }

            _log.LogInformation($"Application finished at {DateTime.Now})");
        }
    }
}
