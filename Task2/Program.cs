using System; 
using CardPickStrategy;

namespace Task2
{
    class Program

    {

        public static void Main(string[] args)

        {

            CreateHostBuilder(args).Build().Run();

        }

        

        public static IHostBuilder CreateHostBuilder(string[] args)        	

        {

            return Host.CreateDefaultBuilder(args)

                .ConfigureServices((hostContext, services) =>

                {

                    services.AddHostedService<CollisiumExperimentWorker>();

                    services.AddScoped<CollisiumSandbox>();

                    services.AddScoped<IDeckShuffler, RandomDeckShuffler>();

                    services.AddScoped<ICardPickStrategy, FirstCardStrategy>();
            
                    services.AddScoped<IPartner, Mark>();
                
                    services.AddScoped<IPartner, Ilon>();
                    
                    services.AddScoped<Deck>();

                });

        }

    }
}