using Domain.Interfaces;

namespace Business.Base
{
    public class ServiceManagerQueryBase : BaseBusinessComon
    {
        public readonly IUnityOfWorkQuery _uowQuery;

        public ServiceManagerQueryBase(IUnityOfWorkQuery uow)
        {
            _uowQuery = uow;
        }
    }
}
