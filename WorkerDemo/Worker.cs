using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerDemo.Services;
using WorkerDemo.Services.Interfaces;

namespace WorkerDemo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _log;
        private readonly IService _service;

        public Worker(ILogger<Worker> log, IService service)
        {
            _log = log;
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int runNumber = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                _log.LogInformation("Worker starting at: {time}", DateTime.Now);
                _log.LogInformation("Run number: {runNumber}", runNumber);

                _service.Run();

                _log.LogInformation("Worker ending at: {time}", DateTime.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
