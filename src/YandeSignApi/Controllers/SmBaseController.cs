using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YandeSignApi.Applications.SecurityAuthorization;

namespace YandeSignApi.Controllers
{

    [Authorize(AuthenticationSchemes = AuthSecurityScheme.AuthenticationSchemeSecuritySmAuth)]
    public class SmBaseController : ControllerBase
    {
        
    }
}
