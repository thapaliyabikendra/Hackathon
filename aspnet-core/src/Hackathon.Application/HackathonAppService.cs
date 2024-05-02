using System;
using System.Collections.Generic;
using System.Text;
using Hackathon.Localization;
using Volo.Abp.Application.Services;

namespace Hackathon;

/* Inherit your application services from this class.
 */
public abstract class HackathonAppService : ApplicationService
{
    protected HackathonAppService()
    {
        LocalizationResource = typeof(HackathonResource);
    }
}
