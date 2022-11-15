﻿namespace In66.Caching.Core.Internal
{
    /// <summary>
    /// Adnc.Infra.Caching const value.
    /// </summary>
    public static class RedisConstValue
    {
        public static class Serializer
        {
            public const string DefaultBinarySerializerName = "binary";

            public const string DefaultProtobufSerializerName = "proto";

            public const string DefaultJsonSerializerName = "json";
        }

        public static class Provider
        {
            public const string StackExchange = "StackExchange";
            public const string FreeRedis = "FreeRedis";
            public const string ServiceStack = "ServiceStack";
            public const string CSRedis = "CSRedis";
        }

        public const int PollyTimeout = 5;
    }
}
