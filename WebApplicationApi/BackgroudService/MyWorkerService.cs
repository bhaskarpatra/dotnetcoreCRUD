namespace dotNetWebApplication.BackgroudService
{
    public class MyWorkerService : IDisposable, IHostedService
    {
        private readonly SampleData _data;
        private Timer? _timer;
        public MyWorkerService(SampleData data)
        {
            _data = data;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
           _timer = new Timer(AddToCache, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
            return Task.CompletedTask;
        }

        private void AddToCache(object? state)
        {
            _data.Data.Add($"Data added {DateTime.Now.ToLongTimeString()}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
