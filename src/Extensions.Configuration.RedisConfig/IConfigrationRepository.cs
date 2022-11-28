namespace Extensions.Configuration.RedisConfig
{
    public interface IConfigrationRepository : IDisposable
    {
        IDictionary<string, string> GetConfig();

        void Watch(IConfigrationWatcher watcher);
    }
}
