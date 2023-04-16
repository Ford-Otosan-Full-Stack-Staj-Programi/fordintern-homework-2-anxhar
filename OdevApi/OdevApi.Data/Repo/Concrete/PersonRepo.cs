using Microsoft.EntityFrameworkCore;
using OdevApi.Data.Repo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdevApi.Data.Repo.Concrete
{
    public class PersonRepo: GenericRepository<Person>, IPersonRepo 
    {
        private DbSet<Person> Persons;
        public PersonRepo(AppDbContext context): base(context)
        {
            this.Persons = this.context.Set<Person>();
        }           
        public List<Person> GetAllByAccountId(int accountId)
        {
            return Persons.AsNoTracking().Where(x => x.AccountId == accountId).ToList();
        }
    }
}
