using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignApi.SecurityAuthorization.RsaChecker;

namespace SignApi.Controllers
{
    [Authorize(AuthenticationSchemes = AuthSecurityScheme.AuthenticationSchemeSecurityRsaAuth)]
    public class RsaBaseController : ControllerBase
    {
    }
}
