using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Abstract
{
    public interface IProductsRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Categories> Categories { get; }
    }
}
