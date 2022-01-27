using System.Text;
using System.Threading.Tasks;

namespace Business.Base
{
    public class BaseBusiness<TApp> : BaseBusinessComon
    {
        protected BaseBusiness(TApp context)
        {
            this._context = context;
        }

        public TApp _context { get; }

    }
}
