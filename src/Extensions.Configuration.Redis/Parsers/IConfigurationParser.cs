namespace Extensions.Configuration.Redis.Parsers
{
    public interface IConfigurationParser
    {
        IDictionary<string, string> Parse(string json);
    }
}