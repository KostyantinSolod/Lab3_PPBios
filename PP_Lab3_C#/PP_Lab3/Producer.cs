using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Lab3
{
    class Producer
    {
        private readonly Warehouse _warehouse;
        private readonly string _id;
        private readonly IEnumerable<int> _productionPlan;

        public Producer(Warehouse warehouse, string id, IEnumerable<int> productionPlan)
        {
            _warehouse = warehouse;
            _id = id;
            _productionPlan = productionPlan;
        }

        public async Task StartAsync()
        {
            foreach (var item in _productionPlan)
            {
                await _warehouse.ProduceAsync(_id, item);
                await Task.Yield(); // Вивільнення потоку без затримки
            }
        }
    }
}
