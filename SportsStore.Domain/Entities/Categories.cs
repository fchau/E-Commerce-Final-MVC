using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryNo { get; set; }
        public string CategoryName { get; set; }
        public int ParentID { get; set; }
        public string ParentName { get; set; }
       

    }
}
