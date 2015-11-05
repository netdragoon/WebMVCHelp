using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMVCHelp.Models;

namespace WebMVCHelp.DAL.Interfaces
{
    public interface IDalCredit: Contracts.IDal<Credit>
    {
        IPagedList<Credit> All(string filtro, int Page, int Total = 10);
    }
}
