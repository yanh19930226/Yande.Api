﻿namespace Extensions.Configuration.Redis
{
    internal sealed class ConfigQueryResult
    {
        public int Version { get; set; }

        public bool Exists { get; set; }

        public string Value { get; set; }
    }
}
