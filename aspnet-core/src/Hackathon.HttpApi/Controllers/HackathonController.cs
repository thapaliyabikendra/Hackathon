using Hackathon.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Hackathon.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HackathonController : AbpControllerBase
{
    protected HackathonController()
    {
        LocalizationResource = typeof(HackathonResource);
    }
}
