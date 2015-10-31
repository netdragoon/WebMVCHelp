using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMVCHelp.DAL.Contracts
{
    public interface IDal<T>
    {
        IConnection Connection { get; }
        T Add(T Model);
        bool Edit(T Model);
        bool Remove(T Model);
        bool Remove(params object[] Keys);
        IEnumerable<T> All();
        T Find(params object[] Keys);
        PagedList.IPagedList<T> All(int Page, int Total = 10);
    }
}
