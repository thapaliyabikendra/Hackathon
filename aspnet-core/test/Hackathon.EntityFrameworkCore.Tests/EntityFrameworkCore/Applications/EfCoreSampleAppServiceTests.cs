using Hackathon.Samples;
using Xunit;

namespace Hackathon.EntityFrameworkCore.Applications;

[Collection(HackathonTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<HackathonEntityFrameworkCoreTestModule>
{

}
