using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Lab3
{
    class Manager
    {
        public async Task RunAsync()
        {
            int warehouseCapacity = 5; // Місткість складу
            int accessLimit = 2; // Максимальна кількість "дверей"
            int producersCount = 3; // Кількість виробників
            int consumersCount = 2; // Кількість споживачів

            var productionPlans = new List<List<int>>
            {
            new List<int> { 1, 2, 3, 4, 5 },
            new List<int> { 6, 7, 8, 9, 10 },
            new List<int> { 11, 12, 13, 14, 15 }
            };

            var consumptionPlans = new List<int> { 7, 8 }; // Кількість споживань для кожного споживача

            var warehouse = new Warehouse(warehouseCapacity, accessLimit);

            var producers = productionPlans
                .Select((plan, index) => new Producer(warehouse, index.ToString(), plan))
                .ToList();

            var consumers = consumptionPlans
                .Select((count, index) => new Consumer(warehouse, index.ToString(), count))
                .ToList();

            var producerTasks = producers.Select(p => p.StartAsync());
            var consumerTasks = consumers.Select(c => c.StartAsync());

            await Task.WhenAll(producerTasks.Concat(consumerTasks));

            Console.WriteLine("Робота завершена.");
        }
    }
}
