namespace del
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

                //  services.AddScoped<IDeckShufller, RandomDeckShufller>();

                //  services.AddScoped<ICardPickStrategy, FirstCardPickStrategy>();
            
                //  services.AddScoped<IPartner, Partner1>();
                
                //  services.AddScoped<IPartner, Partner2>();

                });

        }

    }
}