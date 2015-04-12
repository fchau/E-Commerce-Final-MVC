using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    public class CategoryListModel
    {
        public IEnumerable<Categories> Categories { get; set; }
    }
}