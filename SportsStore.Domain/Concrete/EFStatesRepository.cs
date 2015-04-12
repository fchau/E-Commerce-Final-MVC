using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class EFStatesRepository : IStatesRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<States> States
        {
            get { return context.States; }
        }
    }
}
