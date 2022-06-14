using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YandeSignApi.Applications.SecurityAuthorization;

namespace YandeSignApi.Controllers
{
    [Authorize(AuthenticationSchemes = AuthSecurityScheme.AuthenticationSchemeSecurityMd5Auth)]
    public class Md5BaseController : ControllerBase
    {
    }
}
