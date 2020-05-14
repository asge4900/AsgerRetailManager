using ARM.Repository;

namespace ARM.DesktopUI.Library.Api
{
    public interface IApiHelper
    {
        IARMReposity Repository { get; }

        void UseRest();
    }
}