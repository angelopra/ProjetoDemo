using Domain.Interfaces;

namespace Business.Base
{
    public class ServiceManagerBase
    {
        public readonly IUnityOfWork _uow;

        public ServiceManagerBase(IUnityOfWork uow)
        {
            _uow = uow;
        }
    }
}
