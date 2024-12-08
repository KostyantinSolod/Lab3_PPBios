using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Lab3
{
    class Consumer
    {
        private readonly Warehouse _warehouse;
        private readonly string _id;
        private readonly int _consumptionCount;

        public Consumer(Warehouse warehouse, string id, int consumptionCount)
        {
            _warehouse = warehouse;
            _id = id;
            _consumptionCount = consumptionCount;
        }

        public async Task StartAsync()
        {
            for (int i = 0; i < _consumptionCount; i++)
            {
                await _warehouse.ConsumeAsync(_id);
                await Task.Yield(); // Вивільнення потоку без затримки
            }
        }
    }
}
