using Abp.Application.Services;

namespace CatBookApp
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class CatBookAppAppServiceBase : ApplicationService
    {
        protected CatBookAppAppServiceBase()
        {
            LocalizationSourceName = CatBookAppConsts.LocalizationSourceName;
        }
    }
}