using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Categories> Categories
        {
            get { return context.Categories; }
        }
    }
}
