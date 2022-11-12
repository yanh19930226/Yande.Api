﻿namespace In66.Authentication.Handlers
{
    public interface ITokenGenerator
    {
        public static string Scheme { get; } = string.Empty;

        public string GeneratorName { get; }

        /// <summary>
        /// 创建/获取一个token
        /// </summary>
        /// <returns></returns>
        public string? Create();
    }
}
