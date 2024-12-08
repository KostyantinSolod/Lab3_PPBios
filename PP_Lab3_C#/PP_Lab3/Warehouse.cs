using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_Lab3
{
    class Warehouse
    {
        private readonly int _capacity;
        private readonly SemaphoreSlim _mutex;
        private readonly SemaphoreSlim _emptySlots;
        private readonly SemaphoreSlim _fullSlots;
        private readonly ConcurrentQueue<int> _storage;

        public Warehouse(int capacity, int accessLimit)
        {
            _capacity = capacity;
            _mutex = new SemaphoreSlim(accessLimit, accessLimit);
            _emptySlots = new SemaphoreSlim(capacity, capacity);
            _fullSlots = new SemaphoreSlim(0, capacity);
            _storage = new ConcurrentQueue<int>();
        }

        public async Task ProduceAsync(string producerId, int item)
        {
            await _emptySlots.WaitAsync(); // Чекати, поки з'явиться місце
            await _mutex.WaitAsync(); // Отримати доступ до складу

            _storage.Enqueue(item); // Додати продукцію
            Console.WriteLine($"Виробник {producerId} додав {item}. Склад: [{string.Join(", ", _storage)}]");

            _mutex.Release(); // Звільнити доступ до складу
            _fullSlots.Release(); // Збільшити лічильник зайнятих слотів
        }

        public async Task<int> ConsumeAsync(string consumerId)
        {
            await _fullSlots.WaitAsync(); // Чекати, поки з'явиться продукція
            await _mutex.WaitAsync(); // Отримати доступ до складу

            _storage.TryDequeue(out var item); // Взяти продукцію
            Console.WriteLine($"Споживач {consumerId} використав {item}. Склад: [{string.Join(", ", _storage)}]");

            _mutex.Release(); // Звільнити доступ до складу
            _emptySlots.Release(); // Збільшити лічильник вільних слотів

            return item;
        }
    }
}
