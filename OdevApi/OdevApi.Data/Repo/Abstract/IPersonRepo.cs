using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdevApi.Data.Repo.Abstract
{
    public interface IPersonRepo: IGenericRepository<Person>
    {
        List<Person> GetAllByAccountId(int accountId);
    }
}
