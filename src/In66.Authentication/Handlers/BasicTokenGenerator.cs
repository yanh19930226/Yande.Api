﻿
namespace In66.Authentication.Handlers
{
    public class BasicTokenGenerator : ITokenGenerator
    {
        private readonly AuthenticationInfo? _userContext;
        private readonly ILogger<BasicTokenGenerator> _logger;

        public BasicTokenGenerator(
            IHttpContextAccessor httpContextAccessor,
            ILogger<BasicTokenGenerator> logger)
        {
            _userContext = httpContextAccessor.HttpContext.RequestServices.GetService<AuthenticationInfo>();
            _logger = logger;
        }

        public static string Scheme => "Basic";

        public string GeneratorName => Scheme;

        public virtual string Create()
        {
            long userId;
            if (_userContext is null)
                userId = 0;
            else if (_userContext.Id == 0)
                userId = _userContext.ExationId;
            else
                userId = _userContext.Id;

            _logger.LogDebug($"UserContext:{userId}");
            var userName = $"{BasicTokenValidator.InternalCaller}-{userId}";
            var token = BasicTokenValidator.PackToBase64(userName);
            return token;
        }
    }
}