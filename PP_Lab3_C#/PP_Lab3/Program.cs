using System;
using System.Threading.Tasks;

namespace PP_Lab3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var manager = new Manager();
            await manager.RunAsync();
        }
    }
}
