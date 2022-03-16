using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnityOfWorkQuery : IUnityOfWorkBase
    {
        Task<int> Commit(CancellationToken cancellationToken);
    }
}
