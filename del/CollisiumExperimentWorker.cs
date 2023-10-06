namespace del
{
    public class CollisiumExperimentWorker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int totalExperiments = 1000000;
            int successfulExperiments = 0;

            for (int i = 0; i < totalExperiments; i++)
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                bool isSuccess = true;

                if (isSuccess)
                {
                    successfulExperiments++;
                }

                // Вывести текущий прогресс в консоль
               // Console.WriteLine($"Выполнено {i + 1} из {totalExperiments} экспериментов. Успешных: {successfulExperiments}");

                //await Task.Delay(1); // Минимальная задержка, чтобы не блокировать поток
            }

            // Вывести итоговый результат в консоль
            Console.WriteLine($"Итоговый результат: {successfulExperiments} успешных из {totalExperiments} экспериментов.");

            // Остановить фоновую службу
            // (вызов Dispose на CollisiumExperimentWorker)
            Dispose();
        }
    }
}