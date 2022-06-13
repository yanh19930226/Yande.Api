using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignApi.SecurityAuthorization.RsaChecker;

namespace SignApi.Controllers
{

    [Authorize(AuthenticationSchemes = AuthSecurityScheme.AuthenticationSchemeSecurityMd5Auth)]
    public class Md5BaseController : ControllerBase
    {
    }
}
