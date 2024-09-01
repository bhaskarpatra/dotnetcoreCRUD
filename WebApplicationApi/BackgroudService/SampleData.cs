using System.Collections.Concurrent;

namespace dotNetWebApplication.BackgroudService
{
    public class SampleData
    {
        public ConcurrentBag<string> Data { get; set; } =  new ConcurrentBag<string>();
    }
}
