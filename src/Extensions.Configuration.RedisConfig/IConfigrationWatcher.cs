namespace Extensions.Configuration.RedisConfig
{
    public interface IConfigrationWatcher
    {
        void FireChange();
    }
}
