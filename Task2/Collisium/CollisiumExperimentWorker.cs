using CardPickStrategy;
namespace Task2
{
    public class CollisiumExperimentWorker : BackgroundService
    {
        private readonly ILogger<CollisiumExperimentWorker> _logger;
        public IServiceProvider _services { get; }

        public CollisiumExperimentWorker(IServiceProvider services, 
        ILogger<CollisiumExperimentWorker> logger)
    {
        _services = services;
        _logger = logger;
    }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int totalExperiments = 1000;
            int successfulExperiments = 0;
            CollisiumSandbox sandbox = _services.CreateScope().ServiceProvider.GetRequiredService<CollisiumSandbox>();
            //CollisiumSandbox sandbox = new CollisiumSandbox(partner);

            for (int i = 0; i < totalExperiments; i += 1)
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                bool isSuccess =  sandbox.RunExperiment();

                if (isSuccess)
                {
                    successfulExperiments += 1;
                }

                await Task.Delay(1); 
            }

            double probability = (double)successfulExperiments / (double)totalExperiments;
            Console.Write("number succes: " + successfulExperiments + "\nnumber experiment: " 
            +  totalExperiments + "\nprobability: " + probability);

            Dispose();
        }
    }
}