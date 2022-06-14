using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YandeSignApi.Applications.SecurityAuthorization;

namespace YandeSignApi.Controllers
{
    [Authorize(AuthenticationSchemes = AuthSecurityScheme.AuthenticationSchemeSecurityRsaAuth)]
    public class RsaBaseController : ControllerBase
    {
    }
}
